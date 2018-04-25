using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
    public class Users
    {
        public string Role { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string ResidentialAddress { get; set; }
    }
}