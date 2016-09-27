namespace TrainingTracker.Common.Enumeration
{
    /// <summary>
    /// Enumeration type TypeOfNotification.
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
        /// set value of ReleaseNotification to 1.
        /// </summary>
        NewReleaseNotification = 1,

        /// <summary>
        ///  set value of FeedbackNotification to 2.
        /// </summary>
        CommentFeedbackNotification = 2,

        /// <summary>
        /// set value of Feature Notification to 3.
        /// </summary>
        NewFeatureRequestNotification = 3 ,

        /// <summary>
        /// set value of ReleaseNotification to 4.
        /// </summary>
        FeatureModifiedNotification = 4,

        /// <summary>
        ///  set value of WeeklyNotification to 5.
        /// </summary>
        WeeklyFeedbackNotification = 5 ,

        /// <summary>
        ///  set value of FeedbackNotification to 6.
        /// </summary>
        AssignmentFeedbackNotification = 6 ,

        /// <summary>
        ///  set value of FeedbackNotification to 7.
        /// </summary>
        CodeReviewFeedbackNotification = 7 ,

        /// <summary>
        ///  set value of FeedbackNotification to 8.
        /// </summary>
        SkillFeedbackNotification = 8 ,

        /// <summary>
        /// Enum for New Session Notification
        /// </summary>
        NewSessionNotification = 9,

        /// <summary>
        /// Enum for Session Updated Notification
        /// </summary>
        SessionUpdatedNotification = 10
    }


    /// <summary>
    /// enumeration for feedback type
    /// </summary>
    public enum FeedbackType
    {
        /// <summary>
        /// Comment Feedback
        /// </summary>
        Comment =1,

        /// <summary>
        /// Skill Feedback
        /// </summary>
        Skill=2,

        /// <summary>
        /// Assignment Feedback
        /// </summary>
        Assignment=3,

        /// <summary>
        /// Code Review Feedback
        /// </summary>
        CodeReview=4,

        /// <summary>
        /// Weekly Feedback
        /// </summary>
        Weekly=5
    }

}
