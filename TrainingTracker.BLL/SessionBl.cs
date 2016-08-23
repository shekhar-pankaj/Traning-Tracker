using TrainingTracker.BLL.Base;
using TrainingTracker.Common.Entity;
using TrainingTracker.Common.ViewModel;

namespace TrainingTracker.BLL
{
    /// <summary>
    /// Bussiness class for session
    /// </summary>
    public class SessionBl:BussinessBase
    {
        /// <summary>
        /// method to Add or edit session details
        /// </summary>
        /// <param name="objSession">instance of session Object</param>
        /// <returns>boolean resutt of event's success</returns>
        public bool AddEditSessions(Session objSession)
        {
            return SessionDataAccesor.AddEditSessions(objSession);
        }

        /// <summary>
        /// Get session based on filter conditions
        /// </summary>
        /// <param name="pageSize">number of results to be fetched</param>
        /// <param name="sessionType">type of session All/Presented/Scheduled</param>
        /// <param name="searchKeyword">any keyword term</param>
        /// <returns>instance of session method</returns>
        public SessionVm GetSessionOnFilter( int pageSize , int sessionType , string searchKeyword )
        {
            SessionVm objSessionVm =new SessionVm
            {
                SessionList = SessionDataAccesor.GetSessionOnFilter(pageSize, sessionType, searchKeyword),
                AllAttendees = UserDataAccesor.GetAllUsers()
            };
            return objSessionVm;
        }
    }
}
