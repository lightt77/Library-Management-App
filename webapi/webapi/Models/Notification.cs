using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
    public class Notification
    {
        public int Type { get; set; }
        public Users User { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public string RelatedData { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}