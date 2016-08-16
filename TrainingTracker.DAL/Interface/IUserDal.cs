using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingTracker.Common.Entity;
using TrainingTracker.Common.ViewModel;

namespace TrainingTracker.DAL.Interface
{
    /// <summary>
    /// Interface for User Dal
    /// </summary>
    public interface IUserDal
    {
        /// <summary>
        /// Calls stored procedure which validates user.
        /// </summary>
        /// <param name="userData">User data object.</param>
        /// <returns>True if valid customer.</returns>
        bool ValidateUser(User userData);

        /// <summary>
        /// Calls stored procedure which adds user.
        /// </summary>
        /// <param name="userData">User data object.</param>
        /// <param name="iUserId">Out parameter created UserId.</param>
        /// <returns>True if added.</returns>
        bool AddUser(User userData,out long iUserId);

        /// <summary>
        /// Calls stored procedure which updates user.
        /// </summary>
        /// <param name="objUser">User data object.</param>
        /// <returns>True if updated.</returns>
        bool UpdateUser(User objUser);

        /// <summary>
        /// Gets all Users.
        /// </summary>
        /// <returns>List of all users.</returns>
        List<User> GetAllUsers();

        /// <summary>
        /// Interface method to Fetch user By its name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        User GetUserByUserName(string userName);

        /// <summary>
        /// Fetch User by its Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        User GetUserById(int userId);

        /// <summary>
        /// Get All Dashboard Data
        /// </summary>
        /// <returns>instance Of List of UserData</returns>
        List<UserData> GetDashboardData();

    }
}
