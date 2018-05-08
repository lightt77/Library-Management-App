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

                //HttpContext.Current.Session[HttpContext.Current.Session.SessionID] = user;
                HttpContext.Current.Session.Add(HttpContext.Current.Session.SessionID, user);

                var savedUser = HttpContext.Current.Session[HttpContext.Current.Session.SessionID];
                // add a cookie to the response with email address of the user
                //var cookie = new CookieHeaderValue("session-id", HttpContext.Current.Session.SessionID);

                // TODO: change this part once session is implemented
                var cookie = new CookieHeaderValue("session-id", HttpContext.Current.Session.SessionID);
                cookie.Expires = DateTimeOffset.Now.AddDays(1);
                cookie.Domain = Request.RequestUri.Host;
                cookie.Path = "/";

                //var cookie1 = new CookieHeaderValue("email", user.EmailAddress);
                //cookie.Expires = DateTimeOffset.Now.AddDays(1);
                //cookie.Domain = Request.RequestUri.Host;
                //cookie.Path = "/";

                var response = new HttpResponseMessage();

                response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                response.StatusCode = System.Net.HttpStatusCode.Accepted;
                response.Content = new StringContent(HttpContext.Current.Session.SessionID);

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