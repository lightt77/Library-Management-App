using System.Net.Http;
using System.Web.Http;
using webapi.Services;
using System.Net;
using System.Web.Http.Cors;
using webapi.Models;
using System.Linq;

namespace webapi.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserService userService = new UserService();
        private readonly AccountService accountService = new AccountService();
        //[Route("wishlist/user/{userName}/book/{bookName}")]
        //[HttpPost]
        //public void AddToWishList([FromUri]string userName,[FromUri]string bookName)
        //{
        //    userService.AddToWishList(userName, bookName);
        //}

        [EnableCors(origins: "http://127.0.0.1:5500", headers: "*", methods: "*")]
        [Route("user/wishlist")]
        [HttpPost]
        public HttpResponseMessage AddToWishList([FromBody]Book book)
        {
            string userEmailAddress;

            if (Request.Headers.GetValues("EmailId").Count() == 0)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            userEmailAddress = Request.Headers.GetValues("EmailId").First();

            string userName = accountService.GetNameOfCurrentlyLoggedInUser(userEmailAddress);

            userService.AddToWishList(userName, book.Title);

            return new HttpResponseMessage()
            {
                StatusCode=HttpStatusCode.Accepted
            };
        }

        //FetchBooksInWishlist
    }
}
