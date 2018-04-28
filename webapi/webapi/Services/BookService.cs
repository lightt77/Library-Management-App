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
        private readonly BookDao bookDao = new BookDao();
        private readonly NotificationService notificationService = new NotificationService();

        public List<Book> GetAllBooks()
        {
            return bookDao.GetAllBooks();
        }

        public List<Book> GetBooksByGenre(string genreName)
        {
            return bookDao.GetBooksByGenre(genreName);
        }

        public List<Book> GetBooksByAuthor(string authorName)
        {
            return bookDao.GetBooksByAuthor(authorName);
        }

        public void AddBook(Book book)
        {
            //TODO: validate if the user is admin

            // add new title if the title doesnt already exist
            if (!bookDao.CheckIfBookTitleExists(book.Title))
            {
                //set default genre to General and default to 100
                if (book.Genre.Count == 0)
                {
                    AddTitle(book.Title, book.Author, book.Rating, book.Price == null ? 100 : book.Price, "General");
                }
                else
                {
                    AddTitle(book.Title, book.Author, book.Rating, book.Price == null ? 100 : book.Price, book.Genre[0]);

                    //for multiple genres
                    for (int i = 1; i < book.Genre.Count; i++)
                        bookDao.AddNewGenreToTitle(book.Title,book.Author,book.Genre[i]);
                }   
            }
            else
            {
                bookDao.AddBook(book.Title);
            }   
        }

        private void AddTitle(string title, string author, int rating, int? price, string genre)
        {
            bookDao.AddTitle(title, author, rating, price== null ? 100 : price, genre);
            notificationService.GenerateNewBookArrivalNotifications(title,author);
        }

        public void DeleteBook(Book book)
        {
            //validate if the user is admin
            
            bookDao.DeleteBook(book.Title, book.Author);
        }

        public List<Users> GetUsersForBook(Book book)
        {
            return bookDao.GetUsersForBook(book.Title);
        }
    }
}