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

        public List<Book> GetBooksByAuthor(string authorName)
        {
            return bookDao.GetBooksByAuthor(authorName);
        }

        public void AddBook(Book book)
        {
            //validate if the user is admin

            if (book.Genre.Count == 0)
            {
                bookDao.AddBook(book.Title, book.Author, book.Price == null ? 100 : book.Price, book.Rating, "General");
            }
            else
            {
                bookDao.AddBook(book.Title, book.Author, book.Price == null ? 100 : book.Price, book.Rating, book.Genre[0]);

                // for multiple genres
                for (int i = 1; i < book.Genre.Count; i++)
                {
                    bookDao.AddNewGenreToTitle(book.Title, book.Author, book.Genre[i]);
                }
            }
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