using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapi.Dao;

namespace webapi.Services
{
    public class RentalService
    {
        private readonly RentalDao rentalDao = new RentalDao();
        private readonly UserDao userDao = new UserDao();
        private readonly BookDao bookDao = new BookDao();

        public void AddRental(string titleName, string userName, int? noOfDaysToIssueFor)
        {
            if (bookDao.CheckIfBookTitleExists(titleName) && userDao.CheckIfUserExists(userName))
            {
                // default issue perod is 15 days
                rentalDao.AddRental(titleName, userName, noOfDaysToIssueFor == null ? 15 : noOfDaysToIssueFor);
            }
            else
            {
                // TODO
                // throw exceptions
            }
            
        }
    }
}