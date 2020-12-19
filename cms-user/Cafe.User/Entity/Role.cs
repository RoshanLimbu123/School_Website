using CMS.User.Enums;
using CMS.User.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.User.Entity
{
    public class Role
    {
        private string _name;

        [Key]
        public long role_id { get; set; }

        public string name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Role name must be specified.");
                }
                _name = value;
            }
        }

        public bool is_enabled { get; set; } = true;

        public virtual List<RolePermissionMap> permissions { get; set; }

        public bool isPermissionsAssigned() => permissions.Count > 0;

        public void enable()
        {
            is_enabled = true;
        }

        public void disable()
        {
            is_enabled = false;
        }
    }
}
