﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [RoutePrefix("Books")]
    public class BookController : ApiController
    {
        private readonly BookService bookService = new BookService();
        private readonly RentalService rentalService = new RentalService();
        private readonly AccountService accountService = new AccountService();

        [EnableCors(origins: "http://127.0.0.1:5500", headers: "*", methods: "*")]
        [Route("all")]
        [HttpGet]
        public List<Book> GetAllBooks()
        {
            var data = (Dictionary<string, object>)HttpContext.Current.Session[HttpContext.Current.Session.SessionID];

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

        [EnableCors(origins: "http://127.0.0.1:5500", headers: "*", methods: "*")]
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
            //validate if admin
            return bookService.GetUsersForBook(book);
        }

        [HttpGet]
        [EnableCors(origins: "http://127.0.0.1:5500", headers: "*", methods: "*")]
        [Route("shelf")]
        public List<Book> GetBooksWithUser()
        {
            string userEmailAddress;

            if (Request.Headers.GetValues("EmailId").Count() == 0)
            {
                // return empty list
                return new List<Book>();
            }

            userEmailAddress = Request.Headers.GetValues("EmailId").First();

            return bookService.GetBooksWithUser(userEmailAddress);
        }

        [HttpPost]
        [EnableCors(origins: "http://127.0.0.1:5500", headers: "*", methods: "*")]
        [Route("issue")]
        public HttpResponseMessage IssueBook([FromBody]Book book)
        {
            string userEmailAddress;

            if (Request.Headers.GetValues("EmailId").Count() == 0)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            userEmailAddress = Request.Headers.GetValues("EmailId").First();

            // validate if book is available
            if (!bookService.CheckForBookAvailability(book.Title))
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Forbidden
                };
            }

            string userName = accountService.GetNameOfCurrentlyLoggedInUser(userEmailAddress);

            //TODO: check if rental does not already exists

            // add rental with status unapproved
            rentalService.AddRental(book.Title, userName, null);

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Accepted
            };
        }

        [EnableCors(origins: "http://127.0.0.1:5500", headers: "*", methods: "*")]
        [Route("return")]
        [HttpPost]
        public HttpResponseMessage AddToWishList([FromBody]Book book)
        {
            string userEmailAddress;

            if (Request.Headers.GetValues("EmailId").Count() == 0)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            userEmailAddress = Request.Headers.GetValues("EmailId").First();

            bookService.ReturnBook(book, userEmailAddress);

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Accepted
            };
        }
    }
}
