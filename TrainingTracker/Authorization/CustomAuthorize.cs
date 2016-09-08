﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using TrainingTracker.Common.Constants;
using TrainingTracker.Common.Entity;

namespace TrainingTracker.Authorize
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Overrides AuthorizeCore method for custom authorization checks.
        /// </summary>
        /// <param name="httpContext"> The HTTP context, which encapsulates all HTTP-specific information about an individual HTTP request.</param>
        /// <returns>returns true if authorized else false</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            try
            {
                //bool isAuthorized = base.AuthorizeCore(httpContext);
                bool isAuthorized = false;
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(httpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value);

                if (httpContext.User.Identity.IsAuthenticated && authTicket != null)
                {
                    User currentUser = new JavaScriptSerializer().Deserialize<User>(authTicket.UserData);
                    
                    var userIdRequested = httpContext.Request.Params["userId"] != null ? Convert.ToInt16(httpContext.Request.Params["userId"]) : 0;

                    List<string> currentUserRoles = new List<string>();

                    if (currentUser.IsAdministrator)
                    {
                        currentUserRoles.Add(UserRoles.Administrator);
                    }
                    if (currentUser.IsManager)
                    {
                        currentUserRoles.Add(UserRoles.Manager);
                    }
                    if (currentUser.IsTrainee)
                    {
                        currentUserRoles.Add(UserRoles.Trainee);
                    }
                    if (currentUser.IsTrainer)
                    {
                        currentUserRoles.Add(UserRoles.Trainer);
                    }

                    GenericPrincipal userPrincipal =
                                     new GenericPrincipal(new GenericIdentity(authTicket.Name), currentUserRoles != null ? currentUserRoles.ToArray() : null);

                    isAuthorized = ((string.IsNullOrEmpty(this.Roles)) || (this.Roles.Split(',').ToList<string>().Any(x => userPrincipal.IsInRole(x))) || (currentUser.UserId.Equals(userIdRequested)));

                    httpContext.User = userPrincipal;

                }
                return isAuthorized;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Called when a process requests authorization.
        /// </summary>
        /// <param name="filterContext"> The filter context, which encapsulates information for using System.Web.Mvc.AuthorizeAttribute.</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                base.OnAuthorization(filterContext);

                if (SkipAuthorization(filterContext))
                {
                    return;
                }
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    filterContext.Result = new RedirectResult("~/Login/Login");
                    return;
                }
                if (filterContext.Result is HttpUnauthorizedResult)
                {
                    filterContext.Result = new RedirectResult("~/Unauthorized/UnauthorizedAccess");
                    return;
                }
            }
            catch
            {
                filterContext.Result = new RedirectResult("~/Login/Login");
                return;
            }
        }
        /// <summary>
        /// Checks if authorization need to be skipped.
        /// </summary>
        /// <param name="filterContext">The filter context, which encapsulates information for using System.Web.Mvc.AuthorizeAttribute.</param>
        /// <returns>Returns true if AllowAnonymous attribute is used to skip authorization else returns false</returns>
        private static bool SkipAuthorization(AuthorizationContext filterContext)
        {
            return filterContext != null ? filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any()
                   || filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any() : false;
        }
    }
}
