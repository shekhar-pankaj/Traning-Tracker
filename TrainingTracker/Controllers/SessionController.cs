
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TrainingTracker.Authorize;
using TrainingTracker.BLL;
using TrainingTracker.Common.Entity;
using TrainingTracker.Common.Utility;

namespace TrainingTracker.Controllers
{
    /// <summary>
    /// Controller class for session
    /// </summary>
    [CustomAuthorizeAttribute]
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
        public ActionResult AddEditSession(Session sessionDetails)
        {
            return Json(new SessionBl().AddEditSessions(sessionDetails) , JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// Action method for upload session video file.
        /// </summary>
        /// <param name="fileName">Contain parameter fileName as HttpPostedFileBase object</param>
        /// <returns>Return filename as JSON object.</returns>
        [HttpPost]
        public ActionResult UploadVideo(HttpPostedFileBase fileName)
        {
            HttpPostedFileBase file = Request.Files["file"];
            string strFileName = string.Empty;
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    Guid gId;
                    gId = Guid.NewGuid();
                    strFileName = gId.ToString().Trim() + ".mp4";
                    var path = Path.Combine(Server.MapPath("~/Uploads/SessionVideo/"), strFileName);
                    file.SaveAs(path);
                }
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
                return null;
            }
            return Json(strFileName);

        }
    }
}