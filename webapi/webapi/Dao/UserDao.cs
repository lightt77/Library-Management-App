using System;
using System.Collections.Generic;
using System.Data;
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
            string storedProcedure = "dbo.AddToWishList";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@user_name", userName },
                { "@title_name", bookName }
            };

            connectionDao.RunCUDStoredProc(storedProcedure, parameterDictionary);
        }

        public bool CheckIfWishListEntryExists(string userName, string bookName)
        {
            string storedProcedure = "dbo.CheckIfWishlistEntryExists";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@user_name", userName },
                { "@title_name", bookName }
            };

            DataSet resultDataSet=connectionDao.RunRetrievalStoredProc(storedProcedure, parameterDictionary);

            int entryCount = (int)resultDataSet.Tables[0].Rows[0]["result_count"];

            return entryCount > 0;
        }
    }
}