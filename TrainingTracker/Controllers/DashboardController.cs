using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingTracker.BLL;
using TrainingTracker.Common.Constants;
using TrainingTracker.Authorize;
using TrainingTracker.Common.Entity;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace TrainingTracker.Controllers
{
    [CustomAuthorize]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            try
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                User currentUser = new JavaScriptSerializer().Deserialize<User>(authTicket.UserData);

                if (HttpContext.User.IsInRole(UserRoles.Administrator) || HttpContext.User.IsInRole(UserRoles.Manager) || HttpContext.User.IsInRole(UserRoles.Trainer))
                {
                    return View("Dashboard");
                }
                return RedirectToAction("UserProfile", "Profile", new { userId = currentUser.UserId });
            }
            catch
            {
                return View("Dashboard");
            }
        }

        [CustomAuthorize(Roles = UserRoles.Administrator + "," + UserRoles.Manager + "," + UserRoles.Trainer)]
        public ActionResult GetDashboardData()
        {
            User currentUser = new UserBl().GetUserByUserName(User.Identity.Name);
            return Json(new DashboardBl().GetDashboardData(currentUser) , JsonRequestBehavior.AllowGet);
        }

       
    }
}