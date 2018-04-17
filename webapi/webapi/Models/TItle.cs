using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
    public class Title
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }
        public string Author { get; set; }
        public int Rating { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}