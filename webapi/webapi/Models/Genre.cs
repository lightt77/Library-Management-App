using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}