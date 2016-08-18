using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TrainingTracker.Common.Entity;
using TrainingTracker.Common.Utility;
using TrainingTracker.DAL.Interface;


namespace TrainingTracker.DAL.DataAccess
{
    public class FeedbackDal:IFeedbackDal
    {

        public bool AddFeedback(Feedback feedbackData)
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
                var rowsAffected = SqlUtility.ExecuteNonQuery(SPAddFeedback.NAME,
                    CommandType.StoredProcedure, prms);
                return (rowsAffected > 0);
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
            }
            return false;
        }

        /// <summary>
        /// Gets all feedback for a users.
        /// </summary>
        /// <returns>List of all feedback for a users.</returns>
        public List<Feedback> GetUserFeedback(int userId, int count,int? feedbackId= null)
        {
            var feedbacks = new List<Feedback>();

            var prms = new List<SqlParameter>
            {
                SqlUtility.CreateParameter(SPGetUserFeedbacks.PARAM_USER_ID, SqlDbType.Int, userId),
                SqlUtility.CreateParameter(SPGetUserFeedbacks.FEEDBACK_ID, SqlDbType.Int, feedbackId),
                SqlUtility.CreateParameter(SPGetUserFeedbacks.PARAM_PAGE_SIZE, SqlDbType.Int, count),
                SqlUtility.CreateParameter(SPGetUserFeedbacks.PARAM_OFFSET, SqlDbType.Int, 0)
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
                                       EndDate = Convert.ToDateTime(row["EndDate"])
                                       
                                   });
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
            }

            return feedbacks;
        }

    }
}