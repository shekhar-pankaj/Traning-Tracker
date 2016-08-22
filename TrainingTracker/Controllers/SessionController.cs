
using System.Web.Mvc;
using TrainingTracker.BLL;
using TrainingTracker.Common.Entity;

namespace TrainingTracker.Controllers
{
    /// <summary>
    /// Controller class for session
    /// </summary>
    [Authorize]
    public class SessionController:Controller
    {
        /// <summary>
        /// Action method for Index
        /// </summary>
        /// <returns>View Results</returns>
        public ActionResult Index()
        {
            return View("Sessions");
        }

        /// <summary>
        /// Get Sessions for user on filter
        /// </summary>
        /// <returns>Json result</returns>
        public ActionResult GetUserFeedbackOnFilter( int pageSize , int seminarType , string searchKeyword = "" )
        {
            return Json(new SessionBl().GetSessionOnFilter(pageSize , seminarType , searchKeyword) , JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        ///Add or Edit Session
        /// </summary>
        /// <returns>Json result</returns>
        public ActionResult AddEditSession( Session sessionDetails  )
        {
            return Json(new SessionBl().AddEditSessions(sessionDetails) , JsonRequestBehavior.AllowGet);

        }
    }
}