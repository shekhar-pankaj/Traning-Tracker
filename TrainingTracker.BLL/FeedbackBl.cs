using TrainingTracker.BLL.Base;
using TrainingTracker.Common.Entity;

namespace TrainingTracker.BLL
{
    /// <summary>
    /// Business class for feedback, Implemets Business base
    /// </summary>
    public class FeedbackBl:BussinessBase
    {
        /// <summary>
        /// Add New feedback for user
        /// </summary>
        /// <param name="feedback">instance of feedback object</param>
        /// <returns>success flag wether feedback was added or not</returns>
        public bool AddFeedback(Feedback feedback)
        {
            feedback.Project = feedback.Project ?? new Project();
            feedback.Title = string.IsNullOrEmpty(feedback.Title) ? feedback.FeedbackType.Description : feedback.Title;
          
            if(feedback.FeedbackType.FeedbackTypeId == 2)
            {
                feedback.Title = feedback.Skill.Name;
                feedback.Skill = new Skill
                {
                    SkillId = feedback.Skill.SkillId
                };
            }
            else
            {
                feedback.Skill = new Skill();
            }

            // no way comment can have feedback rating
            if (feedback.FeedbackType.FeedbackTypeId == 1)
            {
                feedback.Rating = 0;
            }

            return FeedbackDataAccesor.AddFeedback(feedback);
        }
    }
}