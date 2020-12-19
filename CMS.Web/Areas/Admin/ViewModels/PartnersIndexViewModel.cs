using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Admin.ViewModels
{
    public class PartnersIndexViewModel
    {
        public List<PartnersDetail> partnersDetails { get; set; }
    }
    public class PartnersDetail
    {
        public long partners_id { get; set; }
        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Image")]
        public string logo_name { get; set; }
        public bool is_active { get; set; } = true;


    }
}
