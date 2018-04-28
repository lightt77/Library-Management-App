using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}