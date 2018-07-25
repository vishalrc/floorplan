using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JLT.Floorplan.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.PageTitle = "Dashboard";
            ViewBag.Title = "Dashboard";

            return View();
        }
    }
}
