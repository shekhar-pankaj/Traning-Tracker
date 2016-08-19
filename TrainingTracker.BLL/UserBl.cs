using System.Collections.Generic;
using System.Linq;
using TrainingTracker.BLL.Base;
using TrainingTracker.Common.Entity;
using TrainingTracker.Common.ViewModel;

namespace TrainingTracker.BLL
{
    /// <summary>
    /// Bussiness class for user
    /// </summary>
    public class UserBl:BussinessBase
    {

        /// <summary>
        /// Calls stored procedure which adds user.
        /// </summary>
        /// <param name="userData">User data object.</param>
        /// <param name="userId">Out parameter created UserId.</param>
        /// <returns>True if added.</returns>
        public bool AddUser(User userData,out int userId)
        {
            return UserDataAccesor.AddUser(userData , out userId);
        }

        /// <summary>
        /// Calls stored procedure which updates user.
        /// </summary>
        /// <param name="userData">User data object.</param>
        /// <returns>True if updated.</returns>
        public bool UpdateUser(User userData)
        {
            userData.Password = Common.Encryption.Cryptography.Encrypt(userData.Password);
            return UserDataAccesor.UpdateUser(userData);
        }

        /// <summary>
        /// GEt all user
        /// </summary>
        /// <returns>List of User</returns>
        public List<User> GetAllUsers()
        {
            return UserDataAccesor.GetAllUsers();
        }

        /// <summary>
        /// Get User By name
        /// </summary>
        /// <param name="userName">string of User Name</param>
        /// <returns>instance of User object</returns>
        public User GetUserByUserName(string userName)
        {
            return (string.IsNullOrEmpty(userName)) ? new User() : UserDataAccesor.GetUserByUserName(userName);
        }

        /// <summary>
        /// Get view model for user profile page
        /// </summary>
        /// <param name="userId">logged in user id</param>
        /// <returns>instance of User vm</returns>
        public UserProfileVm GetUserProfileVm(int userId)
        {
          
            return new UserProfileVm
            {
                User = UserDataAccesor.GetUserById(userId) ,
                Skills = SkillDataAccesor.GetSkillsByUserId(userId) ,
                AllSkills = SkillDataAccesor.GetAllSkillsForApp() ,
                Sessions = SessionDataAccesor.GetSessionsByUserId(userId) ,
                Projects = ProjectDataAccesor.GetProjectsByUserId(userId) ,
                Feedbacks = FeedbackDataAccesor.GetUserFeedback(userId , 5) ,
                RecentCrFeedback = FeedbackDataAccesor.GetUserFeedback(userId , 1000 , 4) ,
                RecentWeeklyFeedback = FeedbackDataAccesor.GetUserFeedback(userId , 1000 , 5) ,
                AllTrainer =  GetAllUsers().Where(x=>x.IsTrainer || x.IsManager).ToList(),
                FeedbackTypes = new List<FeedbackType>
                {
                    new FeedbackType
                    {
                        FeedbackTypeId =1, Description = "Comment"
                    },
                    new FeedbackType
                    {
                        FeedbackTypeId =2, Description = "Skill"
                    },
                    new FeedbackType
                    {
                        FeedbackTypeId =3, Description = "Assignment"
                    },
                    new FeedbackType
                    {
                        FeedbackTypeId =4, Description = "Code Review"
                    },
                    new FeedbackType
                    {
                        FeedbackTypeId =5, Description = "Weekly Feedback"
                    }
                }
            };
        }

        /// <summary>
        /// Fetches user Feedback based on filter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageSize"></param>
        /// <param name="feedbackId"></param>
        /// <returns></returns>
        public List<Feedback> GetUserFeedbackOnFilter(int userId, int pageSize, int feedbackId )
        {
            return FeedbackDataAccesor.GetUserFeedback(userId , pageSize , feedbackId);
        }
    }
}