using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using TrainingTracker.Common.Entity;
using TrainingTracker.Common.Utility;
using TrainingTracker.Common.ViewModel;
using TrainingTracker.DAL.Interface;


namespace TrainingTracker.DAL.DataAccess
{
    public class UserDal : IUserDal
    {

        /// <summary>
        /// Calls stored procedure which validates user.
        /// </summary>
        /// <param name="userData">User data object.</param>
        /// <returns>True if valid customer.</returns>
        public bool ValidateUser( User userData )
        {
            var prms = new List<SqlParameter>
            {
                SqlUtility.CreateParameter(SPValidateUser.PARAM_USER_NAME, 
                SqlDbType.VarChar,userData.UserName),
                SqlUtility.CreateParameter(SPValidateUser.PARAM_PASSWORD, 
                SqlDbType.VarChar,userData.Password)
            };
            try
            {
                var dt = SqlUtility.ExecuteAndGetTable(SPValidateUser.NAME ,
                    CommandType.StoredProcedure , SPValidateUser.TABLE_NAME , prms);
                if (dt.Rows[0][0].ToString().Equals("1"))
                    return true;
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
            }
            return false;
        }

        /// <summary>
        /// Calls stored procedure which adds user.
        /// </summary>
        /// <param name="userData">User data object.</param>
        /// <returns>True if added.</returns>
        public bool AddUser( User userData )
        {
            var prms = new List<SqlParameter>
            {
                SqlUtility.CreateParameter(SPAddUser.PARAM_FIRST_NAME, 
                SqlDbType.VarChar,userData.FirstName),
                SqlUtility.CreateParameter(SPAddUser.PARAM_LAST_NAME, 
                SqlDbType.VarChar,userData.LastName),
                SqlUtility.CreateParameter(SPAddUser.PARAM_USER_NAME, 
                SqlDbType.VarChar,userData.UserName),
                SqlUtility.CreateParameter(SPAddUser.PARAM_PASSWORD, 
                SqlDbType.VarChar,userData.Password),
                SqlUtility.CreateParameter(SPAddUser.PARAM_EMAIL, 
                SqlDbType.VarChar,userData.Email),
                SqlUtility.CreateParameter(SPAddUser.PARAM_DESIGNATION, 
                SqlDbType.VarChar,userData.Designation),
                SqlUtility.CreateParameter(SPAddUser.PARAM_PROFILE_PICTURE_NAME, 
                SqlDbType.VarChar,userData.ProfilePictureName),
                SqlUtility.CreateParameter(SPAddUser.PARAM_IS_FEMALE, 
                SqlDbType.Bit,userData.IsFemale),
                SqlUtility.CreateParameter(SPAddUser.PARAM_IS_ADMINISTRATOR, 
                SqlDbType.VarChar,userData.IsAdministrator),
                SqlUtility.CreateParameter(SPAddUser.PARAM_IS_TRAINER, 
                SqlDbType.VarChar,userData.IsTrainer),
                SqlUtility.CreateParameter(SPAddUser.PARAM_IS_TRAINEE, 
                SqlDbType.VarChar,userData.IsTrainee),
                SqlUtility.CreateParameter(SPAddUser.PARAM_IS_MANAGER, 
                SqlDbType.VarChar,userData.IsManager)
            };
            try
            {
                var rowsAffected = SqlUtility.ExecuteNonQuery(SPAddUser.NAME ,
                    CommandType.StoredProcedure , prms);
                return (rowsAffected > 0);
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
            }
            return false;
        }

        /// <summary>
        /// Gets all Users.
        /// </summary>
        /// <returns>List of all users.</returns>
        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            var prms = new List<SqlParameter>();
            try
            {
                var dt = SqlUtility.ExecuteAndGetTable(SPGetAllUsers.NAME ,
                    CommandType.StoredProcedure , SPGetAllUsers.TABLE_NAME , prms);

                users.AddRange(from DataRow row in dt.Rows
                               select new User
                               {
                                   UserId = Convert.ToInt32(row["UserId"]) ,
                                   FirstName = row["FirstName"].ToString() ,
                                   LastName = row["LastName"].ToString() ,
                                   UserName = row["UserName"].ToString() ,
                                   Email = row["Email"].ToString() ,
                                   Designation = row["Designation"].ToString() ,
                                   ProfilePictureName = row["ProfilePictureName"].ToString() ,
                                   IsFemale = Convert.ToBoolean(row["IsFemale"]) ,
                                   IsAdministrator = Convert.ToBoolean(row["IsAdministrator"]) ,
                                   IsTrainer = Convert.ToBoolean(row["IsTrainer"]) ,
                                   IsTrainee = Convert.ToBoolean(row["IsTrainee"]) ,
                                   IsManager = Convert.ToBoolean(row["IsManager"])
                               });
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
            }

            return users;
        }

