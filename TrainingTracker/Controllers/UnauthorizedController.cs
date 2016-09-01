using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrainingTracker.Controllers
{
    public class UnauthorizedController : Controller
    {
        // GET: Unauthorized
        public ActionResult UnauthorizedAccess()
        {
            return View("UnauthorizedAccess");
        }
    }
}