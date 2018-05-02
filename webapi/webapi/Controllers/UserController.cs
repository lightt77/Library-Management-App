using System.Net.Http;
using System.Web.Http;
using webapi.Dao;
using webapi.Services;
using System.Net;

namespace webapi.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserService userService = new UserService();

        [Route("wishlist/user/{userName}/book/{bookName}")]
        [HttpPost]
        public void AddToWishList([FromUri]string userName,[FromUri]string bookName)
        {
            userService.AddToWishList(userName, bookName);
        }

        //FetchBooksInWishlist
    }
}
