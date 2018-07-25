using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JLT.Common.Utility;

namespace JLT.eProctor.Controllers
{
    public class ManageController : Controller
    {

        [Authorize]
        public ActionResult Users()
        {
            if ((CommonUtility.LoggedInUser.role & Convert.ToUInt64(Enums.UserRoles.Admin)) != Convert.ToUInt64(Enums.UserRoles.Admin))
            {
                TempData["unauth"] = true;
                return RedirectToAction("Unauthorised", "Account");
            }
            ViewBag.PageTitle = "Manage User";
            ViewBag.Title = "Manage User";
            return View();
        }

        [Authorize]
        public ActionResult Leave()
        {
            if ((CommonUtility.LoggedInUser.role & Convert.ToUInt64(Enums.UserRoles.Admin)) != Convert.ToUInt64(Enums.UserRoles.Admin))
            {
                TempData["unauth"] = true;
                return RedirectToAction("Unauthorised", "Account");
            }
            ViewBag.PageTitle = "Sync Leave";
            ViewBag.Title = "Sync Leave";
            return View();
        }

        [Authorize]
        public ActionResult Employee()
        {
            if ((CommonUtility.LoggedInUser.role & Convert.ToUInt64(Enums.UserRoles.Admin)) != Convert.ToUInt64(Enums.UserRoles.Admin))
            {
                TempData["unauth"] = true;
                return RedirectToAction("Unauthorised", "Account");
            }
            ViewBag.PageTitle = "Sync Employee";
            ViewBag.Title = "Sync Employee";
            return View();
        }

        [Authorize]
        public ActionResult AssignSeat()
        {
            if ((CommonUtility.LoggedInUser.role & Convert.ToUInt64(Enums.UserRoles.Admin)) != Convert.ToUInt64(Enums.UserRoles.Admin))
            {
                TempData["unauth"] = true;
                return RedirectToAction("Unauthorised", "Account");
            }
            ViewBag.PageTitle = "Assign Seat";
            ViewBag.Title = "Assign Seat";
            return View();
        }

    }
}
