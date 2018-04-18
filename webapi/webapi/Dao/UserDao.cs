using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace webapi.Dao
{
    public class UserDao
    {
        private readonly String CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private readonly ConnectionDao connectionDao = new ConnectionDao();

        public string GetUserRole(string emailAddress)
        {
            string storedProcName = "dbo.GetRoleByEmailAddress";
            var parameterDictionary = new Dictionary<string, object>();

            parameterDictionary.Add("@email_address", emailAddress);

            DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcName, parameterDictionary);

            // check if no rows returned
            if ((resultDataSet.Tables[0].Rows.Count == 0))
                return "";

            return (string)resultDataSet.Tables[0].Rows[0]["role_name"];
        }
    }
}