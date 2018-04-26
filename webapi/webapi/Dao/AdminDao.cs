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

        public bool CheckIfTitleExists(string titleName)
        {
            string storedProcedure = "dbo.CheckTitleExists";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@title_name", titleName }
            };

            int titleCount = (int)connectionDao.RunRetrievalStoredProc(storedProcedure, parameterDictionary).Tables[0].Rows[0]["title_count"];

            return titleCount != 0;
        }

        public bool CheckIfUserExists(string userName)
        {
            string storedProcedure = "dbo.CheckUserExists";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@user_name", userName }
            };

            int userCount = (int)connectionDao.RunRetrievalStoredProc(storedProcedure, parameterDictionary).Tables[0].Rows[0]["user_count"];

            return userCount != 0;
        }
    }
}