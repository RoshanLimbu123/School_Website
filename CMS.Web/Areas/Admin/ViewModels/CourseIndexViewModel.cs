using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class CourseIndexViewModel
    {
        public List<CourseDetail> courses { get; set; }
    }

    public class CourseDetail
    {
        public long course_id { get; set; }
        public long faculty_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string specification { get; set; }
        public string features { get; set; }
        public bool is_enabled { get; set; } = true;
        public string file_name { get; set; }

        public virtual Faculty faculty { get; set; }
    }
}
