using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string BookName { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RentalStatus { get; set; } 
    }
}