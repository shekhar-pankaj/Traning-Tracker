using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingTracker.BLL.Base;
using TrainingTracker.Common.Entity;
using TrainingTracker.Common.Utility;

namespace TrainingTracker.BLL
{
    /// <summary>
    /// instnce class for Survey bussiness layer
    /// </summary>
    public class SurveyBl:BussinessBase
    {
        /// <summary>
        /// Bussiness method to Fecth weekly survey Questions for team
        /// </summary>
        /// <param name="teamId">team id</param>
        /// <returns>Instance of survey id</returns>
        public Survey FetchWeeklySurveyQuestionForTeam(int teamId)
        {
            return SurveyDataAccesor.GetWeeklySurveySetForTeam(teamId);
        }

        public bool SaveWeeklySurveyResponseForTrainee(SurveyResponse response)
        {
            // Weekly feedback must have team associated with it.
            if (!response.AddedBy.TeamId.HasValue) return false;

            Survey survey = SurveyDataAccesor.GetWeeklySurveySetForTeam(response.AddedBy.TeamId.Value);
            SurveyQuestion question=new SurveyQuestion();
            try
            {
                foreach (ResponseMapping surveyReponse in response.Response)
                {
                    var questionSet = survey.SurveySubSections.First(x => x.Questions.Any(y => y.SurveyQuestionId == surveyReponse.QuestionId))
                                                              .Questions
                                                              .First(x => x.SurveyQuestionId == surveyReponse.QuestionId);

                    surveyReponse.QuestionText = questionSet.QuestionText;
                    var answer = questionSet.Answers.FirstOrDefault(x => x.Id == surveyReponse.AnswerId);

                    if (answer != null) surveyReponse.AnswerText = answer.OptionText;
                }

                response.Feedback.FeedbackText = UtilityFunctions.GenerateHtmlForFeedbackOnSurveyResponse(response);

                response.Feedback.FeedbackId = SurveyDataAccesor.SaveWeeklySurveyResponseForTrainee(response, survey);
                return response.Feedback.FeedbackId > 0 && new NotificationBl().AddFeedbackNotification(response.Feedback);
            }
            catch (Exception ex)
            {
                LogUtility.ErrorRoutine(ex);
                return false;
            }        
        }
    }
}
