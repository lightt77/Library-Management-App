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
        private readonly BookDao bookDao = new BookDao();
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

        [HttpGet]
        [Route("GetBookQuantityByTitleName")]
        public int GetBookQuantityByTitleName(string TitleName)
        {
            var result = new List<string>();
            int quantity = 0;

            String CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                String storedProc1 = "dbo.GetBookQuantityByTitleName";
                SqlCommand cmd = new SqlCommand(storedProc1, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@title_name", TitleName);
                conn.Open();
                quantity = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return quantity;
        }
    }
}
