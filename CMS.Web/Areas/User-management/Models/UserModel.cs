using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.User_management.Models
{
    public class UserModel
    {
        public long user_id { get; set; }
        public long created_by { get; set; }

        [Display(Name ="Full Name")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Full name  is required")]
        public string full_name { get; set; }

        [Display(Name = "Address Line 1")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "At least one address is required")]
        public string address_line_1 { get; set; }
        [Display(Name = "Address Line 2")]
        public string address_line_2 { get; set; }

        [Display(Name = "Primary Contact")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Primary contact number is required")]
        public string primary_contact { get; set; }
        [Display(Name = "Secondary Contact")]
        public string secondary_contact { get; set; }
        [Display(Name = "Status")]
        public bool is_active { get; set; } = true;
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Display(Name = "Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        public string username { get; set; }

        [Display(Name = "Password")]
        public string password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare(nameof(password),ErrorMessage ="Passwords didnot match.")]
        public string confirm_paswword { get; set; }
        [Display(Name = "Image")]
        public string image_path { get; set; }

        [Display(Name = "Roles")]
        public List<AssignedRole> roles { get; set; }
    }

    public class AssignedRole
    {
        public long role_id { get; set; }
        public string role_name { get; set; }
        public bool is_checked { get; set; }
    }
}
