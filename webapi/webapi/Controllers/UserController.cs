using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace webapi.Controllers
{
    public class UserController : ApiController
    {
        [Route("wishlist/user/{userName}/book/{bookName}")]
        [HttpPost]
        public void AddToWishList([FromUri]string userName,[FromUri]string bookName)
        {

        }
    }
}
