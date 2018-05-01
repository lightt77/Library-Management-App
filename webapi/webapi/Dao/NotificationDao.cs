using System;
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

        public void AddNotification(int type, string userName, string message, int status)
        {
            string storedProcedure = "dbo.AddNotification";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@type", type },
                { "@user_name", userName },
                { "@message", message },
                { "@status", status }
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
                (row) => {
                    return new Rental()
                    {
                        BookName=(string)row["title_name"],
                        UserName=(string)row["user_name"],
                        ReturnDate=(DateTime)row["return_date"]
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
                (row) => {
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