using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapi.Dao;
using webapi.Models;

namespace webapi.Services
{
    public class BookService
    {
        BookDao bookDao = new BookDao();

        public List<Book> GetAllBooks()
        {
            return bookDao.GetAllBooks();
        }

        public List<Book> GetBooksByGenre(string genreName)
        {
            return bookDao.GetBooksByGenre(genreName);
        }
    }
}