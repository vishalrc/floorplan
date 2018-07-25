using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.Routing;
using JLT.Floorplan.Entity;

namespace JLT.Common.Utility
{
    public static class CommonUtility
    {
        public static string GetAppSettingKey(string key)
        {
            return WebConfigurationManager.AppSettings[key];
        }

        //ApplicationConfigCollection
        /// <summary>
        /// Function to get Application config collections
        /// </summary>
        /// <param name="appId">ApplicationId</param>
        /// <param name="ConfigNames">Pipe seperated ConfigNames</param>
        /// <returns></returns>
        public static List<Config> GetAppicationConfigCollection(Int32 appId, string ConfigNames)
        {
            StringBuilder ConfigNameXML = new StringBuilder
            {
                Length = 0
            };
            ConfigNameXML.Append("<CS>");
            string[] configNames = ConfigNames.Split('|');
            for (int i = 0; i < configNames.Length; i++)
                ConfigNameXML.Append(@"<C cn=""" + configNames[i] + @""" />");
            ConfigNameXML.Append("</CS>");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetAppSettingKey("RestServiceURL") + "application/ApplicationConfigCollection/" + appId + "?ConfigNameXML=" + ConfigNameXML.ToString());
            request.Method = "GET";
            request.Headers.Add("Accept-Encoding", "gzip, deflate, sdch");
            request.Headers.Add("Accept-Language", "en-US,en;q=0.8");
            //request.Headers.Add("Authorization", "token " + CommonUtility.LoggedInUser.AuthToken);
            var responseResult = new APIResponseBody();

            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            string response = responseReader.ReadToEnd();
                            responseResult = CommonUtility.Deserialize<APIResponseBody>(response);
                        }
                    }
                }
            }
            catch (System.Net.WebException e)
            {
                using (Stream webStream = e.Response.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            string response = responseReader.ReadToEnd();
                            responseResult = CommonUtility.Deserialize<APIResponseBody>(response);
                        }
                    }
                }
            }
            if (responseResult.message != null)
            {
                return CommonUtility.Deserialize<List<Config>>(responseResult.message);
            }
            return new List<Config>();
        }

        /// <summary>
        /// This method copies base class object to an object of derived class, and returns the derived class object
        /// </summary>
        /// <typeparam name="D">Derived Class</typeparam>
        /// <typeparam name="B">Base Class</typeparam>
        /// <param name="objBase">Base Class Object</param>
        /// <returns>Derived Class Object</returns>
        public static D As<D, B>(Object objBase) where D : B
        {
            var Derived = typeof(D);
            var Base = typeof(B);
            var instance = Activator.CreateInstance(Derived);
            var ObjBase = Convert.ChangeType(objBase, Base);
            PropertyInfo[] properties = Base.GetProperties();
            foreach (var property in properties)
            {
                property.SetValue(instance, property.GetValue(ObjBase, null), null);
            }
            return (D)instance;
        }

        public static string Serialize<T>(T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }

        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            if (json == null)
                return obj;
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }

        #region TOList<T>

        /// <summary>
        /// Converts datatable to list<T> dynamically
        /// List<MyClass> list =  dt.ToList<Myclass>
        /// </summary>
        /// <typeparam name="T">Class name</typeparam>
        /// <param name="dataTable">data table to convert</param>
        /// <returns>List<T></returns>
        public static List<T> ToList<T>(this DataTable dataTable) where T : new()
        {
            var dataList = new List<T>();

            //Define what attributes to be read from the class
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //Read Attribute Names and Types
            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //Read Datatable column names and types
            var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
                Select(item => new
                {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var classObj = new T();

                foreach (var dtField in dtlFieldNames)
                {
                    PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                    var field = objFieldNames.Find(x => x.Name == dtField.Name);

                    if (field != null)
                    {

                        if (propertyInfos.PropertyType == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToDateTime(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToInt(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int?))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToIntNullable(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(long))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToLong(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(decimal))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToDecimal(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(ulong))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToUInt64(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(ulong?))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToUInt64Nullable(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(String))
                        {
                            if (dataRow[dtField.Name].GetType() == typeof(DateTime))
                            {
                                propertyInfos.SetValue
                                (classObj, ConvertToDateString(dataRow[dtField.Name]), null);
                            }
                            else
                            {
                                propertyInfos.SetValue
                                (classObj, ConvertToString(dataRow[dtField.Name]), null);
                            }
                        }
                        else if (propertyInfos.PropertyType == typeof(bool))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToBool(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(bool?))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToBool(dataRow[dtField.Name]), null);
                        }
                    }
                }
                dataList.Add(classObj);
            }
            return dataList;
        }

        private static string ConvertToDateString(object date)
        {
            if (date == null)
                return string.Empty;

            return Convert.ToString(Convert.ToDateTime(date));
        }
        private static string ConvertToString(object value)
        {
            if (value == null)
                return string.Empty;
            return Convert.ToString(value);
        }
        public static int ConvertToInt(object value)
        {
            if (value == null)
                return 0;
            else if (string.IsNullOrEmpty(value.ToString()))
                return 0;
            return Convert.ToInt32(value);
        }
        private static bool ConvertToBool(object value)
        {
            if (value == null)
                return false;
            return Convert.ToBoolean(value);
        }
        private static bool? ConvertToBoolNullable(object value)
        {
            if (value == null)
                return null;
            return Convert.ToBoolean(value);
        }
        private static int? ConvertToIntNullable(object value)
        {
            if (value == null)
                return null;
            return Convert.ToInt32(value);
        }
        private static long ConvertToLong(object value)
        {
            if (value == null)
                return 0;
            return Convert.ToInt64(value);
        }
        private static ulong ConvertToUInt64(object value)
        {
            if (value == null)
                return 0;
            return Convert.ToUInt64(value);
        }
        private static ulong? ConvertToUInt64Nullable(object value)
        {
            if (value == null)
                return 0;
            return Convert.ToUInt64(value);
        }
        private static decimal ConvertToDecimal(object value)
        {
            if (value == null)
                return 0;
            return Convert.ToDecimal(value);
        }
        public static DateTime ConvertToDateTime(object date)
        {
            if (date == null)
                return DateTime.MinValue;
            else if (string.IsNullOrEmpty(date.ToString()))
                return DateTime.MaxValue;
            return Convert.ToDateTime(date);
        }

        public static string DateFormat()
        {

            var culture = System.Globalization.CultureInfo.CurrentCulture;

            return culture.DateTimeFormat.ShortDatePattern;
        }
        public static string JqueryDateFormat()
        {
            string[] datePattern;
            if (DateFormat().Contains('/'))
            {
                datePattern = DateFormat().Split('/');
            }
            else
            {
                datePattern = DateFormat().Split('-');
            }

            string dateFormat = String.Empty;

            foreach (string s in datePattern)
            {
                dateFormat += s.Length == 2 ? s : (s.Length > 2 ? s.Substring(0, 2) : (s + s));
                dateFormat += "/";
            }

            return dateFormat.Trim('/').ToLower();
        }

        #endregion

        public static CurrentLogedInUser LoggedInUser
        {
            get
            {
                //var principal = System.Web.HttpContext.Current.User as AuthenticationWebPlatformPrincipal;
                //if (principal != null)
                //{
                //    return principal.UserData;
                //}
                var FormAuthTicket = ((System.Web.Security.FormsIdentity)(System.Web.HttpContext.Current.User.Identity)).Ticket;
                if (FormAuthTicket != null)
                    return CommonUtility.Deserialize<CurrentLogedInUser>(FormAuthTicket.UserData);
                return null;
            }
        }

        public static void DoLogin(CurrentLogedInUser LogedInUserEntity, HttpContextBase HttpContext)
        {
            var timeOut = 60 * 48; //48 hours
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, LogedInUserEntity.userid, DateTime.Now, DateTime.Now.AddMinutes(timeOut), false, CommonUtility.Serialize<CurrentLogedInUser>(LogedInUserEntity));
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Response.Cookies.Add(authCookie);
        }

        /// <summary>
        /// This Mehod returns a random string based on the parametes supplied
        /// Used Alphabets: ABCDEFGHJKLMNPQRSTUVWXYZ
        /// Used Numerics: 23456789
        /// Used Special Chars: $#@*^!
        /// </summary>
        /// <param name="StringLength">Length of the random string being generated</param>
        /// <param name="Alphabets">Number of alphabets required in the random string, for no alphabets pass NULL</param>
        /// <param name="Numerics">Number of numerics required in the random string, for no numerics pass NULL</param>
        /// <param name="SpecialChars">Number of special chars required in the random string, for no special chars pass NULL</param>
        /// <returns></returns>
        public static string GenerateRandomString(int StringLength, int? Alphabets, int? Numerics, int? SpecialChars)
        {
            var lstSpecialChar = "$#@*^!";
            var lstAlphabets = "ABCDEFGHJKLMNPQRSTUVWXYZ";
            var lstNumeric = "23456789";
            List<Char> characters = new List<Char>();
            StringBuilder output = new StringBuilder();
            int ExtraAlphabets = StringLength - ((Alphabets == null ? 0 : (int)Alphabets) + (Numerics == null ? 0 : (int)Numerics) + (SpecialChars == null ? 0 : (int)SpecialChars));
            try
            {
                Random rand = new Random();
                if (Alphabets != null)
                {
                    for (int i = 0; i < Alphabets + (ExtraAlphabets > 0 ? ExtraAlphabets : 0); i++) //Fill Extra length from Alphabets
                    {
                        characters.Add((lstAlphabets.ToArray<char>())[rand.Next(0, 24)]);
                    }
                }
                if (Numerics != null)
                {
                    for (int i = 0; i < Numerics; i++)
                    {
                        characters.Add((lstNumeric.ToArray<char>())[rand.Next(0, 8)]);
                    }
                }
                if (SpecialChars != null)
                {
                    for (int i = 0; i < SpecialChars; i++)
                    {
                        characters.Add((lstSpecialChar.ToArray<char>())[rand.Next(0, 6)]);
                    }
                }

                while (characters.Count != 0)
                {
                    int randPicker = rand.Next(0, characters.Count);
                    output.Append(characters[randPicker]);
                    characters.RemoveAt(randPicker);
                }
                return output.ToString(0, StringLength);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string AddBarcode(string emailBody, string barcodeText)
        {
            var rootPath = HttpContext.Current.Server.MapPath("~");
            //BarCode Generation --- Start
            if (!string.IsNullOrEmpty(barcodeText) && emailBody.Contains("$$BarCode$$"))
            {
                var barcode = new BarcodeLib.Barcode
                {
                    IncludeLabel = true
                };
                System.Drawing.Image img = barcode.Encode(BarcodeLib.TYPE.CODE128, barcodeText, Color.Black, Color.White, 200, 50);
                var barCodefile = rootPath + "/Barcode/" + barcodeText + ".png";
                barcode.SaveImage(barCodefile, BarcodeLib.SaveTypes.PNG);
                emailBody = emailBody.Replace("$$BarCode$$", barCodefile);
            }
            return emailBody;
            //BarCode Generation --- End
        }

       
        /// <summary>
        /// Get final Email Body for emailing 
        /// </summary>
        /// <typeparam name="T">type of entity to be used for replacing value</typeparam>
        /// <param name="emailTemplate">email template to be used</param>
        /// <param name="entity">entity to be used for replacing value</param>
        /// <returns>email body string</returns>
        public static string GetEmailBody<T>(emailtemplate emailTemplate, T entity)
        {
            string emailBody = emailTemplate.emailbody;
            if (entity != null)
            {
                Type type = entity.GetType();
                PropertyInfo[] properties = type.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    emailBody = emailBody.Replace(emailTemplate.bookmarkpre + property.Name + emailTemplate.bookmarksuf, Convert.ToString(property.GetValue(entity, null)));
                }
            }
            return emailBody;
        }

        public static string ConvertNumbertoWords(int number)
        {
            if (number == 0)
                return "ZERO";
            if (number < 0)
                return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000000) + " MILLION ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "")
                    words += "AND ";
                var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
                var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }

        public static RouteValueDictionary AnonymousObjectToHtmlAttributes(object htmlAttributes)
        {
            RouteValueDictionary result = new RouteValueDictionary();
            if (htmlAttributes != null)
            {
                foreach (System.ComponentModel.PropertyDescriptor property in System.ComponentModel.TypeDescriptor.GetProperties(htmlAttributes))
                {
                    result.Add(property.Name.Replace('_', '-'), property.GetValue(htmlAttributes));
                }
            }
            return result;
        }

        public static Dictionary<int, string> EnumToDictionary(Type type)
        {
            var instance = Convert.ChangeType(Activator.CreateInstance(type), type);
            var dic = new Dictionary<int, string>();
            foreach (var value in Enum.GetValues(type))
            {
                dic.Add((int)value, Enum.GetName(type, (int)value).Replace('_', ' '));
            }
            return dic;
        }
    }
}
