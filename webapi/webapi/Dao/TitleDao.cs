using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using webapi.Models;

namespace webapi.Dao
{
    public class TitleDao
    {
        private readonly String CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private readonly ConnectionDao connectionDao = new ConnectionDao();

        public List<Title> GetAllTitles()
        {
            string storedProcName = "dbo.GetAllBooks";
            var parameterDictionary = new Dictionary<string, object>();

            List<Title> titleList = new List<Title>();

            DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcName, parameterDictionary);

            for (int i = 0; i < resultDataSet.Tables[0].Rows.Count; i++)
            {
                Title title = new Title();

                if (!(resultDataSet.Tables[0].Rows[i]["title_id"]).GetType().Name.Equals("DBNull"))
                    title.TitleId = (int)(resultDataSet.Tables[0].Rows[i]["title_id"]);
                if (!(resultDataSet.Tables[0].Rows[i]["title_name"]).GetType().Name.Equals("DBNull"))
                    title.TitleName = (string)(resultDataSet.Tables[0].Rows[i]["title_name"]);
                if (!(resultDataSet.Tables[0].Rows[i]["author"]).GetType().Name.Equals("DBNull"))
                    title.Author = (string)(resultDataSet.Tables[0].Rows[i]["author"]);
                if (!(resultDataSet.Tables[0].Rows[i]["rating"]).GetType().Name.Equals("DBNull"))
                    title.Rating = (int)(resultDataSet.Tables[0].Rows[i]["rating"]);
                if (!(resultDataSet.Tables[0].Rows[i]["quantity"]).GetType().Name.Equals("DBNull"))
                    title.Quantity = (int)(resultDataSet.Tables[0].Rows[i]["quantity"]);
                if (!(resultDataSet.Tables[0].Rows[i]["price"]).GetType().Name.Equals("DBNull"))
                    title.Price = (int)(resultDataSet.Tables[0].Rows[i]["price"]);

                titleList.Add(title);
            }

            return titleList;
        }

        public bool AddTitle(Title title)
        {
            string storedProcName = "dbo.AddTitle";
            Dictionary<string, object> parameterDictionary = new Dictionary<string, object>();
            
            if (GetTitleIdByTitleName(title.TitleName) == 0)
            {
                parameterDictionary.Add("@title_name", title.TitleName);
                parameterDictionary.Add("@author_name", title.Author);
                parameterDictionary.Add("@rating", title.Rating);
                parameterDictionary.Add("@quantity", title.Quantity);
                parameterDictionary.Add("@price", title.Price);

                connectionDao.RunCUDStoredProc(storedProcName,parameterDictionary);
            }

            int titleId = GetTitleIdByTitleName(title.TitleName);

            parameterDictionary.Clear();

            parameterDictionary.Add("@title_id", titleId);

            connectionDao.RunCUDStoredProc("dbo.AddBook", parameterDictionary);
            
            return true;
        }

        public int GetTitleIdByTitleName(string titleName)
        {
            string storedProcName = "dbo.GetTitleIdByTitleName";
            var parameterDictionary = new Dictionary<string, object>();

            parameterDictionary.Add("@title_name", titleName);

            DataSet resultDataSet = connectionDao.RunRetrievalStoredProc(storedProcName, parameterDictionary);

            if ((resultDataSet.Tables[0].Rows.Count==0))
                return 0;

            return (int)resultDataSet.Tables[0].Rows[0]["title_id"];
        }

        public void RemoveTitle(Title title)
        {
            string storedProcName = "dbo.DeleteTitle";
            var parameterDictionary = new Dictionary<string, object>();

            parameterDictionary.Add("@title_name", title.TitleName);

            connectionDao.RunCUDStoredProc(storedProcName, parameterDictionary);
        }
        
    }
}