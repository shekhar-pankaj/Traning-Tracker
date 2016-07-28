using TrainingTracker.Common.Entity;
using TrainingTracker.DAL.DataAccess;

namespace TrainingTracker.BL
{
    public class LoginBl
    {
        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <returns>User data with validation information.</returns>
        public User AuthenticateUser(string userName, string password)
        {
            var userData = new User
            {
                UserName = userName,
                Password = password
            };
            userData.IsValid = new UserDal().ValidateUser(userData);
            return userData;
        }
    }
}