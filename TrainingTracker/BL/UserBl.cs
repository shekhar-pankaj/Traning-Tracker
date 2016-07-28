using System.Collections.Generic;
using TrainingTracker.Common.Entity;
using TrainingTracker.Common.ViewModel;
using TrainingTracker.DAL;
using TrainingTracker.DAL.DataAccess;


namespace TrainingTracker.BL
{
    public class UserBl
    {

        /// <summary>
        /// Calls stored procedure which adds user.
        /// </summary>
        /// <param name="userData">User data object.</param>
        /// <returns>True if added.</returns>
        public bool AddUser(User userData)
        {
            return new UserDal().AddUser(userData);
        }

        public List<User> GetAllUsers()
        {
            return new UserDal().GetAllUsers();
        }

        public User GetUserByUserName(string userName)
        {
            return (string.IsNullOrEmpty(userName)) ? new User() : new UserDal().GetUserByUserName(userName);
        }

        public UserProfileVm GetUserProfileVm(int userId)
        {
            return new UserProfileVm
            {
                User = new UserDal().GetUserById(userId),
                Skills = new SkillDal().GetSkillsByUserId(userId),
                AllSkills = new SkillDal().GetAllSkillsForApp(),
                Sessions = new SessionDal().GetSessionsByUserId(userId),
                Projects = new ProjectDal().GetProjectsByUserId(userId),
                Feedbacks = new FeedbackDal().GetUserFeedback(userId, 5),
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
            return new FeedbackDal().GetUserFeedback(userId , pageSize , feedbackId);
        }
    }
}