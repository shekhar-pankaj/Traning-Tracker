using System;
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
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            try
            {
                bool isAuthorized = base.AuthorizeCore(httpContext);

                if (!isAuthorized && httpContext.User.Identity.IsAuthenticated)
                {
                    HttpCookie authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

                    if (authCookie != null)
                    {
                        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                        User currentUser = new JavaScriptSerializer().Deserialize<User>(authTicket.UserData);

                        List<string> authorizedRoles = this.Roles.Split(',').ToList<string>();

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

                        isAuthorized = ((authorizedRoles.Any(x => userPrincipal.IsInRole(x))) || (currentUser.UserId.Equals(userIdRequested)));

                        httpContext.User = userPrincipal;
                    }
                }
                return isAuthorized;
            }
            catch
            {
                return false;
            }
        }
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
            catch {
                filterContext.Result = new RedirectResult("~/Login/Login");
                return;
            }
        }
        private static bool SkipAuthorization(AuthorizationContext filterContext)
        {
            return filterContext != null ?filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any()
                   || filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any():false;
        }
    }
}
