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

        public List<Title> GetAllBooks()
        {
            string storedProcName = "dbo.GetAllBooks";
            var parameterDictionary = new Dictionary<string, object>();

            List<Title> titleList = new List<Title>();

            DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcName, parameterDictionary);

            for (int i = 0; i < resultDataSet.Tables[0].Rows.Count; i++)
            {
                Title title = new Title();
                
                if ((resultDataSet.Tables[0].Rows[i]["price"]).GetType().Name.Equals("DBNull"))
                    title.Price = 117;
                titleList.Add(new Title()
                {

                    TitleId = (int)resultDataSet.Tables[0].Rows[i]["title_id"],
                    TitleName = (string)resultDataSet.Tables[0].Rows[i]["title_name"],
                    Author = (string)resultDataSet.Tables[0].Rows[i]["author"],
                    Rating = (int)resultDataSet.Tables[0].Rows[i]["rating"],
                    Quantity = (int)resultDataSet.Tables[0].Rows[i]["quantity"],
                    Price = title.Price
                    //Price = (int)resultDataSet.Tables[0].Rows[i]["price"],
                    //CreatedOn = (DateTime)resultDataSet.Tables[0].Rows[i]["created_on"],
                    //LastUpdated = (DateTime)resultDataSet.Tables[0].Rows[i]["last_updated"]
                });
            }

            return titleList;
        }

        public List<Title> GetBookByGenre(string genreName)
        {
            string storedProcName = "dbo.GetBooksByGenre";

            var parameterDictionary = new Dictionary<string, object>();
            parameterDictionary.Add("@genre_name", genreName);

            List<Title> titleList = new List<Title>();

            DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcName, parameterDictionary);

            for (int i = 0; i < resultDataSet.Tables[0].Rows.Count; i++)
            {
                titleList.Add(new Title()
                {
                    TitleId = (int)resultDataSet.Tables[0].Rows[i]["title_id"],
                    TitleName = (string)resultDataSet.Tables[0].Rows[i]["title_name"]
                });
            }

            return titleList;
        }
    }
}