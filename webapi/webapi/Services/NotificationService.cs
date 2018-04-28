using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapi.Dao;
using webapi.Models;

namespace webapi.Services
{
    public class NotificationService
    {
        private readonly NotificationDao notificationDao = new NotificationDao();

        private void AddNotification(Notification notification)
        {
            notificationDao.AddNotification(notification.Type, notification.User.UserName, notification.Message, notification.Status);
        }

        public void GenerateNewBookArrivalNotifications(string title, string author)
        {
            AddNotification(new Notification()
            {
                Type = (int)NotificationType.NEW_BOOK_ARRIVAL,
                Status = (int)NotificationStatus.PENDING,
                User = new Users()
                {
                    UserName = "All"
                },
                Message = "" + title + " by " + author + " is now available!",
            });
        }

        // this should be called by cron job
        public void GenerateBookReturnDateDueNotifications(string bookName, string userName, DateTime dueDate)
        {
            AddNotification(new Notification()
            {

            });
        }
    }

    public enum NotificationType
    {
        NEW_BOOK_ARRIVAL,
        BOOK_RETURN_DATE_DUE,
        BOOK_IN_WISHLIST_AVAILABLE
    }

    public enum NotificationStatus
    {
        PENDING,
        DELIVERED
    }
}