using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.JLT.Common.Utility;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace com.JLT.Security.TokenAuth
{
    public class TokenService : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Role"></param>
        /// <param name="IPAddress"></param>
        /// <param name="AES_KEY"></param>
        /// <param name="AES_SALT"></param>
        /// <param name="IsAdmin">If true it will fetch HashSalt from a_Admin table else from contact table</param>
        /// <param name="SaltLength"></param>
        /// <returns></returns>
        public T obtainAuthToken<T>(T adminEntity, string IPAddress) where T : new()
        {
            try
            {
                string AES_KEY = CommonUtility.GetAppSettingKey(Constants.AuthToken.AES_KEY);
                string AES_SALT = CommonUtility.GetAppSettingKey(Constants.AuthToken.AES_SALT);
                int SaltLength = Convert.ToInt32(CommonUtility.GetAppSettingKey(Constants.AuthToken.SaltLength));

                PropertyInfo useridPropertyInfo = typeof(T).GetProperty(CommonUtility.GetAppSettingKey(Constants.AuthToken.UserEntity_UserId));
                PropertyInfo idPropertyInfo = typeof(T).GetProperty(CommonUtility.GetAppSettingKey(Constants.AuthToken.UserEntity_ID));
                PropertyInfo rolePropertyInfo = typeof(T).GetProperty(CommonUtility.GetAppSettingKey(Constants.AuthToken.UserEntity_Role));
                PropertyInfo authTokenPropertyInfo = typeof(T).GetProperty(CommonUtility.GetAppSettingKey(Constants.AuthToken.UserEntity_AuthToken));

                using (var objTokenDBService = new TokenDBService())
                {
                    var objAdminEntity = objTokenDBService.ValidateAdminCredentials(adminEntity);
                    var id = (UInt64)idPropertyInfo.GetValue(objAdminEntity, null);
                    var role = Convert.ToString(rolePropertyInfo.GetValue(objAdminEntity, null));
                    var userid = (string)useridPropertyInfo.GetValue(objAdminEntity, null);
                    var authTolen = (string)authTokenPropertyInfo.GetValue(objAdminEntity, null);

                    if (userid == (string)useridPropertyInfo.GetValue(adminEntity, null))
                    {
                        var Inner_Msg = id + Constants.AuthToken.SeperatorString + role + Constants.AuthToken.SeperatorString + IPAddress + Constants.AuthToken.SeperatorString + CryptoUtility.GenerateTimeStamp();
                        var HASH_SALT = CryptoUtility.GenerateSalt(SaltLength);
                        objTokenDBService.ChangeHashSalt(id.ToString(), HASH_SALT);
                        var Msg_Hash = CryptoUtility.GenerateHash(Inner_Msg, HASH_SALT);
                        authTokenPropertyInfo.SetValue(objAdminEntity, CryptoUtility.Encrypt(Inner_Msg + "##" + Msg_Hash, AES_KEY, AES_SALT), null);
                        return objAdminEntity;
                    }
                    else
                    {
                        throw new SecurityTokenException("-3|Error granting access token: You entered wrong UserId or Password(UserID: " + userid + " | IP Address: " + IPAddress + ")");
                    }
                }
            }
            catch (SecurityTokenException e)
            {
                throw e;
            }
            catch (MySqlException odbcEx)
            {
                throw odbcEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Method to validate an authentication token
        /// </summary>
        /// <param name="AuthToken"></param>
        /// <param name="IPAddress"></param>
        /// <param name="UserType">type of user to be validated</param>
        /// <param name="IsAuthorize"></param>
        /// <param name="Action"></param>
        /// <returns></returns>
        public bool validateAuthToken(string AuthToken, string IPAddress, bool IsAuthorize, Enums.Action Action)
        {
            try
            {
                string AES_KEY = CommonUtility.GetAppSettingKey(Constants.AuthToken.AES_KEY);
                string AES_SALT = CommonUtility.GetAppSettingKey(Constants.AuthToken.AES_SALT);
                bool IsValidateIPAddress = Convert.ToBoolean(CommonUtility.GetAppSettingKey(Constants.AuthToken.IsValidateIPAddress));
                int TokenValiditySec = Convert.ToInt32(CommonUtility.GetAppSettingKey(Constants.AuthToken.TokenValiditySec));

                var TokenBody = CryptoUtility.Decrypt(AuthToken, AES_KEY, AES_SALT);
                var LstTokenBody = com.JLT.Common.Utility.StringUtility.SplitString(TokenBody, "##");
                var LstInnerMsg = com.JLT.Common.Utility.StringUtility.SplitString(LstTokenBody[0], Constants.AuthToken.SeperatorString); //InnerMsg = UserID + Role + IPAddress + CryptoUtility.GenerateTimeStamp();
                var TokenHash = LstTokenBody[1];
                string Msg_Hash = string.Empty;
                using (var objTokenDBService = new TokenDBService())
                {
                    var Hash_Salt = objTokenDBService.GetHashSalt(LstInnerMsg[0]);
                    Msg_Hash = CryptoUtility.GenerateHash(LstTokenBody[0], Hash_Salt);
                }
                if (Convert.ToInt64(CryptoUtility.GenerateTimeStamp()) - Convert.ToInt64(LstInnerMsg[3]) < TokenValiditySec)
                {
                    if (IsValidateIPAddress)
                    {
                        if (!String.Equals(LstInnerMsg[2], IPAddress, StringComparison.Ordinal))
                            throw new SecurityTokenException("401|Error validating access token: Suspicious request, IP mismatch found(Token: " + AuthToken + " :: Token IP Address: " + LstInnerMsg[2] + " - Current IP Address: " + IPAddress + ")"); //Suspicious Request
                    }

                    if (String.Equals(Msg_Hash, TokenHash, StringComparison.Ordinal))
                    {
                        if (IsAuthorize)
                        {
                            if ((Convert.ToInt64(Action) & Convert.ToInt64(LstInnerMsg[1])) == Convert.ToInt64(Action))
                                return true;
                            else
                                throw new SecurityTokenException("403|User is not authorized to perform this action(Token: " + AuthToken + " :: IP Address: " + IPAddress + ")"); //User does not have role for this action
                        }
                        else
                            return true;
                    }
                    else
                        throw new SecurityTokenException("401|Error validating access token: token not valid(Token: " + AuthToken + " :: IP Address: " + IPAddress + ")"); //Token Expired
                }
                else
                    throw new SecurityTokenException("401|Error validating access token: Session has expired(Token: " + AuthToken + " :: IP Address: " + IPAddress + ")"); //Token Expired
            }
            catch (SecurityTokenException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw new Exception("401|Default Exeption(Token: " + AuthToken + " | IP Address: " + IPAddress + ")", ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AuthToken"></param>
        /// <param name="IPAddress"></param>
        /// <param name="UserType">type of user to be validated</param>
        /// <returns></returns>
        public string refreshAuthToken(string AuthToken, string IPAddress, int UserType)
        {
            string AES_KEY = CommonUtility.GetAppSettingKey(Constants.AuthToken.AES_KEY);
            string AES_SALT = CommonUtility.GetAppSettingKey(Constants.AuthToken.AES_SALT);
            string IsValidateIPAddress = CommonUtility.GetAppSettingKey(Constants.AuthToken.IsValidateIPAddress);
            return string.Empty;
        }
        /// <summary>
        /// Method to revoke/invalidate authentication token
        /// </summary>
        /// <param name="AuthToken"></param>
        /// <param name="UserType">type of user to be validated</param>
        /// <returns></returns>
        public bool revokeAuthToken(string AuthToken)
        {
            try
            {
                string AES_KEY = CommonUtility.GetAppSettingKey(Constants.AuthToken.AES_KEY);
                string AES_SALT = CommonUtility.GetAppSettingKey(Constants.AuthToken.AES_SALT);
                int SaltLength = Convert.ToInt32(CommonUtility.GetAppSettingKey(Constants.AuthToken.SaltLength));
                var TokenBody = CryptoUtility.Decrypt(AuthToken, AES_KEY, AES_SALT);
                var LstTokenBody = com.JLT.Common.Utility.StringUtility.SplitString(TokenBody, "##");
                var LstInnerMsg = com.JLT.Common.Utility.StringUtility.SplitString(LstTokenBody[0], Constants.AuthToken.SeperatorString); //InnerMsg = UserID + Role + IPAddress + CryptoUtility.GenerateTimeStamp();
                var TokenHash = LstTokenBody[1];
                using (var objTokenDBService = new TokenDBService())
                {
                    return objTokenDBService.ChangeHashSalt(LstInnerMsg[0], CryptoUtility.GenerateSalt(SaltLength));
                }

            }
            catch (MySqlException odbcEx)
            {
                throw odbcEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose(){}
    }
}
