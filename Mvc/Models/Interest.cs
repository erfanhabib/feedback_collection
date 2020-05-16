using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class Interest
    {
        public int id { get; set; }
        public int post_id { get; set; }
        public int has_interest { get; set; }
        public int user_id { get; set; }
    }
}