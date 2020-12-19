using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.User.Dto
{
    public class RolePermissionMapDto
    {
        private List<string> _permissions = new List<string>();
        
        public long role_id { get; set; }
        public List<string> permissions { get => _permissions; }

        public void addPermission(string permission)
        {
            if (!_permissions.Contains(permission.Trim()))
            {
                _permissions.Add(permission.Trim());
            }
        }

        public void removePermission(string permission)
        {
            if (_permissions.Contains(permission.Trim()))
            {
                _permissions.Remove(permission.Trim());
            }
        }

        public void removeAllPermissions()
        {
            _permissions = new List<string>();
        }
    }
}
