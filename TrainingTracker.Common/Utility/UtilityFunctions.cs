using System;
using System.Collections.Generic;
using System.Text;
using TrainingTracker.Common.Entity;

namespace TrainingTracker.Common.Utility
{
    /// <summary>
    /// Common Utility Functions to be used through the application
    /// </summary>
    public static class UtilityFunctions
    {
       /// <summary>
       /// Static Function that returns the Last Date on the given day
       /// * eg. this Function returns the Last Friday Date wrt Today's date, If today is Friday, last friday will be today
       /// </summary>
       /// <param name="dayOfWeek">enumeration of type day of week</param>
        /// <param name="referenceDate">reference Date from where the last date wrt day to be calculated</param>
       /// <returns>returns the last day</returns> 
        public static DateTime GetLastDateByDay(DayOfWeek dayOfWeek,DateTime referenceDate )
       {
           var differenceInDay =  dayOfWeek - referenceDate.DayOfWeek ;           
           return differenceInDay > 0 ? referenceDate.AddDays(-7 + differenceInDay) : referenceDate.AddDays(differenceInDay);
       }

        /// <summary>
        /// Returns All the weeks list between two given dates, * week here starts on Monday
        /// </summary>
        /// <param name="startDate">start date</param>
        /// <param name="endDate">end date</param>
        /// <returns>List of week strings</returns>
        public static List<string> GetAllWeeksBetweenDates(DateTime? startDate, DateTime endDate)
        {
            List<string> allWeeksList =new List<string>();

            if (!startDate.HasValue) return allWeeksList;

            while (endDate > startDate)
            {
                allWeeksList.Add(endDate.AddDays(-4).Date.ToString("dd/MM/yyyy") + "-" + endDate.Date.ToString("dd/MM/yyyy"));
                endDate = endDate.AddDays(-7);
            }
            return allWeeksList;
        }

        public static string GenerateHtmlForFeedbackOnSurveyResponse(SurveyResponse response)
        {
            StringBuilder stringBuilder= new StringBuilder();

            stringBuilder.Append("<div class='weekly-feedback'><code>");

            foreach (var responseAnswers in response.Response)
            {
                stringBuilder.Append("<div class='feedback-zone'>");
                stringBuilder.Append("<div class='feedback-question'><label>").Append(responseAnswers.QuestionText.Replace("[[[trainee]]]" , response.AddedFor.FirstName)).Append("</label></div>");

                if (!string.IsNullOrEmpty(Convert.ToString(responseAnswers.AnswerText))) 
                stringBuilder.Append("<div class='feedback-answer'><label> ").Append(responseAnswers.AnswerText).Append("</label></div>");

                if (!string.IsNullOrEmpty(Convert.ToString(responseAnswers.AdditionalNotes)) && responseAnswers.AdditionalNotes.Trim().Length > 0) 
                stringBuilder.Append("<div class='feedback-notes'><label><q>").Append(responseAnswers.AdditionalNotes.Trim().Replace("<script>" , "&lt;script&gt;").Replace("</script>" , "&lt;/script&gt;")).Append("</q></label></div>");

                if (string.IsNullOrEmpty(Convert.ToString(responseAnswers.AdditionalNotes)) &&
                    string.IsNullOrEmpty(responseAnswers.AnswerText))
                {
                    stringBuilder.Append("<div class='feedback-notes'><label><q>").Append("Question Skipped").Append("</q></label></div>");
                }
                stringBuilder.Append("</div>");
            }
            stringBuilder.Append("</code></div>");

            return stringBuilder.ToString();
        }
    }
}
