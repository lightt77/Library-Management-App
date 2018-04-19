using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
    public class Book
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public List<string> Genre { get; set; }
        public string Author { get; set; }
        public int? Rating { get; set; }
        public int? Price { get; set; }
    }
}