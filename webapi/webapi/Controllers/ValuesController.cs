using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace webapi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [Route("Foo")]
        [HttpGet]
        public List<string> Foo()
        {
            List<string> result = new List<string>();
            String CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                //String query1 = "Select * from tblEmployee";
                //String query2 = "Select * from tblEmployee Where Name like '" + TextBox1.Text + "%'";
                //String query3 = "Select * from tblEmployee Where Name like @EmployeeName";
                //String storedProc1 = "getEmployeeByName";

                String query1 = "Select * from dbo.roles";

                //SqlCommand cmd = new SqlCommand(storedProc1, conn);
                SqlCommand cmd = new SqlCommand(query1, conn);

                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Name", TextBox1.Text);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    result.Add((string)rdr["role_name"]);
                }
            }

            return result;
        }
    }
}
