using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TrainingTracker.Common.Entity;
using TrainingTracker.Common.Utility;
using TrainingTracker.DAL.EntityFramework;
using TrainingTracker.DAL.Interface;
using Feedback = TrainingTracker.Common.Entity.Feedback;
using FeedbackType = TrainingTracker.Common.Entity.FeedbackType;
using User = TrainingTracker.Common.Entity.User;

namespace TrainingTracker.DAL.DataAccess
{
    /// <summary>
    /// Data access classs for Feedback , Implements IFeedbackDal
    /// </summary>
    public class FeedbackDal:IFeedbackDal
    {
        /// <summary>
        /// Dal method to Add feedback
        /// </summary>
        /// <param name="feedbackData">feedback instance</param>
        /// <returns>int of feedback</returns>
        public int AddFeedback(Feedback feedbackData)
        {            
            try
            {
                var prms = new List<SqlParameter>
            {
                SqlUtility.CreateParameter(SPAddFeedback.PARAM_FEEDBACK_TEXT, 
                SqlDbType.VarChar,feedbackData.FeedbackText),
                SqlUtility.CreateParameter(SPAddFeedback.PARAM_TITLE, 
                SqlDbType.VarChar,feedbackData.Title),
                SqlUtility.CreateParameter(SPAddFeedback.PARAM_FEEDBACK_TYPE, 
                SqlDbType.Int,feedbackData.FeedbackType.FeedbackTypeId),
                SqlUtility.CreateParameter(SPAddFeedback.PARAM_PROJECT_ID, 
                SqlDbType.Int,feedbackData.Project.ProjectId),
                SqlUtility.CreateParameter(SPAddFeedback.PARAM_SKILL_ID, 
                SqlDbType.Int,feedbackData.Skill.SkillId),
                SqlUtility.CreateParameter(SPAddFeedback.PARAM_RATING, 
                SqlDbType.Int,feedbackData.Rating),
                SqlUtility.CreateParameter(SPAddFeedback.PARAM_ADDED_BY, 
                SqlDbType.Int,feedbackData.AddedBy.UserId),
                SqlUtility.CreateParameter(SPAddFeedback.PARAM_ADDED_FOR, 
                SqlDbType.Int,feedbackData.AddedFor.UserId),
                SqlUtility.CreateParameter(SPAddFeedback.PARAM_START_DATE, 
                SqlDbType.Date,feedbackData.StartDate),
                SqlUtility.CreateParameter(SPAddFeedback.PARAM_END_DATE, 
                SqlDbType.Date,feedbackData.EndDate),
                SqlUtility.CreateParameter("@AddedOn", 
                SqlDbType.DateTime,feedbackData.AddedOn==DateTime.MinValue? DateTime.Now:feedbackData.AddedOn)
            };
                return SqlUtility.ExecuteScalar(SPAddFeedback.NAME,
                    CommandType.StoredProcedure, prms);
                
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
                return 0;
            }
            
        }

        /// <summary>
        /// Gets all feedback for a users.
        /// </summary>
        /// <returns>List of all feedback for a users.</returns>
        public List<Feedback> GetUserFeedback(int userId, int count, int? feedbackId = null, DateTime? startAddedOn = null, DateTime? endAddedOn = null)
        {
            var feedbacks = new List<Feedback>();

            var prms = new List<SqlParameter>
            {
                SqlUtility.CreateParameter(SPGetUserFeedbacks.PARAM_USER_ID, SqlDbType.Int, userId),
                SqlUtility.CreateParameter(SPGetUserFeedbacks.FEEDBACK_ID, SqlDbType.Int, feedbackId),
                SqlUtility.CreateParameter(SPGetUserFeedbacks.PARAM_PAGE_SIZE, SqlDbType.Int, count),
                SqlUtility.CreateParameter(SPGetUserFeedbacks.PARAM_OFFSET, SqlDbType.Int, 0),
                SqlUtility.CreateParameter(SPGetUserFeedbacks.START_ADDED_ON, SqlDbType.Date, startAddedOn),
                SqlUtility.CreateParameter(SPGetUserFeedbacks.END_ADDED_ON, SqlDbType.Date, endAddedOn)
            };

            try
            {
                var dt = SqlUtility.ExecuteAndGetTable(SPGetUserFeedbacks.NAME,
                    CommandType.StoredProcedure, SPGetUserFeedbacks.TABLE_NAME, prms);

                feedbacks.AddRange(from DataRow row in dt.Rows
                                   select new Feedback
                                   {
                                       FeedbackId = Convert.ToInt32(row["FeedbackId"]),
                                       FeedbackText = row["FeedbackText"].ToString(),
                                       Title = row["Title"].ToString(),
                                       FeedbackType = new FeedbackType
                                       {
                                           FeedbackTypeId = Convert.ToInt32(row["FeedbackType"]),
                                           Description = row["FeedbackTypeName"].ToString(),
                                       },
                                       Rating = Convert.ToInt32(row["Rating"]),
                                       AddedOn = Convert.ToDateTime(row["AddedOn"]),
                                       AddedBy = new User
                                       {
                                           UserId = Convert.ToInt32(row["AddedBy"]),
                                           FullName = row["AddedByUser"].ToString(),
                                           ProfilePictureName = row["AddedByUserImage"].ToString(),
                                       },
                                       StartDate = Convert.ToDateTime(row["StartDate"]),
                                       EndDate = Convert.ToDateTime(row["EndDate"]),
                                       ThreadCount = Convert.ToInt32(row["ThreadCount"])
                                   });
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
            }
            return feedbacks;
        }

