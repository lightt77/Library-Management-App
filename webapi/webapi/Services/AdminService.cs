using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapi.Dao;

namespace webapi.Services
{
    public class AdminService
    {
        private readonly AdminDao adminDao = new AdminDao();

        public void AddRental(string titleName, string userName, int? noOfDaysToIssueFor)
        {
            if (adminDao.CheckIfTitleExists(titleName) && adminDao.CheckIfUserExists(userName))
            {
                // default issue perod is 15 days
                adminDao.AddRental(titleName, userName, noOfDaysToIssueFor == null ? 15 : noOfDaysToIssueFor);
            }
            else
            {
                // TODO
                // throw exceptions
            }
            
        }
    }
}