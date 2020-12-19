using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class FranchiseModelIndexViewModel
    {
        public List<franchiseDetailModel> franchiseDetailModels { get; set; }
    }

    public class franchiseDetailModel
    {
        public long franchise_model_id { get; set; }

        public string title { get; set; }
        public string description { get; set; }
        public string file_name { get; set; }
        public bool is_enabled { get; set; }
    }
}
