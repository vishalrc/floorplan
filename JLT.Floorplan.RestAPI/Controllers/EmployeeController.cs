using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JLT.Common.Utility;
using System.Web;
using JLT.Security.TokenAuth;
using MySql.Data.MySqlClient;
using JLT.Floorplan.Entity;
using JLT.Floorplan.DAL;

namespace JLT.RestAPI.Controllers
{
    public class EmployeeController : ApiController
    {
     
        [HttpPost]
        [ActionName("SaveExaminee")]
        //[VTAuthorizeAttribute(Actions = Enums.Action.General)]
        public HttpResponseMessage SaveExaminee(HttpRequestMessage request, [FromBody]string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    throw new SecurityTokenException("501|Server Error: HTTP request body not found");

                var AuthHeader = HttpContext.Current.Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(AuthHeader))
                    throw new SecurityTokenException("403:Authorization Error: Token not provided");
                var objEntity = CommonUtility.Deserialize<Employee>(value);
                using (var objDAL = new EmployeeDAL())
                {
                    var id = objDAL.SaveEmployee(objEntity);

                    return request.CreateResponse(HttpStatusCode.OK,
                            (new APIResponseBody
                            {
                                type = "success:" + request.RequestUri,
                                body = id.ToString(),
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

        
        [HttpPost]
        [ActionName("GetExaminee")]
        [VTAuthorizeAttribute(Actions = Enums.Action.General)]
        public HttpResponseMessage GetExaminee(HttpRequestMessage request, [FromBody]string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    throw new SecurityTokenException("501|Server Error: HTTP request body not found");

                var AuthHeader = HttpContext.Current.Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(AuthHeader))
                    throw new SecurityTokenException("403:Authorization Error: Token not provided");
                var objEntity = CommonUtility.Deserialize<Employee>(value);
                using (var objDAL = new EmployeeDAL())
                {
                    var lstEntity = objDAL.GetEmployee(objEntity);

                    return request.CreateResponse(HttpStatusCode.OK,
                            (new APIResponseBody
                            {
                                type = "success:" + request.RequestUri,
                                body = CommonUtility.Serialize<List<Employee>>(lstEntity),
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

              #region DummySyncAPIs

        [HttpGet]
        public HttpResponseMessage SyncLeave(HttpRequestMessage request)
        {
            var testlist = new List<seatvacancy>();
            //testlist.Add(new Test { name = "sync_test1", id = 200 });
            //testlist.Add(new Test { name = "sync_test2", id = 210 });
            //testlist.Add(new Test { name = "sync_test3", id = 220 });
            //testlist.Add(new Test { name = "sync_test4", id = 230 });
            //testlist.Add(new Test { name = "sync_test5", id = 240 });
            return request.CreateResponse(HttpStatusCode.OK, testlist);
        }
        [HttpGet]
        public HttpResponseMessage SyncEmployee(HttpRequestMessage request, string testids)
        {
            var lstTestIds = testids.Split(',');
            var Examineelist = new List<Employee>();
            for (int i = 0; i < lstTestIds.Count(); i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Examineelist.Add(new Employee
                    {
                        associateid = Convert.ToUInt64(lstTestIds[i]) + Convert.ToUInt64(j),
                        associateno = lstTestIds[i] + j.ToString(),
                        emailid = "email" + j.ToString() + "_" + lstTestIds[i] + "@test.com",
                        name = "Name" + j.ToString() + "_" + lstTestIds[i],
                    });
                }

            }
            return request.CreateResponse(HttpStatusCode.OK, Examineelist);
        }

        #endregion
    }
}
