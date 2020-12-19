using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class BlogCommentIndexViewModel
    {
        public List<BlogCommentDetailModel> blog_details { get; set; }
    }
    public class BlogCommentDetailModel
    {
        public long blog_comment_id { get; set; }

        public virtual Blog blog { get; set; }

        public long blog_id { get; set; }
        public string comment_by { get; set; }

        public DateTime comment_date { get; set; } = DateTime.Now;
        public string email { get; set; }
        public string comments { get; set; }
    }
}
