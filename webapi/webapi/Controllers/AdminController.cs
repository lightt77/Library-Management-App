using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    public class AdminController : ApiController
    {
        private readonly RentalService rentalService = new RentalService();
        private AccountService accountService = new AccountService();

        //[Route("rental/title/{titleName}/user/{userName}/noOfDays/{noOfDays}")]
        //[HttpPut]
        //public void AddRental([FromUri]string titleName, [FromUri]string userName, [FromUri] int? noOfDays)
        //{
        //    // validate if the user is admin
        //    adminService.AddRental(titleName, userName, noOfDays);
        //}

        [EnableCors(origins: "http://127.0.0.1:5500", headers: "*", methods: "*", SupportsCredentials = true)]
        [Route("pending-requests")]
        [HttpGet]
        public List<Rental> GetPendingRentalRequests()
        {
            string userEmailAddress;

            if (Request.Headers.GetValues("EmailId").Count() == 0)
            {
                // return empty list
                return new List<Rental>();
            }

            userEmailAddress = Request.Headers.GetValues("EmailId").First();

            // validate if user is an admin
            if (!accountService.CheckIfGivenEmailIsOfAdmin(userEmailAddress))
            {
                // return empty list
                return new List<Rental>();
            }

            return rentalService.GetPendingRentalRequests();
        }

        [EnableCors(origins: "http://127.0.0.1:5500", headers: "*", methods: "*", SupportsCredentials = true)]
        [Route("reject-request")]
        [HttpPut]
        public HttpResponseMessage RejectRental([FromBody]Rental rental)
        {
            string currentUserEmailAddress;

            if (Request.Headers.GetValues("EmailId").Count() == 0)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }

            currentUserEmailAddress = Request.Headers.GetValues("EmailId").First();

            // validate if user is an admin
            if (!accountService.CheckIfGivenEmailIsOfAdmin(currentUserEmailAddress))
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }

            rentalService.RejectRental(rental.UserName, rental.BookName);

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
        }

        [EnableCors(origins: "http://127.0.0.1:5500", headers: "*", methods: "*", SupportsCredentials = true)]
        [Route("approve-request")]
        [HttpPut]
        public HttpResponseMessage ApproveRental([FromBody]Rental rental)
        {
            string currentUserEmailAddress;

            if (Request.Headers.GetValues("EmailId").Count() == 0)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }

            currentUserEmailAddress = Request.Headers.GetValues("EmailId").First();

            // validate if user is an admin
            if (!accountService.CheckIfGivenEmailIsOfAdmin(currentUserEmailAddress))
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }

            rentalService.ApproveRental(rental.UserName, rental.BookName);

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
