using System.Net.Http;
using System.Web.Http;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    public class AccountController : ApiController
    {
        private AccountService accountService = new AccountService();

        [Route("users/login")]
        [HttpPost]
        public HttpResponseMessage Login([FromBody]Users user)
        {
            return accountService.ValidateLogin(user);
        }

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
