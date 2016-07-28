using System;
using System.Collections.Generic;
using System.ComponentModel;
using TrainingTracker.Models;

namespace TrainingTracker.ViewModel
{
    public class DashboardVm
    {
        public List<UserData> Trainees { get; set; }
    }

    public class UserData
    {
        public User User { get; set; }
        public int PoorRating { get; set; }
        public int AverageRating { get; set; }
        public int FastRating { get; set; }
        public int ExceptionalRating { get; set; }
        public DateTime LastWeeklyFeedback { get; set; }
        public string Skills { get; set; }
        public bool IsFeedbackPending { get; set; }
    }
}