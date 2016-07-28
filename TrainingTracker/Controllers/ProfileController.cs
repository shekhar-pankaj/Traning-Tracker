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

        public ActionResult AllProfiles()
        {
            return View("AllProfiles");
        }

        public ActionResult AddEditProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(User userData)
        {
            return Json(new UserBl().AddUser(userData) ? "true" : "false");
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

    }
}