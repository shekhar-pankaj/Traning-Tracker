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
    }
}
