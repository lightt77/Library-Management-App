using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using webapi.Models;

namespace webapi.Dao
{
    public class BookDao
    {
        private readonly String CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        
        public List<Title> GetAllBooks()
        {
            List<Title> titleList = new List<Title>();



            return titleList;
        }
    }
}