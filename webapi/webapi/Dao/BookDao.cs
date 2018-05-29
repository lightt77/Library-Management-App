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
                book.Author = (string)titleTable.Rows[i]["author"];
                book.Quantity = (int)titleTable.Rows[i]["quantity"];

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
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@genre_name", genreName }
            };

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

        public List<Book> GetBooksByAuthor(string authorName)
        {
            List<Book> resultList = new List<Book>();

            string storedProcName = "dbo.GetBooksByAuthor";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@author_name", authorName }
            };

            DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcName, parameterDictionary);
            DataTable titleTable = resultDataSet.Tables[0];
            DataTable genreTable = resultDataSet.Tables[1];

            return MapToBook(titleTable, genreTable);
        }


        // by default also adds an entry in the book table if a new title is added
        public void AddTitle(string title, string author, int rating, int? price, string genre)
        {
            string storedProcName = "dbo.AddTitle";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@title_name", title },
                { "@author_name", author },
                { "@rating", rating },
                { "@price", price },
                { "@genre_name", genre },
            };

            connectionDao.RunCUDStoredProc(storedProcName, parameterDictionary);
        }

        public void AddBook(string title)
        {
            string storedProcName = "dbo.AddBook";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@title_name", title }
            };

            connectionDao.RunCUDStoredProc(storedProcName, parameterDictionary);
        }

        public void AddNewGenreToTitle(string title, string author, string genre)
        {
            string storedProcName = "dbo.AddNewGenreToTitle"; ;
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@title_name", title },
                { "@author_name", author },
                { "@genre_name", genre }
            };

            connectionDao.RunCUDStoredProc(storedProcName, parameterDictionary);
        }

        public void DeleteBook(string title, string author)
        {
            string storedProcName = "dbo.DeleteBook"; ;
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@title_name", title },
                { "@author_name", author }
            };

            connectionDao.RunCUDStoredProc(storedProcName, parameterDictionary);
        }

        public List<Users> GetUsersForBook(string title)
        {
            string storedProcName = "dbo.FindUsersForBook"; ;
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@title_name", title }
            };

            DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcName, parameterDictionary);

            DataTable userTable = resultDataSet.Tables[0];

            return MapToUsers(userTable);
        }

        private List<Users> MapToUsers(DataTable userTable)
        {
            return userTable.AsEnumerable().Select(
                    (row) =>
                    {
                        return new Users()
                        {
                            UserName = (string)row["user_name"],
                            EmailAddress = (string)row["email_address"],
                            MobileNumber = (string)row["mobile_number"],
                            ResidentialAddress = row["residential_address"].ToString(),
                            Role = (string)row["role_name"]
                        };
                    }
            ).ToList();
        }

        public bool CheckIfBookTitleExists(string titleName)
        {
            string storedProcedure = "dbo.CheckTitleExists";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@title_name", titleName }
            };

            int resultCount = (int)connectionDao.RunRetrievalStoredProc(storedProcedure, parameterDictionary).Tables[0].Rows[0]["title_count"];

            return resultCount != 0;
        }

        public bool CheckIfTitleIsAvailable(string titleName)
        {
            string storedProcedure = "dbo.CheckIfBookIsAvailable";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@title_name", titleName }
            };

            int resultCount = (int)connectionDao.RunRetrievalStoredProc(storedProcedure, parameterDictionary).Tables[0].Rows[0]["count"];

            return resultCount != 0;
        }

        public bool CheckIfBookForGivenTitleIsAvailable(string titleName)
        {
            string storedProcedure = "dbo.CheckIfBookIsAvailable";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@title_name", titleName }
            };

            int resultCount = (int)connectionDao.RunRetrievalStoredProc(storedProcedure, parameterDictionary).Tables[0].Rows[0]["count"];

            return resultCount != 0;
        }

        public List<Book> GetBooksWithUser(string emailAddress)
        {
            string storedProcedure = "dbo.GetBooksWithUser";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@user_email_address", emailAddress }
            };

            DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcedure, parameterDictionary);

            return resultDataSet.Tables[0].AsEnumerable().Select(
                    (row) =>
                    {
                        return new Book()
                        {
                            Author = (string)row["author"],
                            Title = (string)row["title_name"],
                        };
                    }
                ).ToList();

        }

        public void ReturnBookForUser(string userEmailAddress, string bookName)
        {
            string storedProcedure = "dbo.ReturnBook";
            var parameterDictionary = new Dictionary<string, object>
            {
                { "@user_email_address", userEmailAddress },
                { "@book_name", bookName }
            };

            connectionDao.RunCUDStoredProc(storedProcedure, parameterDictionary);
        }
    }
}