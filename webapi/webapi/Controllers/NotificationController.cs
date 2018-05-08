using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using webapi.Models;
using System.Web;

namespace webapi.Controllers
{
    public class NotificationController : ApiController
    {
        [EnableCors(origins: "http://127.0.0.1:5500", headers: "*", methods: "*", SupportsCredentials = true)]
        [HttpGet]
        [Route("users/notifications")]
        public HttpResponseMessage GetAllNotificationsForUser()
        {
            //var a = Request.Headers;

            var sessionId = Request.Headers.GetValues("SessionId").First();

            var user=HttpContext.Current.Session[sessionId];

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Accepted
            };
        }
    }
}
