using System.Collections.Generic;
using TrainingTracker.Common.Entity;

namespace TrainingTracker.DAL.Interface
{
    public interface INotificationDal
    {
        /// <summary>
        /// Adds notification in Notification tables.
        /// </summary>
        /// <param name="description"></param>
        /// <param name="link"></param>
        /// <returns>bool</returns>
        bool AddNotification(Notification notification, List<int> userIds);


        /// <summary>
        /// Returns list of unseen notification.
        /// </summary>
        /// <returns></returns>
        List<Notification> GetNotification(int userId);

        /// <summary>
        /// Update notification.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool UpdateNotification(int userId, Notification notification);

        /// <summary>
        /// update All Notification as read
        /// </summary>
        /// <param name="userId">userid</param>
        /// <returns>sucess event for flag</returns>
        bool MarkAllNotificationAsRead(int userId);


    }
}
