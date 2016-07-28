using TrainingTracker.Common.Entity;
using TrainingTracker.DAL;
using TrainingTracker.DAL.DataAccess;

namespace TrainingTracker.BL
{
    public class FeedbackBl
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

            return new FeedbackDal().AddFeedback(feedback);
        }
    }
}