
namespace JLT.Common.Utility
{
    public static class Constants
    {
        /********************************************** Auth Token Project *******************************************************/
        public static class AuthToken
        {
            public const string SeperatorString = ",";
            public const string AES_KEY = "AES_KEY";
            public const string AES_SALT = "AES_SALT";
            public const string SaltLength = "SaltLength";
            public const string TokenValiditySec = "TokenValiditySec";
            public const string IsValidateIPAddress = "IsValidateIPAddress";
            public const string SP_GetSalt = "SP_GetSalt";
            public const string SP_UpdateSalt = "SP_UpdateSalt";
            public const string SP_ObtainAuthToken = "SP_ObtainAuthToken";
            public const string UserEntity_UserId = "UserEntity_UserId";
            public const string UserEntity_Password = "UserEntity_Password";
            public const string UserEntity_ID = "UserEntity_ID";
            public const string UserEntity_Role = "UserEntity_Role";
            public const string UserEntity_AuthToken = "UserEntity_AuthToken";
        }

        /********************************************** Stored Procedures ********************************************************/
        public static class StoredProcedures
        {
            public const string uasp_user = "uasp_user";
            public const string ussp_user = "ussp_user";
            public const string uasp_test = "uasp_test";
            public const string ussp_test = "ussp_test";
            public const string udsp_test = "udsp_test";
            public const string uasp_examinee = "uasp_examinee";
            public const string ussp_examinee = "ussp_examinee";
            public const string ussp_examinee_report = "ussp_examinee_report";
            public const string uasp_usertestassociation = "uasp_usertestassociation";
            public const string ussp_examineelog = "ussp_examineelog";
            public const string uasp_examineelog = "uasp_examineelog";
            public const string uasp_broadcastmsg = "uasp_broadcastmsg";
            public const string uasp_examinee_status = "uasp_examinee_status";
            public const string uasp_examineeauthorization = "uasp_examineeauthorization";
            public const string uasp_liveproctor = "uasp_liveproctor";
            public const string uasp_assingproctor = "uasp_assingproctor";
            public const string ussp_proctorexaminee = "ussp_proctorexaminee";
            public const string ussp_examineelogin = "ussp_examineelogin";
            public const string ussp_admindashboard = "ussp_admindashboard";

            public const string uasp_activeproctor = "uasp_activeproctor";
            public const string uasp_proctorcandidate = "uasp_proctorcandidate";
            public const string udsp_activeproctor = "udsp_activeproctor";
            public const string ussp_country = "ussp_country";
            public const string ussp_timezone = "ussp_timezone";
            public const string ussp_language = "ussp_language";
            public const string ussp_availableproctor = "ussp_availableproctor";
            public const string ussp_proctorcandidateassociation = "ussp_proctorcandidateassociation";
            public const string ussp_getlivetests = "ussp_getlivetests";
            public const string ussp_checkexamineevalidation = "ussp_checkexamineevalidation";
        }

        /********************************************** SMS Keys *****************************************************************/
        public static class SMSKey
        {
            public const string SmsUrl = "SmsUrl";
            public const string UserName = "UserName";
            public const string Password = "Password";
            public const string SenderId = "SenderId";
            public const string SmsType = "SmsType";
            public const string LogFile = "LogFile";
        }

        /********************************************** Application Configs *****************************************************/
        public static class ApplicationConfiguration
        {
            public const string RestServiceURL = "RestServiceURL";
            public const string maxcandidateperproctor = "maxcandidateperproctor";
            public const string TestEngineAPIurl = "TestEngineAPIurl";
            public const string EamilRegEx = @"EamilRegEx";
            public const string PhotoInterval = "PhotoInterval";
            public const string FaceTrackingInterval = "FaceTrackingInterval";
            public const string RejoiningInterval = "RejoiningInterval";
            public const string photolocation = "photolocation";
        }

        /********************************************** OTP *********************************************************************/
        public static class OTP
        {
            public const string OTPSecret = "OTPSecret";
        }

        /********************************************** Error Number for custom SQL Errors **************************************/
        public static class SQLCustomError
        {
            public const string e_30001 = "Locked try after 30 min";
            public const string e_30002 = "Suspicious attempt Show Captch";
            public const string e_30003 = "Wrong credentials";
            public const string e_30004 = "User does not have any role assigned";
            public const string e_30005 = "Account Locked";
            public const string e_30006 = "User has been disabled, contact administrator";
            public const string e_30007 = "Error Customer Delete: Customer does not exist";
            public const string e_30008 = "Error!: Test with this name alreay exists, choose diffrent name";
            public const string e_30009 = "Error add test!: added by user can not be null or zero";
            public const string e_30010 = "Error add Question Bank!: added by user can not be null or zero";
            public const string e_30011 = "Error!: Question bank with this name alreay exists, choose diffrent name";
            public const string e_30012 = "Error add test!: account id can not be null or zero";
            public const string e_30013 = "Error add question bank!: account id can not be null or zero";
            public const string e_30014 = "Error Get customers!: account id can not be null or zero";
            public const string e_30015 = "Error add customers!: account id can not be null or zero";
            public const string e_30016 = "Error add question!: account id can not be null or zero";
            public const string e_30017 = "Error add question!: secretkey can not be null";
            public const string e_30018 = "Error add Question!: duplicate question, this question exists in this question bank";
            public const string e_30019 = "Error add Section!: added by user can not be null or zero";
            public const string e_30020 = "Error add Section!: user is not associated with this test";
        }

        /********************************************** WebSocketKeys **************************************/
        public static class WebSocketKeys
        {
            public const string WebSocketServer = "WebSocketServer";
        }
    }
}
