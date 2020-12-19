using CMS.User.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.User.Entity
{
    public class RolePermissionMap
    {
        private long _roleId;
        private string _permissionName;

        [Key]
        public long role_permission_map_id { get; set; }

        [Required]
        public long role_id { get; set; }

        public string permission_name
        {
            get => _permissionName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Permission name must be specified.");
                }
                _permissionName = value;
            }
        }

        [ForeignKey("role_id")]
        public virtual Role role { get; set; }
    }
}