        /// <summary>
        /// Fetch feedback's threads
        /// </summary>
        /// <param name="feedbackId">feedback Id</param>
        /// <returns>List Of Threads</returns>
        public List<Threads> GetFeedbackThreads( int feedbackId )
        {
            try
            {
                using (TrainingTrackerEntities context = new TrainingTrackerEntities())
                {
                    return context.FeedbackThreads
                                  .Where(x => x.FeedbackId == feedbackId)
                                  .Select(x => new Threads
                                  {
                                     ThreadId = x.ThreadId,
                                     Comments = x.Comments,
                                     AddedBy = new User
                                                       {
                                                           UserId    = x.User.UserId,
                                                           FullName = x.User.FirstName + " " + x.User.LastName,
                                                           ProfilePictureName = x.User.ProfilePictureName
                                                       },
                                    DateInserted = x.DateTimeInserted
                                  })
                                  .ToList();
                }
            }
            catch(Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
                return null;
            }
        }

        /// <summary>
        /// Fetch feedback with threads
        /// </summary>
        /// <param name="feedbackId">feedback Id</param>
        /// <returns>Instance of Feedback</returns>
        public Feedback GetFeedbackWithThreads( int feedbackId )
        {
            try
            {
                using (TrainingTrackerEntities context = new TrainingTrackerEntities())
                {
                    var feedback = context.Feedbacks
                        .Where(x => x.FeedbackId == feedbackId)
                        .Select(x => new Feedback
                        {
                            FeedbackId = x.FeedbackId,
                            FeedbackText = x.FeedbackText,
                            Title = x.Title,
                            FeedbackType = new FeedbackType
                            {
                                FeedbackTypeId = x.FeedbackType1.FeedbackTypeId,
                                Description = x.FeedbackType1.Description,
                            },

                            Rating = x.Rating ?? 0,
                            AddedOn = x.AddedOn ?? new DateTime(),
                            AddedBy = new User
                            {
                                UserId = x.User.UserId,
                                FullName = x.User.FirstName + " " + x.User.LastName,
                                ProfilePictureName = x.User.ProfilePictureName,
                                TeamId = x.User.TeamId
                            },
                            StartDate = x.StartDate ?? new DateTime(),
                            EndDate = x.EndDate ?? new DateTime(),
                        }).FirstOrDefault();

                    if (feedback == null) return null;

                    feedback.Threads = context.FeedbackThreads.Where(x => x.FeedbackId == feedbackId)
                                                             .Select(x => new Threads
                                                                 {
                                                                    ThreadId = x.ThreadId,
                                                                    Comments = x.Comments,
                                                                    AddedBy = new User
                                                                                      {
                                                                                          UserId    = x.User.UserId,
                                                                                          FullName = x.User.FirstName + " " + x.User.LastName,
                                                                                          ProfilePictureName = x.User.ProfilePictureName
                                                                                      },
                                                                   DateInserted = x.DateTimeInserted
                                                                 }).ToList();
                    return feedback;

                }
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
                return null;
            }
        }

        /// <summary>
        /// Add New Thread to Feedback
        /// </summary>
        /// <param name="thread">instance of thread</param>
        /// <returns>Succes event of flag</returns>
        public bool AddNewThread(Threads thread)
        {
            try
            {
                using (TrainingTrackerEntities context = new TrainingTrackerEntities())
                {
                    context.FeedbackThreads.Add(new FeedbackThread
                                                 {
                                                     FeedbackId = thread.FeedbackId,
                                                     Comments = thread.Comments,
                                                     DateTimeInserted = DateTime.Now,
                                                     AddedBy = thread.AddedBy.UserId,                                                    
                                                 });
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
        /// Get Trainee User Id by Feedback Id
        /// </summary>
        /// <param name="feedbackId">feedbackId</param>
        /// <returns>User Id</returns>
        public int GetTraineebyFeedbackId(int feedbackId)
        {
            try
            {
                using (TrainingTrackerEntities context = new TrainingTrackerEntities())
                {
                    return context.Feedbacks.Where(x => x.FeedbackId == feedbackId)
                            .Select(x => x.AddedFor)
                            .FirstOrDefault() ?? 0;
                }
            }
             catch (Exception ex)
             {
                 LogUtility.ErrorRoutine(ex);
                 return 0;
             }
        }
    }
}