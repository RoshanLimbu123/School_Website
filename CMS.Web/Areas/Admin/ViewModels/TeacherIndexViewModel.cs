using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class TeacherIndexViewModel
    {
       public  List<TeacherDetail> teachers { get; set; }
        public class TeacherDetail
        {
            public long teacher_id { get; set; }
            public long item_category_id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string slug { get; set; }
            public string file_name { get; set; }
            public bool is_enabled { get; set; } = true;
            public string item_category_name { get; set; }
        }


    }
}
