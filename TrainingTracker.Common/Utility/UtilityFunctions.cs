using System;
using System.Collections.Generic;

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
    }
}
