using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.ViewModels
{
    public class ProductCategoryViewModel
    {
        public List<ProductDetail> products { get; set; }
        public ItemCategoryDto category_detail { get; set; }
    }
    public class ProductDetail
    {
        public long product_id { get; set; }
        public long item_category_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string specification { get; set; }
        public string features { get; set; }
        public bool is_enabled { get; set; } = true;
        public string file_name { get; set; }
        public string slug { get; set; }

        public string item_category_name { get; set; }
    }
}
