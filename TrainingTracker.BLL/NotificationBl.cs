/**************************************************************************************************
*   Created By : Satyabrata                                                                                                                                                           
*   Created On : 11 Sept 2016
*   Modified By :
*   Modified Date:
*   Description: Business class for Notification.
**************************************************************************************************/
using System;
using System.Collections.Generic;
using TrainingTracker.BLL.Base;
using TrainingTracker.Common.Entity;
using TrainingTracker.Common.Enumeration;

namespace TrainingTracker.BLL
{
    public class NotificationBl : BussinessBase
    {
        public string releaseLink = "/Release";
        public string feedbackLink = "/Profile/UserProfile?userId={0}";
        public string releaseDescription = "New release, Version:";
        public string feedbackDescription = "New comment on";

       

        /// <summary>
        /// Add notification for a list of users. 
        /// </summary>
        /// <param name="notification">Notification class onject</param>
        /// <param name="userIds">List of userId</param>
        /// <returns>Returns true if Notification is added successfully else false.</returns>
        public bool AddNotification(Notification notification, List<int> userIds)
        {
            return NotificationDataAccesor.AddNotification(notification, userIds);
        }

        /// <summary>
        /// Update notification which make notification seen status true.
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="notification">Notification object</param>
        /// <returns>Returns true if Notification is updated successfully else false.</returns>
        public bool UpdateNotification(int userId, Notification notification)
        {
            return NotificationDataAccesor.UpdateNotification(userId, notification);
        }


        /// <summary>
        /// Function which takes version generates notification message, list of userId, link
        /// Calls AddNotification() to save in the database.
        /// </summary>
        /// <param name="release">Release object</param>
        /// <param name="userId">UseId</param>
        /// <returns>Returns true if Notification is added successfully else false.</returns>
        public bool AddReleaseNotification(Release release, int userId)
        {
            var notification = new Notification
            {
                Description = releaseDescription + release.Major + "." + release.Minor + "." + release.Patch,
                Link = releaseLink,
                TypeOfNotification = NotificationType.ReleaseNotification,
                AddedBy = userId,
                Title = "New Release",
                AddedOn = release.ReleaseDate ?? DateTime.Now,
            };
            return AddNotification(notification, UserDataAccesor.GetUserId(notification.TypeOfNotification, userId));
        }

        /// <summary>
        /// Get list of notification
        /// </summary>
        /// <param name="userId">UseId</param>
        /// <returns>Returns list of notification.</returns>
        public List<Notification> GetNotification(int userId)
        {
            return NotificationDataAccesor.GetNotification(userId);
        }

        /// <summary>
        /// Function which takes feedback data and user list to whom notification is to be added,
        /// Calls AddNotification() to save in the database.
        /// </summary>
        /// <param name="feedback">Contain Feedback object as parameter.</param>
        /// <returns>Returns a boolean value as feedback notification is added successfully or not.</returns>
        public bool AddFeedbackNotification(Feedback feedback)
        {
            var notification = new Notification
            {
                Description = feedbackDescription + feedback.Title,
                Link = string.Format(feedbackLink, feedback.AddedFor.UserId),
                TypeOfNotification = NotificationType.FeedbackNotification,
                AddedBy = feedback.AddedBy.UserId,
                Title = "New Feedback",
                AddedOn =DateTime.Now,
            };
            return AddNotification(notification, UserDataAccesor.GetUserId(notification.TypeOfNotification, feedback.AddedFor.UserId));
        }
    }
}
