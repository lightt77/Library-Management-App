using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using webapi.Models;

namespace webapi.Dao
{
    public class RentalDao
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

        public void HandleRental(string titleName, string userName, int rentalStatus)
        {
            string storedProcedure = "dbo.HandleRentalRequest";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@user_name", userName },
                { "@title_name", titleName },
                { "@rental_status", rentalStatus}
            };

            connectionDao.RunCUDStoredProc(storedProcedure, parameterDictionary);
        }

        public List<Rental> GetPendingRentalRequests()
        {
            string storedProcedure = "dbo.GetAllPendingRequests";
            var parameterDictionary = new Dictionary<string, object>();

            DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcedure, parameterDictionary);

            return resultDataSet.Tables[0].AsEnumerable().Select(
                    (row) =>
                    {
                        return new Rental()
                        {
                            BookName = (string)row["title_name"],
                            UserName = (string)row["user_name"],
                            IssueDate = (DateTime)row["issue_date"],
                            ReturnDate = (DateTime)row["return_date"],
                            RentalStatus = (int)row["rental_status"]

                        };
                    }
                ).ToList();
        }
    }
}