using System.Collections.Generic;
using TrainingTracker.Common.Entity;
using Feedback = TrainingTracker.Models.Feedback;
using Project = TrainingTracker.Models.Project;
using Session = TrainingTracker.Models.Session;
using Skill = TrainingTracker.Models.Skill;
using User = TrainingTracker.Models.User;

namespace TrainingTracker.ViewModel
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
    }
}