using System.Collections.Generic;
using TrainingTracker.Common.Entity;

namespace TrainingTracker.Common.ViewModel
{
    public class UserProfileVm
    {
        public User User { get; set; }
        public List<Session> Sessions { get; set; }
        public List<Project> Projects { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public List<FeedbackType> FeedbackTypes { get; set; }
        public List<Skill> AllSkills { get; set; }
        public List<Feedback> RecentCrFeedback { get; set; }
    }
}