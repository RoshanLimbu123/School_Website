using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class ServicesIndexViewModel
    {
        public List<ServicesDetails> servicesDetails { get; set; }

    }
    public class ServicesDetails
    {
        public long service_id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string image { get; set; }
        public bool is_active { get; set; }
    }
}
