using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace webapi.Dao
{
    public class ConnectionDao
    {
        private readonly String CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public DataSet RunRetrievalStoredProc(string storedProcName, Dictionary<string, object> parameterDict)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

                sqlDataAdapter.SelectCommand = new SqlCommand(storedProcName, con);

                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                foreach (var i in parameterDict)
                {
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue(i.Key, i.Value);
                }

                DataSet resultDataSet = new DataSet();

                sqlDataAdapter.Fill(resultDataSet);

                return resultDataSet;
            }
        }

        public void RunCUDStoredProc(string storedProcName, Dictionary<string, object> parameterDict)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

                sqlDataAdapter.SelectCommand = new SqlCommand(storedProcName, con);

                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                foreach (var i in parameterDict)
                {
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue(i.Key, i.Value);
                }

                DataSet resultDataSet = new DataSet();

                sqlDataAdapter.Fill(resultDataSet);

            }
        }
    }
}