using System;
using System.Collections.Generic;
using System.Linq;
using TrainingTracker.BLL.Base;
using TrainingTracker.Common.ViewModel;
using TrainingTracker.DAL.DataAccess;

namespace TrainingTracker.BLL
{
    public class DashboardBl:BussinessBase
    {

        public DashboardVm GetDashboardData()
        {
            var dashboardVm = new DashboardVm
            {
                Trainees = UserDataAccesor.GetDashboardData()
            };

            var lastFriday = DateTime.Now;
            while (lastFriday.DayOfWeek != DayOfWeek.Friday) lastFriday = lastFriday.AddDays(-1);

            foreach (var trainee in dashboardVm.Trainees)
            {
                bool feedbackAdded = false;
                foreach (var feedback in trainee.WeeklyFeedback)
                {
                    feedback.WeekForFeedbackPresent =string.Empty;

                    if (feedback.StartDate >= lastFriday.AddDays(-5)) feedbackAdded = true;

                    feedback.WeekForFeedbackPresent= feedback.StartDate.ToString("dd/MM/yyyy") + "-" + feedback.EndDate.ToString("dd/MM/yyyy");
                }
                trainee.LastWeekFeedbackAdded = feedbackAdded;
               
                // trainee.IsFeedbackPending = !(trainee.LastWeeklyFeedback > checkLowerDate && trainee.LastWeeklyFeedback <= lastFriday);
            }

            return dashboardVm;
        }
    }
}