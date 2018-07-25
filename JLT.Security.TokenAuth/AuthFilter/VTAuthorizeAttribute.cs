using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using JLT.Common.Utility;
using System.Web;
using System.Net.Http;
using System.Net;
using MySql.Data.MySqlClient;

namespace JLT.Security.TokenAuth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class VTAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        public Enums.Action Actions;
        private string ErrMsg { get; set; }

        //public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        //{
        //    //Perform your logic here
        //    base.OnAuthorization(actionContext);
        //}

        /// <summary>
        /// Indicates whether the specified control is authorized
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //base.OnAuthorization(actionContext);
            try
            {
                if (actionContext.Request.Headers.Contains("Authorization"))
                {
                    var authToken = actionContext.Request.Headers.Authorization.Parameter;
                    //int usertype = 0;
                    //if (actionContext.Request.Headers.GetValues("UserType") != null && (!string.IsNullOrEmpty(Convert.ToString(actionContext.Request.Headers.GetValues("UserType").FirstOrDefault()))))
                    //{
                    //    usertype = Convert.ToInt32(actionContext.Request.Headers.GetValues("UserType").FirstOrDefault());
                    //}
                    if (string.IsNullOrEmpty(authToken))
                        throw new SecurityTokenException("401|Token has not been provided");
                    var IPAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
                    using (var objTokenService = new TokenService())
                    {
                        return objTokenService.validateAuthToken(authToken, IPAddress, true, Actions);
                    }
                }
                else
                {
                    ErrMsg = "403|User not authenticated, send to login screen for authentication";
                    return false;
                }
            }
            catch (SecurityTokenException e)
            {
                ErrMsg = e.Message;
                return false;
            }
            catch (MySqlException odbcEx)
            {
                ErrMsg = odbcEx.Message;
                return false;
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return false;
            }
        }

        /// <summary>
        /// Processes requests that fail authorization
        /// </summary>
        /// <param name="actionContext"></param>
        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
            ErrorHandler.LogError(new UnauthorizedAccessException(ErrMsg), ErrMsg, Enums.Severity.High);
            if (ErrMsg.Contains('|'))
            {
                var msg = ErrMsg.Split('|');
                if (Convert.ToInt32(msg[0]) == 403)
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden,
                                CommonUtility.Serialize<APIResponseBody>(new APIResponseBody
                                {
                                    type = "error:TokenAuthorization",
                                    message = msg[1],
                                    code = Convert.ToInt32(msg[0]),
                                    subcode = 0
                                }));
                    actionContext.Response.Headers.Add("WWW-Authenticate", "token");
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
                                CommonUtility.Serialize<APIResponseBody>(new APIResponseBody
                                {
                                    type = "error:TokenAuthorization",
                                    message = msg[1],
                                    code = Convert.ToInt32(msg[0]),
                                    subcode = 0
                                }));
                }
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                CommonUtility.Serialize<APIResponseBody>(new APIResponseBody
                                {
                                    type = "error:TokenAuthorization",
                                    message = ErrMsg,
                                    code = 501,
                                    subcode = 0
                                }));
            }
        }
    }
}
