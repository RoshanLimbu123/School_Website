using CMS.User.Enums;

namespace CMS.User.Dto
{
    public class UserRoleDto
    {

        public long[] role_ids { get; set; }
        public UserType type { get; set; }
        public long type_id { get; set; }
    }
}
