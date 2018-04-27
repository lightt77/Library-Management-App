using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapi.Dao;

namespace webapi.Services
{
    public class UserService
    {
        private readonly UserDao userDao = new UserDao();
        private readonly BookDao bookDao = new BookDao();

        public void AddToWishList(string userName, string bookName)
        {
            if (!userDao.CheckIfUserExists(userName))
            {
                //TODO
                return;
            }

            if (!bookDao.CheckIfBookExists(bookName))
            {
                //TODO
                return;
            }

            if (userDao.CheckIfWishListEntryExists(userName,bookName))
            {
                //TODO
                return;
            }

            userDao.AddToWishList(userName, bookName);
        }
    }
}