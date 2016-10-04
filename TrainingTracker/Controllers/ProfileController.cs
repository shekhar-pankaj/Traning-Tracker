using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TrainingTracker.Authorize;
using TrainingTracker.BLL;
using TrainingTracker.Common.Constants;
using TrainingTracker.Common.Entity;

namespace TrainingTracker.Controllers
{
    [CustomAuthorizeAttribute]
    public class ProfileController : Controller
    {
        // GET: UserProfile?userId=
        [CustomAuthorize(Roles = UserRoles.Administrator+","+UserRoles.Manager+","+UserRoles.Trainer)]
        public ActionResult UserProfile(int userId)
        {
            return View("Profile");
        }
        /// <summary>
        /// Manage users profile.
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageProfile()
        {
            return PartialView("_PartialProfile");
        }
        /// <summary>
        /// Shows all the profiles.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = UserRoles.Administrator + "," + UserRoles.Manager + "," + UserRoles.Trainer)]
        public ActionResult AllProfiles()
        {
            return View("AllProfiles");
        }

        public ActionResult AddEditProfile()
        {
            return View();
        }
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles = UserRoles.Administrator + "," + UserRoles.Manager + "," + UserRoles.Trainer)]
        public ActionResult CreateUser(User userData)
        {
            int userId;
            bool status = false;
            status = new UserBl().AddUser(userData , out userId);
            var data = new
            {
                userId = userId ,
                status = status
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Updates the existing user.
        /// </summary>
        /// <param name="userData">Input user object</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateUser(User userData)
        {            
            return Json(new { status= new UserBl().UpdateUser(userData)});
        }

        [HttpPost]
        public ActionResult AddFeedback(Feedback feedbackPost)
        {
            return Json(new FeedbackBl().AddFeedback(feedbackPost) ? "true" : "false");
        }

        [CustomAuthorize(Roles = UserRoles.Administrator + "," + UserRoles.Manager + "," + UserRoles.Trainer)]
        public ActionResult GetAllUsers()
        {
            return Json(new UserBl().GetAllUsers(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// ActionMethod for GetActiveUsers
        /// </summary>
        /// <returns> Returns list of active user as json object.</returns>
        [HttpGet]
        public ActionResult GetActiveUsers()
        {
            return Json(new UserBl().GetActiveUsers(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Action to return user profile View model
        /// </summary>
        /// <param name="userId">user id of logged user</param>
        /// <returns>Json Result for loggin User</returns>
        public ActionResult GetUserProfileVm(int userId)
        {
            return Json(new UserBl().GetUserProfileVm(userId), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Action method to handle xhr request for fetching filters based on filter  condition
        /// </summary>
        /// <param name="pageSize">no of records to return</param>
        /// <param name="feedbackId">type of feedback</param>
        /// <param name="userId">user if for feedback to be fetched</param>
        /// <param name="startDate">start date range</param>
        /// <param name="endDate">end date range</param>
        /// <returns></returns>
        public JsonResult GetUserFeedbackOnFilter(int pageSize, int feedbackId, int userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            return Json(new UserBl().GetUserFeedbackOnFilter(userId , pageSize , feedbackId , startDate , endDate) , JsonRequestBehavior.AllowGet);
        }

        //Added for Upload Image 
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase fileName)
        {
            HttpPostedFileBase file = Request.Files["file"];
            string strFileName = string.Empty;
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    Guid gId;
                    gId = Guid.NewGuid();
                    strFileName = gId.ToString().Trim() + ".jpg";
                    var path = Path.Combine(Server.MapPath("~/Uploads/ProfilePicture/"), strFileName);
                    file.SaveAs(path);
                }
            }
            catch (Exception)
            {

            }
            return Json(strFileName);
        }

        /// <summary>
        /// Method to handle xhr request for plot service.
        /// </summary>
        /// <param name="traineeId">trainee's id</param>
        /// <param name="startDate">start date</param>
        /// <param name="endDate">end date</param>
        /// <param name="arrayFeedbackType">array of feedback type</param>
        /// <param name="trainerId">trainer id</param>
        /// <returns>returns json results, contains feedback based on applied filters</returns>
        public JsonResult GetUserFeedbackOnFilterForPlot(int traineeId, DateTime? startDate, DateTime? endDate,
                                                         string arrayFeedbackType, int trainerId)
        {
            return Json(new UserBl().GetUserFeedbackOnFilterForPlot(traineeId , startDate , endDate , arrayFeedbackType , trainerId) , JsonRequestBehavior.AllowGet);
        }
    }
}