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
        private readonly AccountService accountService = new AccountService();

        private readonly string RESERVED_NAME_DENOTING_ALL_ADMINS = "All Admins";
        private readonly string RESERVED_EMAIL_ADDRESS_DENOTING_ALL_ADMINS = "alladmins@acc.com";
        private readonly string RESERVED_NAME_DENOTING_EVERYONE = "All";

        private void AddNotification(Notification notification)
        {
            // add new notification only if the notfication is not already present
            if (!CheckIfNotificationExists(notification))
                notificationDao.AddNotification(notification.Type, notification.User.UserName, notification.Message, notification.Status, (notification.RelatedData == null) ? "" : notification.RelatedData);
        }

        public List<Notification> GetNotificationsForUser(string emailAddress)
        {
            var notificatioList = notificationDao.GetNotificationsForUser(emailAddress);

            if (accountService.CheckIfGivenEmailIsOfAdmin(emailAddress))
            {
                notificatioList.AddRange(notificationDao.GetNotificationsForUser(RESERVED_EMAIL_ADDRESS_DENOTING_ALL_ADMINS));
            }

            return notificatioList;
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
                    UserName = RESERVED_NAME_DENOTING_EVERYONE
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
                Message = "Please return " + bookName + " by " + dueDate.ToLongDateString() + "."
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

        public void GenerateNewBookIssueRequestNotifications(string bookRecipientName, string bookName)
        {
            AddNotification(new Notification()
            {
                Type = (int)NotificationType.ADMIN_NEW_BOOK_ISSUE_REQUEST,
                Status = (int)NotificationStatus.PENDING,
                RelatedData = bookRecipientName,
                User = new Users()
                {
                    // currently set such that all admins will get this notifications
                    UserName = RESERVED_NAME_DENOTING_ALL_ADMINS
                },
                Message = bookRecipientName + " wishes to issue " + bookName + "."
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