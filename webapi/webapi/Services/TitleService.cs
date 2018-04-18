using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapi.Dao;
using webapi.Models;

namespace webapi.Services
{
    public class TitleService
    {
        private readonly TitleDao titleDao = new TitleDao();
        private readonly UserDao userDao = new UserDao();

        public bool AddTitle(Title title)
        {
            //verify if the user is admin
            titleDao.AddTitle(title);

            return false;
        }

        public List<Title> GetAllTitles()
        {
            return titleDao.GetAllTitles();
        }

        public void RemoveTitle(Title title)
        {
            //verify if the user is admin

            titleDao.RemoveTitle(title);
        }
    }
}