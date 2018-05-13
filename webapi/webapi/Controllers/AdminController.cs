using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    public class AdminController : ApiController
    {
        private readonly RentalService adminService = new RentalService();

        [Route("rental/title/{titleName}/user/{userName}/noOfDays/{noOfDays}")]
        [HttpPut]
        public void AddRental([FromUri]string titleName, [FromUri]string userName, [FromUri] int? noOfDays)
        {
            // validate if the user is admin
            adminService.AddRental(titleName, userName, noOfDays);
        }
    }
}
