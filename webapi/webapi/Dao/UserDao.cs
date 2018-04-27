using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Dao
{
    public class UserDao
    {
        private readonly ConnectionDao connectionDao = new ConnectionDao();

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

        public void AddToWishList(string userName, string bookName)
        {
            // change this and test the api
            string storedProcedure = "dbo.CheckUserExists";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@user_name", userName },
                { "@title_name", bookName }
            };

            connectionDao.RunCUDStoredProc(storedProcedure, parameterDictionary);
        }
    }
}