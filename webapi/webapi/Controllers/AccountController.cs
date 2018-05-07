using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    public class AccountController : ApiController
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
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.Accepted
                };
            }

            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized
            };
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