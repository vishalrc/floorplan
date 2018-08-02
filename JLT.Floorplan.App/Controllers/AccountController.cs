using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using JLT.Floorplan.Entity;
using JLT.Floorplan.RestClient;
using JLT.Common.Utility;
using System.IO;


namespace JLT.Floorplan.Controllers
{
    //[Authorize]
    //[InitializeSimpleMembership]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(CurrentLogedInUser model, string returnUrl)
        {
            var header = new System.Collections.Specialized.NameValueCollection
            {
                { "IsAdmin", "true" }
            };
            using (var objApiClient = new RestAPIClient
            {
                requestUriString = CommonUtility.GetAppSettingKey("RestServiceURL") + "security/obtainAuthToken",
                contentType = "application/x-www-form-urlencoded",
                headerNameValueCollection = header,
                postDataString = CommonUtility.Serialize<CurrentLogedInUser>(model)
            })
            {
                var apiResponse = objApiClient.Post();
                if (!apiResponse.type.Contains("error") && apiResponse.body != null && apiResponse.code != -3)
                {
                    CommonUtility.DoLogin(CommonUtility.Deserialize<CurrentLogedInUser>(apiResponse.body.ToString()), this.HttpContext);
                }
                else
                {
                    ViewBag.Title = "Login";
                    ModelState.AddModelError("error_msg", apiResponse.message ?? "Login failed. Please enter valid userid and password");
                    if (apiResponse.code == 1 || apiResponse.code == 2)
                    {
                        ViewBag.Error = apiResponse.message;
                    }
                    else
                    {
                        ViewBag.Error = "Login failed, please try again";
                    }
                    return View(model);
                }
            }
            ViewBag.Error = "";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult MyProfile()
        {
            ViewBag.PageTitle = "My Profile";
            ViewBag.Title = "My Profile";
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult AccountSetting()
        {
            ViewBag.PageTitle = "Account Setting";
            ViewBag.Title = "Account Setting";
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult SaveImage(int Userid, string ImageData)
        {
            string fileName = "P_" + Userid + ".png";
            string fileNameWitPath = Path.Combine(Server.MapPath("~/img/"), fileName);

            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(ImageData);
                    bw.Write(data);
                    bw.Close();
                }
                fs.Close();
            }
            var json_string = @"{""success"": ""true""}";
            return Json(json_string, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Account/LogOff

        [HttpGet]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            ViewBag.Title = "Logout";
            var header = new System.Collections.Specialized.NameValueCollection
            {
                { "Authorization", "token " + CommonUtility.LoggedInUser.authtoken }
            };
            using (var objApiClient = new RestAPIClient
            {
                requestUriString = CommonUtility.GetAppSettingKey("RestServiceURL") + "security/revokeAuthToken",
                contentType = "application/x-www-form-urlencoded",
                headerNameValueCollection = header,
                postDataString = ""
            })
            {
                var apiResponse = objApiClient.Get();
                ViewBag.Title = apiResponse.message;
                FormsAuthentication.SignOut();
                // clear authentication cookie
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName)
                {
                    Expires = DateTime.Now.AddYears(-1)
                };
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public ActionResult Unauthorised()
        {
            if (TempData["unauth"] != null)
            {
                ViewBag.PageTitle = "Unauthorised Page";
                ViewBag.Title = "Unauthorised Page";
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
    }
}
