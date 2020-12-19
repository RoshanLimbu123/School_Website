using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.ViewModels
{
    public class BlogsIndexViewModel
    {
        public List<BlogDetailModel> blog_details { get; set; }
    }
    public class BlogDetailModel
    {
        public long blog_id { get; set; }

        public string title { get; set; }

        public DateTime posted_on { get; set; } = DateTime.Now;

        public string artical_by { get; set; }
        public string image_name { get; set; }
        public string description { get; set; }

        public string comment_by { get; set; }
        public DateTime comment_date { get; set; } = DateTime.Now;
        public string email { get; set; }
        public string comments { get; set; }
        public bool is_enabled { get; set; }
    }
    
}
