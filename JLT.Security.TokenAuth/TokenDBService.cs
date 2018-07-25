using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JLT.MySql.DataAccess;
using MySql.Data.MySqlClient;
using System.Data;
using JLT.Common.Utility;
using System.Reflection;

namespace JLT.Security.TokenAuth
{
    internal class TokenDBService : IDisposable
    {
        internal string GetHashSalt(string UserID)
        {
            MySqlDatabaseFactory db = new MySqlDatabaseFactory();
            Parameters parameters = new Parameters();
            MySqlConnection conn = db.GetDatabaseConnection();
            try
            {
                parameters.Add("p_userid", Convert.ToInt32(UserID), ParameterDirection.Input);

                return db.GetScalar(conn, CommonUtility.GetAppSettingKey(Constants.AuthToken.SP_GetSalt), parameters, CommandType.StoredProcedure).ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { db.CloseConnection(conn); }
        }

        internal bool ChangeHashSalt(string UserID, string HashSalt)
        {
            MySqlDatabaseFactory db = new MySqlDatabaseFactory();
            Parameters parameters = new Parameters();
            MySqlConnection conn = db.GetDatabaseConnection();
            try
            {
                parameters.Add("p_userid", Convert.ToInt32(UserID), ParameterDirection.Input);
                parameters.Add("p_salt", HashSalt, ParameterDirection.Input);
                var result = false;
                using (MySqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        var a = db.ExecuteNonQuery(conn, tran, CommandType.StoredProcedure, CommonUtility.GetAppSettingKey(Constants.AuthToken.SP_UpdateSalt), parameters);
                        tran.Commit();
                        result = true;
                    }
                    catch
                    {
                        tran.Rollback();
                        result = false;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { db.CloseConnection(conn); }
        }

        internal T ValidateAdminCredentials<T>(T objAdminEntity) where T : new()
        {
            MySqlDatabaseFactory db = new MySqlDatabaseFactory();
            Parameters parameters = new Parameters();
            MySqlConnection conn = db.GetDatabaseConnection();
            DataSet dataSet = new DataSet();
            try
            {
                PropertyInfo useridPropertyInfo = typeof(T).GetProperty(CommonUtility.GetAppSettingKey(Constants.AuthToken.UserEntity_UserId));
                PropertyInfo passwordPropertyInfo = typeof(T).GetProperty(CommonUtility.GetAppSettingKey(Constants.AuthToken.UserEntity_Password));
                var userid = (string)useridPropertyInfo.GetValue(objAdminEntity, null);
                var password = (string)passwordPropertyInfo.GetValue(objAdminEntity, null);

                MySqlParameter p_userid = new MySqlParameter("p_userid", userid);
                p_userid.Direction = ParameterDirection.Input;
                MySqlParameter p_password = new MySqlParameter("p_password", password);
                p_password.Direction = ParameterDirection.Input;
                MySqlParameter p_secretkey = new MySqlParameter("p_secretkey", CommonUtility.GetAppSettingKey(Constants.AuthToken.AES_KEY));
                p_secretkey.Direction = ParameterDirection.Input;

                MySqlCommand cm = new MySqlCommand(CommonUtility.GetAppSettingKey(Constants.AuthToken.SP_ObtainAuthToken), conn);
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                cm.Parameters.Add(p_userid);
                cm.Parameters.Add(p_password);
                cm.Parameters.Add(p_secretkey);
                MySqlDataAdapter da = new MySqlDataAdapter(cm);
                da.Fill(dataSet);

                var lstAdminEntity = CommonUtility.ToList<T>(dataSet.Tables[0]);
                return lstAdminEntity.Count > 0 ? lstAdminEntity.First() : objAdminEntity;

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

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}

