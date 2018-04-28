using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.Dao;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [RoutePrefix("Books")]
    public class BookController : ApiController
    {
        private readonly BookService bookService = new BookService();

        [Route("all")]
        [HttpGet]
        public List<Book> GetAllBooks()
        {
            return bookService.GetAllBooks();
        }

        [HttpGet]
        [Route("genre")]
        public List<Book> GetBooksByGenre([FromUri]string genreName)
        {
            return bookService.GetBooksByGenre(genreName);
        }

        [HttpGet]
        [Route("author")]
        public List<Book> GetBooksByAuthor([FromUri]string authorName)
        {
            return bookService.GetBooksByAuthor(authorName);
        }

        [HttpPost]
        [Route("add")]
        public void AddBook([FromBody]Book book)
        {
            bookService.AddBook(book);
        }

        [HttpPost]
        [Route("delete")]
        public void DeleteBook([FromBody]Book book)
        {
            bookService.DeleteBook(book);
        }

        [HttpPost]
        [Route("users")]
        public List<Users> GetUsersForBook([FromBody]Book book)
        {
            return bookService.GetUsersForBook(book);
        }
    }
}
