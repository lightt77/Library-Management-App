using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace webapi.Dao
{
    public class AccountDao
    {
        private readonly ConnectionDao connectionDao = new ConnectionDao();

        public string GetPasswordForEmailAddress(string emailAddress)
        {
            string storedProcName = "dbo.GetUserDetailsForLoginValidation";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@email_address", emailAddress }
            };

            DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcName, parameterDictionary);

            // returns empty string if no password is found
            if (resultDataSet.Tables[0].Rows.Count == 0)
                return "";

            return (string)resultDataSet.Tables[0].Rows[0]["password"];
        }

        public void AddNewUserRegistration(string name, string emailAddress, string password, string mobileNo, string residentialAddress)
        {
            // set default residential address to NA
            if (residentialAddress == null)
                residentialAddress = "NA";

            string storedProcName = "dbo.AddNewUserRegistration";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@name", name },
                { "@email", emailAddress },
                { "@password", password },
                { "@mobile", mobileNo },
                { "@residential_address", residentialAddress}
            };

            connectionDao.RunCUDStoredProc(storedProcName, parameterDictionary);
        }

        public bool CheckIfEmailAddressAlreadyExists(string emailAddress)
        {
            string storedProcName = "dbo.CheckIfEmailAddressAlreadyExists";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@email_address", emailAddress }
            };

            DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcName, parameterDictionary);

            int resultCount = (int)resultDataSet.Tables[0].Rows[0]["count"];

            return resultCount > 0;
        }
    }
}