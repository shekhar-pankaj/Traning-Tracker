using System;
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

            var checkLowerDate = new DateTime(1900, 1, 1);

            foreach (var trainee in dashboardVm.Trainees)
            {
                trainee.IsFeedbackPending = !(trainee.LastWeeklyFeedback > checkLowerDate && trainee.LastWeeklyFeedback <= lastFriday);
            }

            return dashboardVm;
        }
    }
}