using TrainingTracker.BLL.Base;
using TrainingTracker.Common.Entity;
using TrainingTracker.DAL.DataAccess;

namespace TrainingTracker.BLL
{
    public class FeedbackBl:BussinessBase
    {
        public bool AddFeedback(Feedback feedback)
        {
            feedback.Project = feedback.Project ?? new Project();
            //feedback.Skill = (feedback.Skill == null) ? new Skill() : feedback.Skill;

            feedback.Title = string.IsNullOrEmpty(feedback.Title) ? feedback.FeedbackType.Description : feedback.Title;
            if (feedback.Skill == null)
            {
                feedback.Skill = new Skill();
            }
            else if(feedback.FeedbackType.FeedbackTypeId == 2)
            {
                feedback.Title = feedback.Skill.Description;
            }

            return FeedbackDataAccesor.AddFeedback(feedback);
        }
    }
}