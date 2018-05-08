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
            // add new notification only if the notfication is not already present
            if(!CheckIfNotificationExists(notification))
                notificationDao.AddNotification(notification.Type, notification.User.UserName, notification.Message, notification.Status);
        }

        // called when a new title is added
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
                Message = "" + title + " by " + author + " is now available!"
            });
        }

        // called by cron job
        public void GenerateBookReturnDateDueNotifications(string bookName, string userName, DateTime dueDate)
        {
            AddNotification(new Notification()
            {
                Type = (int)NotificationType.BOOK_RETURN_DATE_DUE,
                Status = (int)NotificationStatus.PENDING,
                User = new Users()
                {
                    UserName = userName
                },
                Message = "Please return " + bookName + " by " + dueDate.ToLongDateString()+"."
            });
        }

        // called by cron job
        public void GenerateBookInWishlistAvailableNotifications(string bookName, string userName)
        {
            AddNotification(new Notification()
            {
                Type = (int)NotificationType.BOOK_IN_WISHLIST_AVAILABLE,
                Status = (int)NotificationStatus.PENDING,
                User = new Users()
                {
                    UserName = userName
                },
                Message = "" + bookName + " from your wishlist is now available" + "."
            });
        }

        public void GenerateNewBookIssuerequestNotifications(string recipientName, string bookName)
        {
            AddNotification(new Notification()
            {
                Type = (int)NotificationType.ADMIN_NEW_BOOK_ISSUE_REQUEST,
                Status = (int)NotificationStatus.PENDING,
                User = new Users()
                {
                    UserName = recipientName
                },
                Message = "" + bookName + " from your wishlist is now available" + "."
            });
        }

        public List<Rental> GetAllBookReturnDateDueRecords()
        {
            return notificationDao.GetAllBookReturnDateDueRecords();
        }

        public List<Rental> GetBooksInWishlistAvailableRecords()
        {
            return notificationDao.GetBooksInWishlistAvailableRecords();
        }

        public bool CheckIfNotificationExists(Notification notification)
        {
            return notificationDao.CheckIfNotificationExists(notification.User.UserName, notification.Type, notification.Message);
        }
    }

    public enum NotificationType
    {
        NEW_BOOK_ARRIVAL,
        BOOK_RETURN_DATE_DUE,
        BOOK_IN_WISHLIST_AVAILABLE,
        ADMIN_NEW_BOOK_ISSUE_REQUEST
    }

    public enum NotificationStatus
    {
        PENDING,
        DELIVERED
    }
}