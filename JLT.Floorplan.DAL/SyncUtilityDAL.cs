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
    public class SyncUtilityDAL : IDisposable
    {
      

        public int SyncEmployee(List<Employee> obj)
        {
            MySqlDatabaseFactory db = new MySqlDatabaseFactory();
            Parameters parameters = new Parameters();
            MySqlConnection conn = db.GetDatabaseConnection();
            try
            {
                var examinexml = new StringBuilder();
                examinexml.Append("<es>");
                foreach (var o in obj)
                {
                    //examinexml.Append("<e examineeid=\"" + o.examineeid + "\"");
                    //examinexml.Append(" rollnumber=\"" + o.rollnumber + "\"");
                    //examinexml.Append(" examenginetestid=\"" + o.examenginetestid + "\"");
                    //examinexml.Append(" name=\"" + o.name + "\"");
                    //examinexml.Append(" idcardnumber=\"" + o.idcardnumber + "\"");
                    //examinexml.Append(" email=\"" + o.email + "\"");
                    //examinexml.Append(" fathername=\"" + o.fathername + "\"");
                    //examinexml.Append(" mothername=\"" + o.mothername + "\"");
                    //examinexml.Append(" dob=\"" + o.dob + "\"");
                    //examinexml.Append(" trainingcentreid=\"" + o.trainingcentreid + "\"");
                    //examinexml.Append(" mobile=\"" + o.mobile + "\"");
                    //examinexml.Append(" profilepicurl=\"" + o.profilepicurl + "\"");
                    //examinexml.Append(" profilepicbinary=\"" + o.profilepicbinary + "\" />");
                }
                examinexml.Append("</es>");
                DataTable dt = new DataTable();
                parameters.Add("p_examinexml", examinexml.ToString(), ParameterDirection.Input);

                int result;
                using (MySqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        var ds = db.ExecuteDataSet(conn, tran, CommandType.StoredProcedure, parameters, Constants.StoredProcedures.uasp_examinee);
                        tran.Commit();
                        result = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                    }
                    catch (MySqlException ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
                return result;
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
