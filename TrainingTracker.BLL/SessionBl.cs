using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public bool AddEditSessions(Session objSession)
        {
            return SessionDataAccesor.AddEditSessions(objSession);
        }

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
