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

namespace com.JLT.RestAPI.Controllers
{
    public class SecurityController : ApiController
    {
        [HttpGet]
        [ActionName("TestAPI")]
        public HttpResponseMessage TestAPI(HttpRequestMessage request)
        {
            string userType = "None";
            if (HttpContext.Current.Request.Headers["UserType"] != null)
                userType = HttpContext.Current.Request.Headers["UserType"];

            return request.CreateResponse(HttpStatusCode.OK,
                           (new APIResponseBody
                           {
                               type = "success:TestAPI",
                               body = "UserType: " + userType,
                               message = "API Working",
                               code = 1,
                               subcode = 0
                           }));
        }

        [HttpPost]
        [ActionName("obtainAuthToken")]
        public HttpResponseMessage obtainAuthToken(HttpRequestMessage request, [FromBody]string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    throw new SecurityTokenException("501|Server Error: HTTP request body not found");

                int userType = 0;
                if (HttpContext.Current.Request.Headers["UserType"] != null)
                    userType = Convert.ToInt16(HttpContext.Current.Request.Headers["UserType"]);

                var IPAddress = HttpContext.Current.Request.UserHostAddress;
                //Code to authenticate
                using (var objTokenService = new TokenService())
                {
                    var objUserEntity = objTokenService.obtainAuthToken(CommonUtility.Deserialize<CurrentLogedInUser>(value), IPAddress.ToString());
                    return request.CreateResponse(HttpStatusCode.OK,
                            (new APIResponseBody
                            {
                                type = "success:TokenAuthentication",
                                body = CommonUtility.Serialize<CurrentLogedInUser>(objUserEntity),
                                message = "",
                                code = 0,
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
                    case 30001:
                        httpResMsg = request.CreateResponse(HttpStatusCode.Forbidden,
                            (new APIResponseBody
                            {
                                type = "error:TokenAuthentication",
                                body = "",
                                message = "Error granting access token: Account has been locked try after 30 minutes",
                                code = 1,
                                subcode = 0
                            }));//Locked try after 30 min
                        break;
                    case 30002:
                        httpResMsg = request.CreateResponse(HttpStatusCode.Forbidden,
                            (new APIResponseBody
                            {
                                type = "error:TokenAuthentication",
                                body = "",
                                message = "Error granting access token: You have tried 2 or more wrong attempt, please enter image text(CAPTCHA) in the box",
                                code = 2,
                                subcode = 0
                            }));//Suspicious attempt Show Captch 
                        break;
                    case 30003:
                        httpResMsg = request.CreateResponse(HttpStatusCode.NotFound,
                            (new APIResponseBody
                            {
                                type = "error:TokenAuthentication",
                                body = "",
                                message = "Error granting access token: You entered wrong UserId or Password",
                                code = 1,
                                subcode = 0
                            }));//Wrong credentials
                        break;
                    case 30004:
                        httpResMsg = request.CreateResponse(HttpStatusCode.Unauthorized,
                            (new APIResponseBody
                            {
                                type = "error:TokenAuthentication",
                                body = "",
                                message = "Error granting access token: User does not have any role assigned",
                                code = 1,
                                subcode = 0
                            }));//User does not have any role assigned
                        break;
                    case 30005:
                        httpResMsg = request.CreateResponse(HttpStatusCode.Forbidden,
                            (new APIResponseBody
                            {
                                type = "error:TokenAuthentication",
                                body = "",
                                message = "Error granting access token: Account locked, 5 unsuccessfull attemp",
                                code = 1,
                                subcode = 0
                            }));//Account Locked
                        break;
                    case 30006:
                        httpResMsg = request.CreateResponse(HttpStatusCode.Forbidden,
                            (new APIResponseBody
                            {
                                type = "error:TokenAuthentication",
                                body = "",
                                message = "Error granting access token:  " + Constants.SQLCustomError.e_30006, //"Account has been disabled, contact administrator"
                                code = 1,
                                subcode = 0
                            }));//Account Locked
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
                            }));//Account Locked
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
                    httpResMsg = request.CreateResponse(HttpStatusCode.Unauthorized,
                                (new APIResponseBody
                                {
                                    type = "error:TokenAuthentication",
                                    body = "",
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
                                    type = "error:TokenAuthentication",
                                    body = "",
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
                if (ex.Message.Contains('|'))
                {
                    var msg = ex.Message.Split('|');
                    httpResMsg = request.CreateResponse(HttpStatusCode.InternalServerError,
                                (new APIResponseBody
                                {
                                    type = "error:TokenAuthentication",
                                    body = "",
                                    message = "Error granting access token: " + msg[1],
                                    code = Convert.ToInt32(msg[0]),
                                    subcode = 0
                                }));
                }
                else
                {
                    httpResMsg = request.CreateResponse(HttpStatusCode.Unauthorized,
                                (new APIResponseBody
                                {
                                    type = "error:TokenAuthentication",
                                    body = "",
                                    message = "Error granting access token: " + ex.Message,
                                    code = 401,
                                    subcode = 0
                                }));
                }
                return httpResMsg;
            }
            finally
            {

            }

        }

        [HttpGet]
        [ActionName("validateToken")]
        public HttpResponseMessage validateToken(HttpRequestMessage request)
        {
            string token = string.Empty;
            string IPAddress = string.Empty;
            try
            {
                var AuthHeader = HttpContext.Current.Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(AuthHeader))
                    throw new SecurityTokenException("403:Authorization Error: Token not provided");

                token = com.JLT.Common.Utility.StringUtility.SplitString(AuthHeader, " ")[1];
                IPAddress = HttpContext.Current.Request.UserHostAddress;
                bool IsValid = false;
                using (var objTokenService = new TokenService())
                {
                    IsValid = objTokenService.validateAuthToken(token, IPAddress, false, 0);
                }
                if (IsValid)
                {
                    return request.CreateResponse(HttpStatusCode.OK,
                            (new APIResponseBody
                            {
                                type = "success:TokenAuthorization",
                                body = "true",
                                message = "Authorization Successfull",
                                code = 1,
                                subcode = 0
                            }));
                }
                else
                {
                    ErrorHandler.LogError(new UnauthorizedAccessException("Token now valid"), 
                        "Token autorization failed. (Token: " + token + " | IP Address: " + IPAddress + ")", Enums.Severity.High);
                    return request.CreateResponse(HttpStatusCode.Unauthorized,
                            (new APIResponseBody
                            {
                                type = "error:TokenAuthorization",
                                body = "false",
                                message = "Authorization failed",
                                code = 1,
                                subcode = 0
                            }));
                }
            }
            catch (SecurityTokenException e)
            {
                ErrorHandler.LogError(e, e.Message + "(Token: " + token + " | IP Address: " + IPAddress + ")", Enums.Severity.High);
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
                                       type = "error:TokenAuthorization",
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
                                        type = "error:TokenAuthorization",
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
                                        type = "error:TokenAuthorization",
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
                ErrorHandler.LogError(ex, ex.Message + "(Token: " + token + " | IP Address: " + IPAddress + ")", Enums.Severity.High);
                HttpResponseMessage httpResMsg = new HttpResponseMessage();
                httpResMsg = request.CreateResponse(HttpStatusCode.InternalServerError,
                            (new APIResponseBody
                            {
                                type = "error:TokenAuthorization",
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

        [HttpGet]
        [ActionName("revokeAuthToken")]
        public HttpResponseMessage revokeAuthToken(HttpRequestMessage request)
        {
            string token = string.Empty;
            string IPAddress = string.Empty;
            try
            {
                var AuthHeader = HttpContext.Current.Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(AuthHeader))
                    throw new SecurityTokenException("403:Authorization Error: Token not provided");
                token = com.JLT.Common.Utility.StringUtility.SplitString(AuthHeader, " ")[1];
                IPAddress = HttpContext.Current.Request.UserHostAddress;
                bool IsValid = false;
                using (var objTokenService = new TokenService())
                {
                    IsValid = objTokenService.revokeAuthToken(token);
                }
                if (IsValid)
                {
                    return request.CreateResponse(HttpStatusCode.OK,
                            (new APIResponseBody
                            {
                                type = "success:RevokeToken",
                                body = "true",
                                message = "Token revoked",
                                code = 1,
                                subcode = 0
                            }));
                }
                else
                {
                    return request.CreateResponse(HttpStatusCode.InternalServerError,
                            (new APIResponseBody
                            {
                                type = "error:RevokeToken",
                                body = "false",
                                message = "Token revoke failed",
                                code = 1,
                                subcode = 0
                            }));
                }
            }
            catch (SecurityTokenException e)
            {
                ErrorHandler.LogError(e, e.Message + "(Token: " + token + " | IP Address: " + IPAddress + ")", Enums.Severity.High);
                if (e.Message.Contains('|'))
                {
                    var msg = e.Message.Split('|');
                    return request.CreateResponse(HttpStatusCode.InternalServerError,
                                (new APIResponseBody
                                {
                                    type = "error:RevokeToken",
                                    body = "false",
                                    message = msg[1],
                                    code = Convert.ToInt32(msg[0]),
                                    subcode = 0
                                }));
                }
                else
                {
                    return request.CreateResponse(HttpStatusCode.InternalServerError,
                                (new APIResponseBody
                                {
                                    type = "error:RevokeToken",
                                    body = "false",
                                    message = e.Message,
                                    code = 501,
                                    subcode = 0
                                }));
                }
            }
            catch (Exception e)
            {
                ErrorHandler.LogError(e, e.Message + "(Token: " + token + " | IP Address: " + IPAddress + ")", Enums.Severity.High);
                return request.CreateResponse(HttpStatusCode.InternalServerError,
                            (new APIResponseBody
                            {
                                type = "error:RevokeToken",
                                body = "false",
                                message = e.Message,
                                code = 501,
                                subcode = 0
                            }));
            }
        }

    }
}
