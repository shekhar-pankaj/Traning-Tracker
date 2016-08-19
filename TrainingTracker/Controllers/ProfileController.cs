using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TrainingTracker.BLL;
using TrainingTracker.Common.Entity;

namespace TrainingTracker.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        // GET: UserProfile?userId=
        public ActionResult UserProfile(int userId)
        {
            return View("Profile");
        }
        public ActionResult ManageProfile()
        {
            return PartialView("_PartialProfile");
        }

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

        public ActionResult GetAllUsers()
        {
            return Json(new UserBl().GetAllUsers(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUserProfileVm(int userId)
        {
            return Json(new UserBl().GetUserProfileVm(userId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserFeedbackOnFilter(int pageSize, int feedbackId,int userId)
        {
            return Json(new UserBl().GetUserFeedbackOnFilter(userId, pageSize, feedbackId), JsonRequestBehavior.AllowGet);
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
    }
}