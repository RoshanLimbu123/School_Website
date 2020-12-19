using CMS.User.Enums;
using CMS.User.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.User.Entity
{
    public class UserRole
    {
        private long _typeId, _roleId;

        [Key]
        public long user_role_id { get; set; }

        [Required]
        public UserType type { get; set; } = UserType.user;

        [Required]
        public long type_id { get; set; }

        [Required]
        public long role_id { get; set; }

        [ForeignKey("role_id")]
        public virtual Role role { get; set; }

    }
}
