using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using com.JLT.Common.Utility;

namespace com.JLT.RestClient
{
    public class RestAPIClient : IDisposable
    {
        public string requestUriString { get; set; }
        private string _postDataString;
        public string postDataString { get { return _postDataString; } set { _postDataString = "=" + value; } }
        public string contentType { get; set; }
        public System.Collections.Specialized.NameValueCollection headerNameValueCollection { get; set; }

        public APIResponseBody Get()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUriString);
            request.Method = "GET";
            request.Headers.Add("Accept-Encoding", "gzip, deflate, sdch");
            request.Headers.Add("Accept-Language", "en-US,en;q=0.8");
            if (headerNameValueCollection != null && headerNameValueCollection.Count > 0)
                request.Headers.Add(headerNameValueCollection);
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
            catch (Exception e)
            {
                throw e;
            }
            return responseResult;
        }
        public APIResponseBody Post()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUriString);
            request.Method = "POST";
            request.ContentType = contentType;// "application/x-www-form-urlencoded";//"application/json";
            request.ContentLength = postDataString.Length;
            if (headerNameValueCollection != null && headerNameValueCollection.Count > 0)
                request.Headers.Add(headerNameValueCollection);
            var responseResult = new APIResponseBody();
            var errorResponseResult = new APIClientResponseBody();

            using (Stream webStream = request.GetRequestStream())
            using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
            {
                requestWriter.Write(postDataString);
            }

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
                            errorResponseResult = CommonUtility.Deserialize<APIClientResponseBody>(response);
                            if (errorResponseResult.Message == null)
                                return CommonUtility.Deserialize<APIResponseBody>(response);
                            else
                                return CommonUtility.Deserialize<APIResponseBody>(errorResponseResult.Message);
                        }
                    }
                }
                //return CommonUtility.Deserialize<APIResponseBody>(errorResponseResult.Message);
            }
            catch (Exception e)
            {
                throw e;
            }
            return responseResult;
        }

        public void Dispose() { }
    }
}
