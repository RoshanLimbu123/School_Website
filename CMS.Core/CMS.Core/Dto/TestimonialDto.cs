using System.ComponentModel.DataAnnotations;

namespace CMS.Core.Dto
{
    public class TestimonialDto
    {
        public long testimonial_id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage ="Please enter a person name")]
        [Display(Name ="Person Name")]
        public string person_name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a statement")]
        [Display(Name = "Statement")]
        public string statement { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a designation")]
        [Display(Name = "Designation")]
        public string designation { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a company name")]
        [Display(Name = "Company Name")]
        public string associated_company_name { get; set; }
        [Display(Name ="Image")]
        public string image_name { get; set; }

        public bool is_visible { get; set; } = true;
    }
}
