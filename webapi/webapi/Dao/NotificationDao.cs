﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using webapi.Models;

namespace webapi.Dao
{
    public class NotificationDao
    {
        private readonly ConnectionDao connectionDao = new ConnectionDao();

        public List<Notification> GetNotificationsForUser(string emailAddress)
        {
            string storedProcedure = "dbo.GetNotificationForUser";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@email_address", emailAddress}
            };

            DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcedure, parameterDictionary);

            return resultDataSet.Tables[0].AsEnumerable().Select(
                (row) =>
                {
                    return new Notification()
                    {
                        Type = (int)row["notification_type"],
                        Status = (int)row["notification_status"],
                        Message = (string)row["notification_message"],
                        LastUpdateDate = (DateTime)row["last_updated"]
                    };
                }
            ).ToList();

        }

        public void AddNotification(int type, string userName, string message, int status, string relatedData)
        {
            string storedProcedure = "dbo.AddNotification";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@type", type },
                { "@user_name", userName },
                { "@message", message },
                { "@status", status },
                { "@related_data", relatedData }
            };

            connectionDao.RunCUDStoredProc(storedProcedure, parameterDictionary);
        }

        public bool CheckIfNotificationExists(string userName, int notificationType, string message)
        {
            string storedProcedure = "dbo.CheckIfNotificationExists";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@user_name", userName },
                { "@message", message },
                { "@notification_type", notificationType}
            };

            DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcedure, parameterDictionary);

            int resultCount = (int)resultDataSet.Tables[0].Rows[0]["result_count"];

            return resultCount > 0;
        }

        public List<Rental> GetAllBookReturnDateDueRecords()
        {
            string storedProcedure = "dbo.GetAllBookReturnDateDueRecords";
            var parameterDictionary = new Dictionary<string, object>();

            DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcedure, parameterDictionary);

            return resultDataSet.Tables[0].AsEnumerable().Select(
                (row) =>
                {
                    return new Rental()
                    {
                        BookName = (string)row["title_name"],
                        UserName = (string)row["user_name"],
                        ReturnDate = (DateTime)row["return_date"]
                    };
                }
            ).ToList();
        }

        public List<Rental> GetBooksInWishlistAvailableRecords()
        {
            string storedProcedure = "dbo.GetAllBooksInWishlistAvailableRecords";
            var parameterDictionary = new Dictionary<string, object>();

            DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcedure, parameterDictionary);

            return resultDataSet.Tables[0].AsEnumerable().Select(
                (row) =>
                {
                    return new Rental()
                    {
                        BookName = (string)row["title_name"],
                        UserName = (string)row["user_name"]
                    };
                }
            ).ToList();
        }


        public List<Rental> GetNewRentalRequests()
        {
            string storedProcedure = "dbo.GetNewRentalRequests";
            var parameterDictionary = new Dictionary<string, object>();

            DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcedure, parameterDictionary);

            return resultDataSet.Tables[0].AsEnumerable().Select(
                (row) =>
                {
                    return new Rental()
                    {
                        BookName = (string)row["title_name"],
                        UserName = (string)row["user_name"]
                    };
                }
            ).ToList();
        }
    }
}