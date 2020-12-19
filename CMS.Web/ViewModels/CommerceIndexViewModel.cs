using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.ViewModels
{
    public class CommerceIndexViewModel
    {
        public List<CommerceDetail> commerces { get; set; }
    }
    public class CommerceDetail
    {
        public long commerce_id { get; set; }
        public string title { get; set; }
        public string slug { get; set; }
        public string image_name { get; set; }
        public bool is_enabled { get; set; }
    }
}
