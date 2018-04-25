using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using webapi.Models;

namespace webapi.Dao
{
    public class BookDao
    {
        private readonly String CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private readonly ConnectionDao connectionDao = new ConnectionDao();

        public List<Book> GetAllBooks()
        {
            string storedProcName = "dbo.GetAllBooks";
            var parameterDictionary = new Dictionary<string, object>();

            List<Book> bookList = new List<Book>();

            DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcName, parameterDictionary);

            DataTable titleTable = resultDataSet.Tables[0];
            DataTable genreTable = resultDataSet.Tables[1];

            return MapToBook(titleTable, genreTable);
            //    return resultDataSet.Tables[0].AsEnumerable().Select(x=>MapToBook(x)).ToList();
        }

        private List<Book> MapToBook(DataTable titleTable, DataTable genreTable)
        {
            List<Book> result = new List<Book>();

            for (int i = 0; i < titleTable.Rows.Count; i++)
            {
                Book book = new Book();

                int titleId = (int)titleTable.Rows[i]["title_id"];
                
                book.Price = (int)titleTable.Rows[i]["price"];
                book.Rating = (int)titleTable.Rows[i]["rating"];
                book.Title = (string)titleTable.Rows[i]["title_name"];
                book.Genre = new List<string>();
                book.Author= (string)titleTable.Rows[i]["author"];

                for (int j = 0; j < genreTable.Rows.Count; j++)
                {
                    if (titleId == (int)genreTable.Rows[j]["title_id"])
                        book.Genre.Add((string)genreTable.Rows[j]["genre_name"]);
                }

                result.Add(book);
            }
            return result;
        }

        public List<Book> GetBooksByGenre(string genreName)
        {
            List<Book> resultList = new List<Book>();

            string storedProcName = "dbo.GetBooksByGenre";
            var parameterDictionary = new Dictionary<string, object>();

            parameterDictionary.Add("@genre_name", genreName);

            DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcName, parameterDictionary);

            DataTable titleGenreJoinedTable = resultDataSet.Tables[0];

            return MapToBookFromTitleGenreTable(titleGenreJoinedTable);
        }

        private List<Book> MapToBookFromTitleGenreTable(DataTable titleGenreTable)
        {
            return titleGenreTable.AsEnumerable().Select(
                (row) =>
                {
                    Book book = new Book()
                    {
                        Author = (string)row["author"],
                        Price = (int)row["price"],
                        Rating = (int)row["rating"],
                        Title = (string)row["title_name"]
                    };

                    book.Genre.Add((string)row["genre_name"]);

                    return book;
                }
            ).ToList();
        }

        public void AddBook(Book book)
        {
            string storedProcName = "dbo.GetBooksByGenre";
            var parameterDictionary = new Dictionary<string, object>();

            int titleId = GetTitleIdByTitleName(book.Title, book.Author);

            if (titleId == 0)
            {
                //create a new title

            }
        }

        // returns titleId of the book if title exists else returns 0
        private int GetTitleIdByTitleName(string titleName, string authorName)
        {
            string storedProcName = "dbo.GetTitleIdForTitleNameAndAuthorName";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@title_name", titleName },
                { "@author_name", authorName }
            };

            DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcName, parameterDictionary);

            // checks if title is already present
            if (resultDataSet.Tables[0].Rows.Count != 0)
            {
                return (int)resultDataSet.Tables[0].Rows[0]["title_id"];
            }

            return 0;
        }
    }
}