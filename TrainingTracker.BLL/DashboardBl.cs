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
                Trainees = UserDataAccesor.GetDashboardData(),
                UpcomingSessions = SessionDataAccesor.GetSessionOnFilter(100,1,"")
            };


            var lastFriday = DateTime.Now;
            while (lastFriday.DayOfWeek != DayOfWeek.Friday) lastFriday = lastFriday.AddDays(-1);

            foreach (var trainee in dashboardVm.Trainees)
            {
                bool feedbackAdded = false;

                if ((trainee.User.DateAddedToSystem >= lastFriday)) feedbackAdded = true;

                else
                {

                    trainee.WeekForFeedbackNotPresent = new List<string>();


                    DateTime endOfWeeks = lastFriday;

                    while (endOfWeeks > trainee.User.DateAddedToSystem)
                    {
                        trainee.WeekForFeedbackNotPresent.Add(endOfWeeks.AddDays(-4).Date.ToString("dd/MM/yyyy") + "-" + endOfWeeks.Date.ToString("dd/MM/yyyy"));

                        endOfWeeks = endOfWeeks.AddDays(-7);
                    }
                    foreach (var feedback in trainee.WeeklyFeedback)
                    {
                        feedback.WeekForFeedbackPresent = string.Empty;

                        if (feedback.StartDate >= lastFriday.AddDays(-5)) feedbackAdded = true;

                        feedback.WeekForFeedbackPresent = feedback.StartDate.ToString("dd/MM/yyyy") + "-" + feedback.EndDate.ToString("dd/MM/yyyy");

                        var startOfWeeks = feedback.StartDate;

                        while (startOfWeeks.DayOfWeek != DayOfWeek.Monday) startOfWeeks = startOfWeeks.AddDays(-1);

                        trainee.WeekForFeedbackNotPresent.Remove(startOfWeeks.ToString("dd/MM/yyyy") + "-" + startOfWeeks.AddDays(4).ToString("dd/MM/yyyy"));
                    }
                }
                trainee.LastWeekFeedbackAdded = feedbackAdded;

                trainee.RemainingFeedbacks = trainee.RemainingFeedbacks.OrderByDescending(x => x.AddedOn).ToList();

                // trainee.IsFeedbackPending = !(trainee.LastWeeklyFeedback > checkLowerDate && trainee.LastWeeklyFeedback <= lastFriday);
            }

            return dashboardVm;
        }
    }
}