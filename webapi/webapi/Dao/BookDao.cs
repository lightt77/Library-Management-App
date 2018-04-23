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

        // uncomment later
        //public List<Title> GetBookByGenre(string genreName)
        //{
        //    string storedProcName = "dbo.GetBooksByGenre";

        //    var parameterDictionary = new Dictionary<string, object>();
        //    parameterDictionary.Add("@genre_name", genreName);

        //   // List<Title> titleList = new List<Title>();

        //    DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcName, parameterDictionary);
        //    return resultDataSet.Tables[0].AsEnumerable().Select(x => new Title()
        //    {
        //        TitleId = x.Field<int>("title_id"),
        //        TitleName = x.Field<string>("title_name")
        //    }).ToList();
        //    //for (int i = 0; i < resultDataSet.Tables[0].Rows.Count; i++)
        //    //{
        //    //    titleList.Add(new Title()
        //    //    {
        //    //        TitleId = (int)resultDataSet.Tables[0].Rows[i]["title_id"],
        //    //        TitleName = (string)resultDataSet.Tables[0].Rows[i]["title_name"]
        //    //    });
        //    //}

        //    //return titleList;
        //}

    
    }
}