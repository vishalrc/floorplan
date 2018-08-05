using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JLT.Floorplan.Entity;
using JLT.MySql.DataAccess;
using MySql.Data.MySqlClient;
using System.Data;
using JLT.Common.Utility;
using System.Globalization;

namespace JLT.Floorplan.DAL
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
                var employeexml = new StringBuilder();
                employeexml.Append("<es>");
                foreach (var o in obj)
                {
                    //employeexml.Append("<e examineeid=\"" + o.examineeid + "\"");
                    //employeexml.Append(" rollnumber=\"" + o.rollnumber + "\"");
                    //employeexml.Append(" examenginetestid=\"" + o.examenginetestid + "\"");
                    //employeexml.Append(" name=\"" + o.name + "\"");
                    //employeexml.Append(" idcardnumber=\"" + o.idcardnumber + "\"");
                    //employeexml.Append(" email=\"" + o.email + "\"");
                    //employeexml.Append(" fathername=\"" + o.fathername + "\"");
                    //employeexml.Append(" mothername=\"" + o.mothername + "\"");
                    //employeexml.Append(" dob=\"" + o.dob + "\"");
                    //employeexml.Append(" trainingcentreid=\"" + o.trainingcentreid + "\"");
                    //employeexml.Append(" mobile=\"" + o.mobile + "\"");
                    //employeexml.Append(" profilepicurl=\"" + o.profilepicurl + "\"");
                    //employeexml.Append(" profilepicbinary=\"" + o.profilepicbinary + "\" />");
                }
                employeexml.Append("</es>");
                DataTable dt = new DataTable();
                parameters.Add("p_employeexml", employeexml.ToString(), ParameterDirection.Input);

                int result;
                using (MySqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        var ds = db.ExecuteDataSet(conn, tran, CommandType.StoredProcedure, parameters, Constants.StoredProcedures.uasp_employee);
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


        public int SyncLeave(List<seatvacancy> obj)
        {
            MySqlDatabaseFactory db = new MySqlDatabaseFactory();
            Parameters parameters = new Parameters();
            MySqlConnection conn = db.GetDatabaseConnection();
            try
            {
                var leavexml = new StringBuilder();
                leavexml.Append("<es>");
                foreach (var o in obj)
                {
                    //leavexml.Append("<e examineeid=\"" + o.examineeid + "\"");
                    //leavexml.Append(" rollnumber=\"" + o.rollnumber + "\"");
                    //leavexml.Append(" examenginetestid=\"" + o.examenginetestid + "\"");
                    //leavexml.Append(" name=\"" + o.name + "\"");
                    //leavexml.Append(" idcardnumber=\"" + o.idcardnumber + "\"");
                    //leavexml.Append(" email=\"" + o.email + "\"");
                    //leavexml.Append(" fathername=\"" + o.fathername + "\"");
                    //leavexml.Append(" mothername=\"" + o.mothername + "\"");
                    //leavexml.Append(" dob=\"" + o.dob + "\"");
                    //leavexml.Append(" trainingcentreid=\"" + o.trainingcentreid + "\"");
                    //leavexml.Append(" mobile=\"" + o.mobile + "\"");
                    //leavexml.Append(" profilepicurl=\"" + o.profilepicurl + "\"");
                    //leavexml.Append(" profilepicbinary=\"" + o.profilepicbinary + "\" />");
                }
                leavexml.Append("</es>");
                DataTable dt = new DataTable();
                parameters.Add("p_leavexml", leavexml.ToString(), ParameterDirection.Input);

                int result;
                using (MySqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        var ds = db.ExecuteDataSet(conn, tran, CommandType.StoredProcedure, parameters, Constants.StoredProcedures.uasp_leave);
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
