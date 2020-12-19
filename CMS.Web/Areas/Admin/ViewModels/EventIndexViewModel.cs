using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class EventIndexViewModel
    {
        public List<EventDetailModel> event_details { get; set; }
    }

    public class EventDetailModel
    {
        public long event_id { get; set; }

        public string title { get; set; }

        public DateTime event_from_date { get; set; } = DateTime.Now;

        public DateTime event_to_date { get; set; } = DateTime.Now.AddDays(7);
        public string description { get; set; }
        public string image_name { get; set; }
        public string venue { get; set; }

        public string file_name { get; set; }
        public string time { get; set; }
        public bool is_closed { get; set; }
    }
}

