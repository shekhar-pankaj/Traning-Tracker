using System.Collections.Generic;
using TrainingTracker.Common.Entity;

namespace TrainingTracker.DAL.Interface
{
    /// <summary>
    /// Interface for Session Dal
    /// </summary>
   public interface ISessionDal
    {
        /// <summary>
        /// Inteface method to fetch sessions by User id
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>List Of session</returns>
        List<Session> GetSessionsByUserId(int userId);

        /// <summary>
        /// Inteface method to fetch sessions by User id
        /// </summary>
        /// <param name="objSession">User Id</param>
        /// <returns>List Of session</returns>
       bool AddEditSessions( Session objSession );

       List<Session> GetSessionOnFilter(int pageSize ,int sessionType ,string searchKeyword); 
    }
}
