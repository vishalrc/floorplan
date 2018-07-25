using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.JLT.Entity;
using com.JLT.DataAccess;
using MySql.Data.MySqlClient;
using System.Data;
using com.JLT.Common.Utility;
using System.Globalization;

namespace com.JLT.DAL
{
    public class MastersDAL : IDisposable
    {
        public List<Country> GetCountry(Country obj)
        {
            MySqlDatabaseFactory db = new MySqlDatabaseFactory();
            Parameters parameters = new Parameters();
            MySqlDataReader reader = null;
            MySqlConnection conn = db.GetDatabaseConnection();
            try
            {
                DataTable dt = new DataTable();
                parameters.Add("p_id", obj.id, ParameterDirection.Input);
                parameters.Add("p_name", obj.name, ParameterDirection.Input);
                parameters.Add("p_isactive", obj.isactive, ParameterDirection.Input);
                using (reader = db.GetReader(conn, CommandType.StoredProcedure, Constants.StoredProcedures.ussp_country, parameters))
                {
                    dt = db.Load(reader, true);
                }
                return CommonUtility.ToList<Country>(dt);
            }
            catch (MySqlException odbcEx)
            {
                throw odbcEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { db.CloseConnection(conn); }
        }

             public void Dispose() { }
    }
}
