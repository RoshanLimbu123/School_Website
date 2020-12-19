using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Admin.ViewModels
{
    public class DetailsIndexViewModel
    {
        public List<DetailsDetailModel> details { get; set; }
    }

    public class DetailsDetailModel
    {
        public long details_id { get; set; }
        public long value1 { get; set; }
        public long value2 { get; set; }

        public long value3 { get; set; }

        public long value4 { get; set; }
        public long value0 { get; set; }

    }
}
