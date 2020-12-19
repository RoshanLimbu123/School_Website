using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class EnquiryIndexViewModel
    {
        public List<EnquiryDetailModel> enquiry_details { get; set; }
    }
    public class EnquiryDetailModel
    {
        [Key]
        public long enquiry_id { get; set; }
        [MaxLength(100)]
        public string address { get; set; }

        [MaxLength(50)]
        public string contact_number { get; set; }

        [Required]
        [MaxLength(200)]
        public string name { get; set; }

        [Required]
        [MaxLength(300)]
        public string email { get; set; }


        [MaxLength(5000)]
        public string description { get; set; }
        public DateTime enquiry_date { get; set; } = DateTime.Now;

    }
}
