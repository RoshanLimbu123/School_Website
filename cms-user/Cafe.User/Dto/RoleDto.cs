using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.User.Dto
{
    public class RoleDto
    {
        public long role_id { get; set; }
        public string name { get; set; }
        public bool is_active { get; set; }

        public List<string> permissions { get; set; }
    }
}
