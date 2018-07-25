﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;


namespace com.JLT.DataAccess
{
    public class MySqlDatabaseFactory
    {
        private string _ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public string ConnectionString
        {
            get
            {
                return _ConnectionString;
            }
            set
            {
                _ConnectionString = value;
            }
        }


        #region "DataAccess Functions"

        /// <summary>
        /// GetReader Function
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdtype"></param>
        /// <param name="parameters"></param>
        /// <param name="Sql"></param>
        /// <returns></returns>
        private MySqlDataReader GetReader(MySqlConnection conn, MySqlTransaction trans, CommandType cmdtype, Parameters parameters, string Sql)
        {
            MySqlCommand cmd;
            MySqlDataReader reader;

            cmd = new MySqlCommand(Sql, conn);
            cmd.CommandType = cmdtype;

            //set transaction
            if (trans != null)
            {
                cmd.Transaction = trans;
            }

            //set parameters
            if (parameters != null)
            {
                foreach (MySqlParameter param in parameters.collection.Values)
                {
                    cmd.Parameters.Add(param);
                }
            }

            reader = cmd.ExecuteReader();

            return reader;
        }

        public MySqlDataReader GetReader(MySqlConnection conn, MySqlTransaction trans, CommandType cmdtype, string Sql)
        {
            return GetReader(conn, trans, cmdtype, null, Sql);
        }

        public MySqlDataReader GetReader(MySqlConnection conn, MySqlTransaction trans, string Sql)
        {
            return GetReader(conn, trans, CommandType.Text, null, Sql);
        }

        public MySqlDataReader GetReader(MySqlConnection conn, string Sql)
        {
            return GetReader(conn, null, CommandType.Text, null, Sql);
        }

        public MySqlDataReader GetReader(MySqlConnection conn, Parameters parameters, string Sql)
        {
            return GetReader(conn, null, CommandType.Text, parameters, Sql);
        }

        public MySqlDataReader GetReader(MySqlConnection conn, CommandType cmdtype, string Sql, Parameters parameters)
        {
            return GetReader(conn, null, cmdtype, parameters, Sql);
        }

        public Object GetScalar(MySqlConnection conn, MySqlTransaction trans, string Sql, Parameters parameters, CommandType cmdType = CommandType.Text)
        {
            Object obj;
            MySqlCommand cmd;

            cmd = new MySqlCommand(Sql, conn, trans);

            if (parameters != null)
            {
                foreach (MySqlParameter param in parameters.collection.Values)
                {
                    cmd.Parameters.Add(param);
                }
            }

            cmd.CommandType = cmdType;

            obj = cmd.ExecuteScalar();

            return obj;
        }

        public Object GetScalar(MySqlConnection conn, string Sql, Parameters parameters, CommandType cmdType = CommandType.Text)
        {
            return GetScalar(conn, null, Sql, parameters, cmdType);
        }

        public Object GetScalar(MySqlConnection conn, string Sql)
        {
            return GetScalar(conn, null, Sql, null);
        }

        public DataSet ExecuteDataSet(MySqlConnection conn, MySqlTransaction trans, CommandType cmdtype, Parameters parameters, string Sql)
        {
            MySqlCommand cmd;
            DataSet dataSet = new DataSet();
            cmd = new MySqlCommand(Sql, conn);
            cmd.CommandType = cmdtype;

            //set transaction
            if (trans != null)
            {
                cmd.Transaction = trans;
            }

            //set parameters
            if (parameters != null)
            {
                foreach (MySqlParameter param in parameters.collection.Values)
                {
                    cmd.Parameters.Add(param);
                }
            }

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dataSet);
            return dataSet;

        }

        public int ExecuteNonQuery(MySqlConnection conn, MySqlTransaction trans, string Sql, Parameters parameters)
        {
            return ExecuteNonQuery(conn, trans, CommandType.Text, Sql, parameters);
        }

        public int ExecuteNonQuery(MySqlConnection conn, MySqlTransaction trans, CommandType cmdtype, string Sql, Parameters parameters)
        {
            MySqlCommand cmd;

            cmd = new MySqlCommand(Sql, conn, trans);
            cmd.CommandType = cmdtype;

            if (parameters != null)
            {
                foreach (MySqlParameter param in parameters.collection.Values)
                {
                    cmd.Parameters.Add(param);
                }
            }

            return cmd.ExecuteNonQuery();
        }

        #endregion

        #region "Connection Handlers"

        public MySqlConnection GetDatabaseConnection()
        {
            try
            {
                //validate connection string.
                if ((ConnectionString == "") || (ConnectionString == null))
                {
                    throw new Exception(ConnectionErrors.ConnectionError_BlankConnectionString.ToString() + " - " + (int)ConnectionErrors.ConnectionError_BlankConnectionString);
                }

                MySqlConnection conn = new MySqlConnection(ConnectionString);
                conn.Open();
                return conn;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CloseConnection(MySqlConnection conn)
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public DataTable Load(IDataReader reader, bool createColumns)
        {
            var table = new DataTable();
            if (createColumns)
            {
                table.Columns.Clear();
                var schemaTable = reader.GetSchemaTable();
                foreach (DataRowView row in schemaTable.DefaultView)
                {
                    var columnName = (string)row["ColumnName"];
                    var type = (Type)row["DataType"];
                    table.Columns.Add(columnName, type);
                }
            }

            table.Load(reader);
            return table;
        }
        #endregion
    }
}