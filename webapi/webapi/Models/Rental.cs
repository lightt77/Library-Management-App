using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
    public class Rental
    {
        public int RentalId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RentalStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdated { get; set; } 
    }
}