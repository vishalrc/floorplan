using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JLT.Floorplan.Controllers
{
    public class FloorplanController : Controller
    {
        //
        // GET: /Floorplan/

        public ActionResult Index()
        {
            ViewBag.PageTitle = "Manage Seat Plan";
            ViewBag.Title = "Manage Seat Plan";
            return View();
        }

    }
}
