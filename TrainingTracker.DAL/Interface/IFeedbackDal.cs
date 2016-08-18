using System.Collections.Generic;
using TrainingTracker.Common.Entity;

namespace TrainingTracker.DAL.Interface
{
    /// <summary>
    /// Interface for Feedback dal
    /// </summary>
    public interface IFeedbackDal
    {
        /// <summary>
        /// Interface method to add feddback
        /// </summary>
        /// <param name="feedbackData">instance of new feedback data</param>
        /// <returns>boolean value of success event</returns>
        bool AddFeedback( Feedback feedbackData );

        List<Feedback> GetUserFeedback(int userId, int count, int? feedbackId=null );
    }
}
