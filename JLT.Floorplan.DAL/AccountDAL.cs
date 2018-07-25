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
    public class AccountDAL : IDisposable
    {
        public Int64 SaveUser(user obj)
        {
            MySqlDatabaseFactory db = new MySqlDatabaseFactory();
            Parameters parameters = new Parameters();
            MySqlConnection conn = db.GetDatabaseConnection();
            try
            {
                DataTable dt = new DataTable();
                parameters.Add("p_id", obj.id, ParameterDirection.Input);
                parameters.Add("p_userid", obj.userid, ParameterDirection.Input);
                parameters.Add("p_usertypeid", obj.usertypeid, ParameterDirection.Input);
                parameters.Add("p_firstname", obj.firstname, ParameterDirection.Input);
                parameters.Add("p_middlename", obj.middlename, ParameterDirection.Input);
                parameters.Add("p_lastname", obj.lastname, ParameterDirection.Input);
                parameters.Add("p_emailid", obj.emailid, ParameterDirection.Input);
                parameters.Add("p_phone", obj.phone, ParameterDirection.Input);
                parameters.Add("p_mobile", obj.mobile, ParameterDirection.Input);
                parameters.Add("p_profilepic", obj.profilepic, ParameterDirection.Input);
                parameters.Add("p_country", obj.country, ParameterDirection.Input);
                parameters.Add("p_address", obj.address, ParameterDirection.Input);
                parameters.Add("p_city", obj.city, ParameterDirection.Input);
                parameters.Add("p_state", obj.state, ParameterDirection.Input);
                parameters.Add("p_zip", obj.zip, ParameterDirection.Input);
                parameters.Add("p_isenabled", obj.isenabled, ParameterDirection.Input);
                parameters.Add("p_code", obj.code, ParameterDirection.Input);
                parameters.Add("p_hash", obj.hash, ParameterDirection.Input);
                parameters.Add("p_password", obj.password, ParameterDirection.Input);
                parameters.Add("p_salt", obj.salt, ParameterDirection.Input);
                parameters.Add("p_addedby", obj.addedby, ParameterDirection.Input);
                parameters.Add("p_editedby", obj.editedby, ParameterDirection.Input);
                parameters.Add("p_timestamp", obj.timestamp, ParameterDirection.Input);
                parameters.Add("p_islockedout", obj.islockedout, ParameterDirection.Input);
                parameters.Add("p_encrykey", obj.encrykey, ParameterDirection.Input);
                parameters.Add("p_roles", obj.roles, ParameterDirection.Input);

                int result;
                using (MySqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        var ds = db.ExecuteDataSet(conn, tran, CommandType.StoredProcedure, parameters, Constants.StoredProcedures.uasp_user);
                        tran.Commit();
                        result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
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
        public List<user> GetUsers(user obj)
        {
            MySqlDatabaseFactory db = new MySqlDatabaseFactory();
            Parameters parameters = new Parameters();
            MySqlDataReader reader = null;
            MySqlConnection conn = db.GetDatabaseConnection();
            try
            {
                DataTable dt = new DataTable();
                parameters.Add("p_id", obj.id, ParameterDirection.Input);
                parameters.Add("p_userid", obj.userid, ParameterDirection.Input);
                parameters.Add("p_usertypeid", obj.usertypeid, ParameterDirection.Input);
                parameters.Add("p_firstname", obj.firstname, ParameterDirection.Input);
                parameters.Add("p_lastname", obj.lastname, ParameterDirection.Input);
                parameters.Add("p_emailid", obj.emailid, ParameterDirection.Input);
                parameters.Add("p_mobile", obj.mobile, ParameterDirection.Input);
                parameters.Add("p_isenabled", obj.isenabled, ParameterDirection.Input);
                parameters.Add("p_islockedout", obj.islockedout, ParameterDirection.Input);
                using (reader = db.GetReader(conn, CommandType.StoredProcedure, Constants.StoredProcedures.ussp_user, parameters))
                {
                    dt = db.Load(reader, true);
                }
                return CommonUtility.ToList<user>(dt);
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
        public bool AssignUserTest(seatallocation obj)
        {
            MySqlDatabaseFactory db = new MySqlDatabaseFactory();
            Parameters parameters = new Parameters();
            MySqlConnection conn = db.GetDatabaseConnection();
            try
            {
                //parameters.Add("p_userid", obj.userid, ParameterDirection.Input);
                //parameters.Add("p_userids", obj.userids, ParameterDirection.Input);
                //parameters.Add("p_testids", obj.testids, ParameterDirection.Input);

                using (MySqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        var ds = db.ExecuteDataSet(conn, tran, CommandType.StoredProcedure, parameters, Constants.StoredProcedures.uasp_usertestassociation);
                        tran.Commit();
                    }
                    catch (MySqlException ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
                return true;
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
