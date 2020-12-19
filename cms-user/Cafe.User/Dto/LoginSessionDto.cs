using CMS.User.Enums;

namespace CMS.User.Dto
{
    public class LoginSessionDto
    {
        public long authentication_id { get; set; }
        public SessionType type { get; set; } = SessionType.login;
    }
}
