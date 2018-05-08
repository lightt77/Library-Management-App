using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using webapi.Models;
using webapi.Services;
using System.Web;
using System.Net.Http.Headers;
using System;
using System.Web.SessionState;

namespace webapi.Controllers
{
    public class AccountController : ApiController, IRequiresSessionState, IReadOnlySessionState
    {
        private AccountService accountService = new AccountService();

        [EnableCors(origins: "http://127.0.0.1:5500", headers: "*", methods: "*")]
        //[WebMethod(EnableSession=true)]
        [Route("users/login")]
        [HttpPost]
        public HttpResponseMessage Login([FromBody]Users user)
        {
            if (accountService.ValidateLoginFields(user) == false)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.Unauthorized,
                };

            }

            if (accountService.ValidateLogin(user) == true)
            {
                // save the user in session
                //session[user.EmailAddress] = user;
                //if (HttpContext.Current.Session == null)
                //    throw new Exception("Session is null");

                //HttpContext.Current.Session["email"] = user;

                // add a cookie to the response with email address of the user
                //var cookie = new CookieHeaderValue("session-id", HttpContext.Current.Session.SessionID);
                var cookie = new CookieHeaderValue("logged-in", "true");
                cookie.Expires = DateTimeOffset.Now.AddDays(1);
                cookie.Domain = Request.RequestUri.Host;
                cookie.Path = "/";

                var response = new HttpResponseMessage();

                response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                response.StatusCode = System.Net.HttpStatusCode.Accepted;

                return response;
            }

            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized
            };
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }

        [EnableCors(origins: "http://127.0.0.1:5500", headers: "*", methods: "*")]
        [Route("users/register")]
        [HttpPost]
        public HttpResponseMessage Register([FromBody]Users user)
        {
            return new HttpResponseMessage()
            {
                StatusCode = accountService.Register(user)
            };
        }

    }
}