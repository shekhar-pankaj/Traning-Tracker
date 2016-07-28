using System.Collections.Generic;
using TrainingTracker.BLL.Base;
using TrainingTracker.Common.Entity;
using TrainingTracker.Common.ViewModel;

namespace TrainingTracker.BLL
{
    public class UserBl:BussinessBase
    {

        /// <summary>
        /// Calls stored procedure which adds user.
        /// </summary>
        /// <param name="userData">User data object.</param>
        /// <returns>True if added.</returns>
        public bool AddUser(User userData)
        {
            return UserDataAccesor.AddUser(userData);
        }

        public List<User> GetAllUsers()
        {
            return UserDataAccesor.GetAllUsers();
        }

        public User GetUserByUserName(string userName)
        {
            return (string.IsNullOrEmpty(userName)) ? new User() : UserDataAccesor.GetUserByUserName(userName);
        }

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