using System.ComponentModel.DataAnnotations;

namespace CMS.User.Dto
{
    public class UpdatePasswordDto
    {
        [Required]
        [Range(1,long.MaxValue)]
        public long type_id { get; set; }

        public Enums.UserType type { get; set; } = Enums.UserType.user;

        [Required(AllowEmptyStrings =false,ErrorMessage ="Old password is required.")]
        [Display(Name ="Old Password")]
        public string old_password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "New password is required.")]
        [Display(Name = "New Password")]
        public string new_password { get; set; }

        [Compare(nameof(new_password),ErrorMessage ="Passwords didnot match.")]
        [Display(Name = "Confirm Password")]
        public string confirm_password { get; set; }
    }
}
