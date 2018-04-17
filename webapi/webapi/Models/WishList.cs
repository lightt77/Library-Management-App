using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
    public class WishList
    {
        public int WishListId { get; set; }
        public int UserId { get; set; }
        public int TitleId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}