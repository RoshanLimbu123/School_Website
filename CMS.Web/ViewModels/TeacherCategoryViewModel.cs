using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CMS.Web.Areas.Core.ViewModels.TeacherIndexViewModel;

namespace CMS.Web.ViewModels
{
    public class TeacherCategoryViewModel
    {
        public List<TeacherDetail> teachers { get; set; }
        public ItemCategoryDto category_details { get; set; }
    }
    public class TeacherDetail
    {
        public long teacher_id { get; set; }
        public long item_category_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool is_enabled { get; set; } = true;
        public string file_name { get; set; }
        public string slug { get; set; }
        public string item_category_name { get; set; }

    }
}
