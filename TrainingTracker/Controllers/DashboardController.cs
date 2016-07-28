using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingTracker.BLL;

namespace TrainingTracker.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View("Dashboard");
        }

        public ActionResult GetDashboardData()
        {
            return Json(new DashboardBl().GetDashboardData(), JsonRequestBehavior.AllowGet);
        }
    }
}