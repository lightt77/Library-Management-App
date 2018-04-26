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

        [HttpPost]
        [Route("add")]
        public void AddBook([FromBody]Book book)
        {
            bookService.AddBook(book);
        }

        [HttpGet]
        [Route("")]
        public List<string> GetBooksByAuthor([FromUri]string AuthorName)
        {
            var result = new List<string>();

            String CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                String storedProc1 = "dbo.GetBooksByAuthor";
                SqlCommand cmd = new SqlCommand(storedProc1, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@author_name", AuthorName);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    result.Add((string)rdr["title_name"]);
                }
            }

            return result;
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

        //uncomment later
        //[HttpPost]
        //[Route("AddBook")]
        ////public string AddTitle([FromBody]string titleName, [FromBody]string author, [FromBody]int rating, [FromBody]int quantity, [FromBody]int price)
        //public string AddTitle([FromBody]Title title)
        //{
        //    //validate if the user is admin

        //    string result = "Title added..";

        //    String CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        //    using (SqlConnection conn = new SqlConnection(CS))
        //    {
        //        String storedProc1 = "dbo.AddTitle";
        //        SqlCommand cmd = new SqlCommand(storedProc1, conn);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@title_name", title.TitleName);
        //        cmd.Parameters.AddWithValue("@author_name", title.Author);
        //        cmd.Parameters.AddWithValue("@rating", title.Rating);
        //        cmd.Parameters.AddWithValue("@quantity", title.Quantity);
        //        cmd.Parameters.AddWithValue("@price", title.Price);

        //        conn.Open();
        //        var rdr = cmd.ExecuteNonQuery();
        //    }

        //    return result;
        //}

        //[HttpGet]
        //[Route("Delete")]
        //public string DeleteTitle(string titleName)
        //{
        //    //validate if user is admin

        //}


    }
}
