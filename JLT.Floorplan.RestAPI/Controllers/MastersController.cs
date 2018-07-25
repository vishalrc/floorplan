using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using com.JLT.Common.Utility;
using System.Web;
using com.JLT.Security.TokenAuth;
using MySql.Data.MySqlClient;
using com.JLT.Entity;
using com.JLT.DAL;

namespace com.JLT.RestAPI.Controllers
{
    public class MastersController : ApiController
    {
        [HttpPost]
        [ActionName("GetCountry")]
        [VTAuthorizeAttribute(Actions = Enums.Action.General)]
        public HttpResponseMessage GetCountry(HttpRequestMessage request, [FromBody]string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    throw new SecurityTokenException("501|Server Error: HTTP request body not found");

                var AuthHeader = HttpContext.Current.Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(AuthHeader))
                    throw new SecurityTokenException("403:Authorization Error: Token not provided");
                var objEntity = CommonUtility.Deserialize<Country>(value);
                using (var objDAL = new MastersDAL())
                {
                    var lstEntity = objDAL.GetCountry(objEntity);

                    return request.CreateResponse(HttpStatusCode.OK,
                            (new APIResponseBody
                            {
                                type = "success:" + request.RequestUri,
                                body = CommonUtility.Serialize<List<Country>>(lstEntity),
                                message = "success",
                                code = 1,
                                subcode = 0
                            }));
                }
            }
            catch (MySqlException odbcEx)
            {
                ErrorHandler.LogError(odbcEx, odbcEx.Message, Enums.Severity.High);
                HttpResponseMessage httpResMsg = new HttpResponseMessage();
                switch (odbcEx.Number)
                {
                    case 30014:
                        httpResMsg = request.CreateResponse(HttpStatusCode.Forbidden,
                            (new APIResponseBody
                            {
                                type = "error:" + request.RequestUri,
                                body = "",
                                message = odbcEx.Message,
                                code = odbcEx.Number,
                                subcode = 0
                            }));
                        break;
                    default:
                        httpResMsg = request.CreateResponse(HttpStatusCode.InternalServerError,
                            (new APIResponseBody
                            {
                                type = "error:SystemError",
                                body = "",
                                message = "Data Access error: Exeption found in executing database query",
                                code = -2,
                                subcode = 0
                            }));
                        break;
                }
                return httpResMsg;
            }
            catch (SecurityTokenException e)
            {
                ErrorHandler.LogError(e, e.Message, Enums.Severity.High);
                HttpResponseMessage httpResMsg = new HttpResponseMessage();
                if (e.Message.Contains('|'))
                {
                    var msg = e.Message.Split('|');
                    if (Convert.ToInt32(msg[0]) == 403)
                    {
                        HttpContext.Current.Response.AddHeader("WWW-Authenticate", "token");
                        httpResMsg = request.CreateResponse(HttpStatusCode.Forbidden,
                                   (new APIResponseBody
                                   {
                                       type = "error:" + request.RequestUri,
                                       body = "false",
                                       message = msg[1],
                                       code = Convert.ToInt32(msg[0]),
                                       subcode = 0
                                   }));
                    }
                    else
                    {
                        httpResMsg = request.CreateResponse(HttpStatusCode.Unauthorized,
                                    (new APIResponseBody
                                    {
                                        type = "error:" + request.RequestUri,
                                        body = "false",
                                        message = msg[1],
                                        code = Convert.ToInt32(msg[0]),
                                        subcode = 0
                                    }));
                    }
                }
                else
                {
                    httpResMsg = request.CreateResponse(HttpStatusCode.Unauthorized,
                                    (new APIResponseBody
                                    {
                                        type = "error:" + request.RequestUri,
                                        body = "false",
                                        message = e.Message,
                                        code = 401,
                                        subcode = 0
                                    }));
                }
                return httpResMsg;
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, ex.Message, Enums.Severity.High);
                HttpResponseMessage httpResMsg = new HttpResponseMessage();
                httpResMsg = request.CreateResponse(HttpStatusCode.InternalServerError,
                            (new APIResponseBody
                            {
                                type = "error:" + request.RequestUri,
                                body = "false",
                                message = ex.Message,
                                code = 501,
                                subcode = 0
                            }));
                return httpResMsg;
            }
            finally
            {

            }
        }
        
        
    }
}
