﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI.WebControls;
using TrainingTracker.Authorize;
using TrainingTracker.BLL;
using TrainingTracker.Common.Entity;

namespace TrainingTracker.Controllers
{

    [CustomAuthorizeAttribute]
    public class LoginController : Controller
    {
        /// <summary>
        /// Default Action result to Login Action
        /// </summary>
        /// <returns>redirect to Login</returns>
        public ActionResult Index()
        {
          return RedirectToAction("Login");
        }

        /// <summary>
        /// Http get method for Default login controller method
        /// </summary>
        /// <returns></returns>

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login( LoginModel objLoginModel )
        {
            if (!ModelState.IsValid) return View("Login");

            var userData = new LoginBl().AuthenticateUser(objLoginModel.UserName , Common.Encryption.Cryptography.Encrypt(objLoginModel.Password));
            
            if (userData.IsValid)
            {
                string currentUser = new JavaScriptSerializer().Serialize(new UserBl().GetUserByUserName(userData.UserName));

                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                  1,
                  userData.UserName.ToString(),
                  DateTime.Now,
                  DateTime.Now.AddDays(1),
                  false,
                  currentUser,
                  "/");
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                Response.Cookies.Add(cookie);
                //FormsAuthentication.SetAuthCookie(objLoginModel.UserName , true);
                return RedirectToAction("Index" , "Dashboard");
            }
            ModelState.AddModelError("","Login Failed,Invalid Credentials");
            return View("Login");
        }

        /// <summary>
        /// Redirects to Manage license view.
        /// </summary>
        /// <returns></returns>
        public ActionResult Valid()
        {
            return RedirectToAction("Index" , "Dashboard");
        }

        //public ActionResult AuthenticateLogin(string userName, string password)
        //{
        //    var userData = new LoginBl().AuthenticateUser(userName, password);
        //    if (userData.IsValid)
        //    {
        //        FormsAuthentication.SetAuthCookie(userName, true);
        //    }
        //    return Json(userData, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetCurrentUser()
        {
            return Json(new UserBl().GetUserByUserName(User.Identity.Name) , JsonRequestBehavior.AllowGet);
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