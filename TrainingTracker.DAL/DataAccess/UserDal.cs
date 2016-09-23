using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TrainingTracker.Common.Utility;
using TrainingTracker.Common.ViewModel;
using TrainingTracker.DAL.EntityFramework;
using TrainingTracker.DAL.Interface;
using Feedback = TrainingTracker.Common.Entity.Feedback;
using FeedbackType = TrainingTracker.Common.Entity.FeedbackType;
using Skill = TrainingTracker.Common.Entity.Skill;
using User = TrainingTracker.Common.Entity.User;

namespace TrainingTracker.DAL.DataAccess
{
    /// <summary>
    /// Class for User Dal Implementation
    /// </summary>
    public class UserDal : IUserDal
    {

        /// <summary>
        /// Calls stored procedure which validates user.
        /// </summary>
        /// <param name="userData">User data object.</param>
        /// <returns>True if valid customer.</returns>
        public bool ValidateUser( User userData )
        {
            try
            {
                using (TrainingTrackerEntities context = new TrainingTrackerEntities())
                {
                    return context.Users.Any(x => x.UserName == userData.UserName && x.Password == userData.Password);
                }
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
                return false;
            }
        }

        /// <summary>
        /// Calls stored procedure which adds user.
        /// </summary>
        /// <param name="userData">User data object.</param>
        /// <param name="userId">Out parameter created UserId.</param>
        /// <returns>True if added.</returns>
        public bool AddUser( User userData , out int userId )
        {
            userId = 0;
            try
            {
                using (TrainingTrackerEntities context = new TrainingTrackerEntities())
                {
                    EntityFramework.User objUser = new EntityFramework.User
                                                        {
                                                            FirstName = userData.FirstName ,
                                                            LastName = userData.LastName ,
                                                            UserName = userData.UserName ,
                                                            Password = userData.Password ,
                                                            Email = userData.Email ,
                                                            Designation = userData.Designation ,
                                                            ProfilePictureName = userData.ProfilePictureName ,
                                                            IsFemale = userData.IsFemale ,
                                                            IsAdministrator = userData.IsAdministrator ,
                                                            IsTrainer = userData.IsTrainer ,
                                                            IsTrainee = userData.IsTrainee ,
                                                            IsManager = userData.IsManager ,
                                                            DateAddedToSystem = DateTime.Now,
                                                            IsActive = true
                                                        };
                    context.Users.Add(objUser);
                    context.SaveChanges();
                    userId = objUser.UserId;
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
                return false;
            }
        }

        /// <summary>
        /// Calls stored procedure which updates user.
        /// </summary>
        /// <param name="userData">User data object.</param>
        /// <returns>True if updated.</returns>
        public bool UpdateUser( User userData )
        {
            if (userData.UserId <= 0) return false;

            try
            {
                using (TrainingTrackerEntities context = new TrainingTrackerEntities())
                {
                    var userContext = context.Users.FirstOrDefault(x => x.UserId == userData.UserId);

                    if (userContext == null) return false;

                    userContext.FirstName = userData.FirstName;
                    userContext.LastName = userData.LastName;
                    userContext.UserName = userData.UserName;
                    userContext.Email = userData.Email;
                    userContext.Designation = userData.Designation;
                    userContext.ProfilePictureName = userData.ProfilePictureName;
                    userContext.IsFemale = userData.IsFemale;
                    userContext.IsAdministrator = userData.IsAdministrator;
                    userContext.IsTrainer = userData.IsTrainer;
                    userContext.IsTrainee = userData.IsTrainee;
                    userContext.IsManager = userData.IsManager;

                    if (!string.IsNullOrEmpty(userData.Password))
                    {
                        userContext.Password = userData.Password;
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
                return false;
            }
        }

        /// <summary>
        /// Gets all Users.
        /// </summary>
        /// <returns>List of all users.</returns>
        public List<User> GetAllUsers()
        {
            try
            {
                using (TrainingTrackerEntities context = new TrainingTrackerEntities())
                {
                    return context.Users.Select(x => new User
                                                    {
                                                        UserId = x.UserId ,
                                                        FirstName = x.FirstName ,
                                                        LastName = x.LastName ,
                                                        FullName = x.FirstName + " " + x.LastName,
                                                        UserName = x.UserName ,
                                                        Email = x.Email ,
                                                        Designation = x.Designation ,
                                                        ProfilePictureName = x.ProfilePictureName ,
                                                        IsFemale = x.IsFemale??false ,
                                                        IsAdministrator = x.IsAdministrator ?? false ,
                                                        IsTrainer = x.IsTrainer ?? false ,
                                                        IsTrainee = x.IsTrainee ?? false ,
                                                        IsManager = x.IsManager ?? false ,
                                                        IsActive = x.IsActive ?? false,
                                                        DateAddedToSystem = x.DateAddedToSystem                                                       
                                                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
                return null;
            }
        }

        /// <summary>
        /// Fetch user by User Name
        /// </summary>
        /// <param name="userName">string of username to be searched</param>
        /// <returns>Instance of User</returns>
        public User GetUserByUserName( string userName )
        {
            try
            {
                using (TrainingTrackerEntities context = new TrainingTrackerEntities())
                {
                    //No user can have same UserName, so used First in IQuerable clause intented to throw exception.
                    return context.Users.Where(x => x.UserName == userName)
                                        .Select(x=> new User
                                                {
                                                   UserId = x.UserId ,
                                                   FirstName = x.FirstName ,
                                                   LastName = x.LastName ,
                                                   UserName = x.UserName ,
                                                   Email = x.Email ,
                                                   Designation = x.Designation ,
                                                   ProfilePictureName = x.ProfilePictureName ,
                                                   IsFemale = x.IsFemale ?? false ,
                                                   IsAdministrator = x.IsAdministrator ?? false ,
                                                   IsTrainer = x.IsTrainer ?? false ,
                                                   IsTrainee = x.IsTrainee ?? false ,
                                                   IsManager = x.IsManager ?? false ,
                                                   IsActive = x.IsActive ?? false ,
                                                   UserRating = 0 
                                                }).First();

                }
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
                return new User();
            }
        }

        /// <summary>
        /// Fetch User by UserId
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>Instance of user</returns>
        public User GetUserById( int userId )
        {
            try
            {
                using (TrainingTrackerEntities context = new TrainingTrackerEntities())
                {
                    //No user can have same user id it's an Primary key, so used First in IQuerable clause intented to throw exception.
                    return context.Users.Where(x => x.UserId == userId)
                                        .Select(x => new User
                                        {
                                            UserId = x.UserId ,
                                            FirstName = x.FirstName ,
                                            LastName = x.LastName ,
                                            UserName = x.UserName ,
                                            Email = x.Email ,
                                            Designation = x.Designation ,
                                            ProfilePictureName = x.ProfilePictureName ,
                                            IsFemale = x.IsFemale ?? false ,
                                            IsAdministrator = x.IsAdministrator ?? false ,
                                            IsTrainer = x.IsTrainer ?? false ,
                                            IsTrainee = x.IsTrainee ?? false ,
                                            IsManager = x.IsManager ?? false ,
                                            IsActive = x.IsActive ?? false ,
                                            DateAddedToSystem = x.DateAddedToSystem ,   
                                            UserRating = 0
                                        }).First();
                }
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets all Users.
        /// </summary>
        /// <returns>List of all users.</returns>
        public List<UserData> GetDashboardData()
        {
            var users = new List<UserData>();
            //var objUserData = new UserData();

            var prms = new List<SqlParameter>();
            try
            {
                var dt = SqlUtility.ExecuteAndGetTable(SPGetDashboardData.NAME ,
                    CommandType.StoredProcedure , SPGetDashboardData.TABLE_NAME , prms);

                foreach (DataRow rows in dt.Rows)
                {
                    if (!users.Exists(x => x.User.UserId == Convert.ToInt32(rows["UserId"])))
                    {
                        users.Add(new UserData
                        {
                            User = new User
                            {
                                UserId = Convert.ToInt32(rows["UserId"]),
                                DateAddedToSystem = Convert.ToDateTime(rows["DateAddedToSystem"]),
                                FullName = rows["FullName"].ToString() ,
                            } ,
                            RemainingFeedbacks = new List<Feedback>() ,
                            WeeklyFeedback = new List<Feedback>() ,
                            SkillsFeedback = new List<Feedback>()
                        });
                    }
                    var objUserData = users.First(x => x.User.UserId == Convert.ToInt32(rows["UserId"]));
                    AddFeedbacksToTheList(ref users , rows);
                }
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
            }

            return users;
        }

        /// <summary>
        /// Private method to Add Feedbacks to the  List
        /// </summary>
        /// <param name="userData">reference to userData</param>
        /// <param name="row">datarow</param>
        private void AddFeedbacksToTheList( ref List<UserData> userData , DataRow row )
        {
            int skillId , feedbackType;
            DateTime startDate , addedOn;
            DateTime endDate;

            DateTime.TryParse(Convert.ToString(row["StartDate"]) , out startDate);
            DateTime.TryParse(Convert.ToString(row["EndDate"]) , out endDate);
            DateTime.TryParse(Convert.ToString(row["AddedOn"]) , out addedOn);
            Int32.TryParse(Convert.ToString(row["SkillId"]) , out skillId);
            Int32.TryParse(Convert.ToString(row["FeedbackType"]) , out feedbackType);

            if (feedbackType == 0) return;
         
                var feedback = new Feedback
                {
                    AddedBy = new User { FullName = Convert.ToString(row["AddedBy"]) } ,
                    AddedOn = addedOn ,
                    //						AddedBy	AddedOn		
                    FeedbackType = new FeedbackType
                    {
                        Description = Convert.ToString(row["FeedbackText"]) ,
                        FeedbackTypeId = Convert.ToInt32(row["FeedbackType"])
                    } ,
                    //Project = new Project{}
                    Skill = new Skill
                    {
                        Name = (row["SkillName"]).ToString() ,
                        SkillId = skillId
                    } ,
                    Title = Convert.ToString(row["Title"]) ,
                    Rating = Convert.ToInt32(row["Rating"]) ,
                    StartDate = startDate ,
                    EndDate = endDate ,
                };

                int userId = Convert.ToInt32(row["UserId"]);

                switch (Convert.ToInt32(row["FeedbackType"]))
                {
                    case 3:
                    case 4:
                        userData.First(x => x.User.UserId == userId)
                                .RemainingFeedbacks
                                .Add(feedback);
                        break;
                    case 2:
                        if (!userData.First(x => x.User.UserId == Convert.ToInt32(row["UserId"]))
                                 .SkillsFeedback.Any(x => x.Skill.Name.Equals((row["SkillName"]).ToString())))
                            userData.First(x => x.User.UserId == Convert.ToInt32(row["UserId"]))
                                    .SkillsFeedback
                                    .Add(feedback);
                        break;
                    case 5:
                        var objUSerData = userData.First(x => x.User.UserId == userId);

                        objUSerData.WeeklyFeedback.Add(feedback);
                        objUSerData.RemainingFeedbacks.Add(feedback);
                        break;
                }
        }

	    /// <summary>
        /// Public method GetUserId returns list of user id as per the condition,
        /// So specific notifications are added to specific user profile.
        /// </summary>
        /// <param name="notificationType">Contain notificationType as parameter.</param>
        /// <returns>Returns user Ids as a list.</returns>
        public List<int> GetUserId(Common.Enumeration.NotificationType notificationType, int addedFor)
        {
            try
            {
                using (TrainingTrackerEntities context = new TrainingTrackerEntities())
                {
                    if (notificationType == Common.Enumeration.NotificationType.FeedbackNotification)
                    {
                        return context.Users
                                      .Where(x => x.UserId == addedFor || x.IsTrainer == true || x.IsManager == true)
                                      .Select(x => x.UserId)
                                      .ToList();
                    }
                    else if(notificationType == Common.Enumeration.NotificationType.ReleaseNotification)
                    {
                        return context.Users
                                  .Select(x => x.UserId)
                                  .ToList();
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
                return null;
            }
        }	
    }
}