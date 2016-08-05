using System.Web.Mvc;
using System.Web.Security;
using TrainingTracker.BLL;

namespace TrainingTracker.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View("Login");
        }

        /// <summary>
        /// Redirects to Manage license view.
        /// </summary>
        /// <returns></returns>
        public ActionResult Valid()
        {
            return RedirectToAction( "Index","Dashboard");
        }

        public ActionResult AuthenticateLogin(string userName, string password)
        {
            var userData = new LoginBl().AuthenticateUser(userName, password);
            if (userData.IsValid)
            {
                FormsAuthentication.SetAuthCookie(userName, true);
            }
            return Json(userData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCurrentUser()
        {
            return Json(new UserBl().GetUserByUserName(User.Identity.Name), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Sign outs the user.
        /// </summary>
        /// <returns>To login page.</returns>
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}