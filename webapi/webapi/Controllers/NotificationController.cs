using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using webapi.Models;
using System.Web;
using webapi.Services;

namespace webapi.Controllers
{
    public class NotificationController : ApiController
    {
        private NotificationService notificationService = new NotificationService();
        private AccountService accountService = new AccountService();

        [EnableCors(origins: "http://127.0.0.1:5500", headers: "*", methods: "*", SupportsCredentials = true)]
        [HttpGet]
        [Route("users/notifications")]
        //public HttpResponseMessage GetAllNotificationsForUser()
        public List<Notification> GetAllNotificationsForUser()
        {
            //var sessionId = Request.Headers.GetValues("SessionId").First();

            //var user=HttpContext.Current.Session[sessionId];

            // TODO: dont use email id for authentication once session part is done
            // check if email id is available on request header
            if (Request.Headers.GetValues("EmailId").Count() == 0)
            {
                //return new HttpResponseMessage()
                //{
                //    StatusCode = HttpStatusCode.BadRequest
                //};

                return new List<Notification>();
            }

            var currentUserEmailAddress = Request.Headers.GetValues("EmailId").First();

            //return new HttpResponseMessage()
            //{
            //    StatusCode = HttpStatusCode.Accepted,
            //    Content=new HttpContent
            //};

            return notificationService.GetNotificationsForUser(currentUserEmailAddress);
        }
    }
}
