using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class Post
    {
        public int id { get; set; }
        public string post_desc { get; set; }
        public System.DateTime entered_on { get; set; }
        public int user_id { get; set; }
        public int parent_id { get; set; }

        public string user_name { get; set; }
        public string comment_count { get; set; }
        public string comment_count_like { get; set; }
        public string comment_count_dis_like { get; set; }

        public string  entered_date { get; set; }
    }
}