using System;
using System.Linq;
using TrainingTracker.BLL.Base;
using TrainingTracker.Common.ViewModel;

namespace TrainingTracker.BLL
{
    /// <summary>
    /// Bussiness Layer class for Dashboard, Inherits Bussiness Base Class
    /// </summary>
    public class DashboardBl:BussinessBase
    {

        public DashboardVm GetDashboardData()
        {
            var dashboardVm = new DashboardVm
            {
                Trainees = UserDataAccesor.GetDashboardData(),
                UpcomingSessions = SessionDataAccesor.GetSessionOnFilter(100,1,"")
            };


            DateTime lastFriday = Common.Utility.UtilityFunctions.GetLastDateByDay(DayOfWeek.Friday,DateTime.Now);

            foreach (var trainee in dashboardVm.Trainees)
            {
                bool isFeedbackAdded = false;

                // No Feedback pending should be there if user is added after last friday
                if ((trainee.User.DateAddedToSystem >= lastFriday))
                {
                    isFeedbackAdded = true;
                }
                else
                {
                    trainee.WeekForFeedbackNotPresent = Common.Utility.UtilityFunctions.GetAllWeeksBetweenDates(trainee.User.DateAddedToSystem , lastFriday);
                   
                    foreach (var feedback in trainee.WeeklyFeedback)
                    {
                        var startOfWeeks = Common.Utility.UtilityFunctions.GetLastDateByDay(DayOfWeek.Monday , feedback.StartDate); 

                        // Check for last week feedback added or not
                        if (feedback.StartDate >= lastFriday.AddDays(-5)) isFeedbackAdded = true;

                        feedback.WeekForFeedbackPresent = feedback.StartDate.ToString("dd/MM/yyyy") + "-" + feedback.EndDate.ToString("dd/MM/yyyy");
                        trainee.WeekForFeedbackNotPresent.Remove(startOfWeeks.ToString("dd/MM/yyyy") + "-" + startOfWeeks.AddDays(4).ToString("dd/MM/yyyy"));
                    }
                }
                trainee.LastWeekFeedbackAdded = isFeedbackAdded;

                trainee.RemainingFeedbacks = trainee.RemainingFeedbacks.OrderByDescending(x => x.AddedOn).ToList();
            }

            return dashboardVm;
        }
    }
}