        public User GetUserByUserName( string userName )
        {
            var user = new User();

            var prms = new List<SqlParameter>
            {
                SqlUtility.CreateParameter(SPGetUser.PARAM_USER_NAME, SqlDbType.VarChar, userName),
                SqlUtility.CreateParameter(SPGetUser.PARAM_SCENARIO, SqlDbType.Int, 2)
            };

            try
            {
                var dt = SqlUtility.ExecuteAndGetTable(SPGetUser.NAME ,
                    CommandType.StoredProcedure , SPGetUser.TABLE_NAME , prms);

                var row = dt.Rows[0];
                user = new User
                {
                    UserId = Convert.ToInt32(row["UserId"]) ,
                    FirstName = row["FirstName"].ToString() ,
                    LastName = row["LastName"].ToString() ,
                    UserName = row["UserName"].ToString() ,
                    Email = row["Email"].ToString() ,
                    Designation = row["Designation"].ToString() ,
                    ProfilePictureName = row["ProfilePictureName"].ToString() ,
                    IsFemale = Convert.ToBoolean(row["IsFemale"]) ,
                    IsAdministrator = Convert.ToBoolean(row["IsAdministrator"]) ,
                    IsTrainer = Convert.ToBoolean(row["IsTrainer"]) ,
                    IsTrainee = Convert.ToBoolean(row["IsTrainee"]) ,
                    IsManager = Convert.ToBoolean(row["IsManager"]) ,
                    UserRating = (row["UserRating"] == DBNull.Value) ? 0 : Convert.ToInt32(row["UserRating"])
                };
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
            }

            return user;
        }
        public User GetUserById( int userId )
        {
            var user = new User();

            var prms = new List<SqlParameter>
            {
                SqlUtility.CreateParameter(SPGetUser.PARAM_USER_ID, SqlDbType.Int, userId),
                SqlUtility.CreateParameter(SPGetUser.PARAM_SCENARIO, SqlDbType.Int, 1)
            };

            try
            {
                var dt = SqlUtility.ExecuteAndGetTable(SPGetUser.NAME ,
                    CommandType.StoredProcedure , SPGetUser.TABLE_NAME , prms);

                var row = dt.Rows[0];
                user = new User
                {
                    UserId = userId ,
                    FirstName = row["FirstName"].ToString() ,
                    LastName = row["LastName"].ToString() ,
                    UserName = row["UserName"].ToString() ,
                    Email = row["Email"].ToString() ,
                    Designation = row["Designation"].ToString() ,
                    ProfilePictureName = row["ProfilePictureName"].ToString() ,
                    IsFemale = Convert.ToBoolean(row["IsFemale"]) ,
                    IsAdministrator = Convert.ToBoolean(row["IsAdministrator"]) ,
                    IsTrainer = Convert.ToBoolean(row["IsTrainer"]) ,
                    IsTrainee = Convert.ToBoolean(row["IsTrainee"]) ,
                    IsManager = Convert.ToBoolean(row["IsManager"]) ,
                    UserRating = (row["UserRating"] == DBNull.Value) ? 0 : Convert.ToInt32(row["UserRating"])
                };
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
            }

            return user;
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
                                UserId = Convert.ToInt32(rows["UserId"]) ,
                                FullName = rows["FullName"].ToString() ,
                            },
                            RemainingFeedbacks = new List<Feedback>(),
                            WeeklyFeedback = new List<Feedback>(),
                            SkillsFeedback = new List<Feedback>()
                        });
                    }
                    var objUserData = users.First(x => x.User.UserId == Convert.ToInt32(rows["UserId"]));
                    AddFeedbacksToTheList(ref users , rows);
                }

                //users.AddRange(from DataRow row in dt.Rows
                //               select new UserData
                //               {
                //                   User = new User
                //                   {
                //                       UserId = Convert.ToInt32(row["UserId"]),
                //                       FullName = row["FullName"].ToString(),
                //                       UserName = row["UserName"].ToString(),
                //                       Email = row["Email"].ToString(),
                //                       ProfilePictureName = row["ProfilePictureName"].ToString()
                //                   },
                //                   PoorRating = Convert.ToInt32(row["PoorRating"]),
                //                   AverageRating = Convert.ToInt32(row["AverageRating"]),
                //                   FastRating = Convert.ToInt32(row["FastRating"]),
                //                   ExceptionalRating = Convert.ToInt32(row["ExceptionalRating"]),
                //                   Skills = (row["Skills"] == DBNull.Value) ? string.Empty : row["Skills"].ToString(),
                //                   LastWeeklyFeedback = (row["LastWeeklyFeedback"] == DBNull.Value) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(row["LastWeeklyFeedback"]),
                //               });
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
            }

            return users;
        }

        private void AddFeedbacksToTheList( ref List<UserData> userData , DataRow row )
        {
            int skillId,feedbackType;
            DateTime startDate,addedOn;
            DateTime endDate;

            DateTime.TryParse(Convert.ToString(row["StartDate"]) , out startDate);
            DateTime.TryParse(Convert.ToString(row["EndDate"]),out endDate);
            DateTime.TryParse(Convert.ToString(row["AddedOn"]) , out addedOn);
            Int32.TryParse(Convert.ToString(row["SkillId"]), out skillId);
            Int32.TryParse(Convert.ToString(row["FeedbackType"]) , out feedbackType);

            if (feedbackType == 0) return;

            try
            {
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
                        userData.First(x => x.User.UserId == Convert.ToInt32(row["UserId"]))
                                .WeeklyFeedback
                               .Add(feedback);
                        break;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}