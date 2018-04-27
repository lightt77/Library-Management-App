using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapi.Dao;

namespace webapi.Services
{
    public class UserServices
    {
        private readonly UserDao userDao = new UserDao();
        private readonly BookDao bookDao = new BookDao();

        public void AddToWishList(string userName, string bookName)
        {
            if (userDao.CheckIfUserExists(userName) && bookDao.CheckIfBookExists(bookName))
            {
                userDao.AddToWishList(userName,bookName);
            }
            else
            {
                //TODO
                //throw exceptions
            }
        }
    }
}