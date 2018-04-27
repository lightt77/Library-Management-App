using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace webapi.Dao
{
    public class AdminDao
    {
        private readonly ConnectionDao connectionDao = new ConnectionDao();

        public void AddRental(string titleName, string userName, int? noOfDaysToIssueFor)
        {
            string storedProcedure = "dbo.AddRental";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@user_name", userName },
                { "@title_name", titleName },
                { "@no_of_days", noOfDaysToIssueFor }
            };

            connectionDao.RunCUDStoredProc(storedProcedure, parameterDictionary);
        }
    }
}