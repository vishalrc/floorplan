using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace JLT.Common.Utility
{
    public static class SmsUtility
    {
        readonly static string SmsMainUrl = CommonUtility.GetAppSettingKey(Constants.SMSKey.SmsUrl);
        readonly static string SmsUserName = CommonUtility.GetAppSettingKey(Constants.SMSKey.UserName);
        readonly static string SmsPassword = CommonUtility.GetAppSettingKey(Constants.SMSKey.Password);
        readonly static string SmsSenderId = CommonUtility.GetAppSettingKey(Constants.SMSKey.SenderId);
        readonly static string StrSmsType = CommonUtility.GetAppSettingKey(Constants.SMSKey.SmsType);
        readonly static string StrLogFile = CommonUtility.GetAppSettingKey(Constants.SMSKey.LogFile);
        public static string SendSmsMessage(string contactNumber, string message)
        {
            string info = string.Empty;
            string smsString = string.Empty;
            StreamReader sr = null;
            Stream stream;
            System.Net.WebResponse response;
            System.Net.WebRequest request;
            try
            {
                smsString = SmsMainUrl + "UserName=" + SmsUserName + "&Password=" + SmsPassword + "&Type=Individual&Mask="
                    + SmsSenderId + "&To=" + contactNumber + "&Message=" + message;
                request = System.Net.WebRequest.Create(smsString);
                response = request.GetResponse();
                stream = response.GetResponseStream();
                if (stream != null) sr = new StreamReader(stream, System.Text.Encoding.ASCII);
                if (sr != null) info = sr.ReadToEnd();
                if (!string.IsNullOrEmpty(StrLogFile)) LogSmsTransaction(StrLogFile, smsString, info);
            }
            catch (Exception ex)
            {
                LogSmsTransaction(StrLogFile, smsString, ex.Message);
            }
            finally
            {
                request = null;
                response = null;
                sr = null;
                stream = null;
            }
            return info;
        }
        public static void LogSmsTransaction(string logFileName, string paramList, string responseOut)
        {
            if (!string.IsNullOrEmpty(StrLogFile))
            {
                var logStreamWriter = new StreamWriter(logFileName, true);
                try
                {
                    logStreamWriter.WriteLine("________________________________________________________");
                    logStreamWriter.WriteLine("Date: " + DateTime.Now.ToString());
                    logStreamWriter.WriteLine(paramList);
                    logStreamWriter.WriteLine(responseOut);
                    logStreamWriter.WriteLine("_________________________________________________________");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    logStreamWriter.Close();
                }
            }
        }
    }
}
