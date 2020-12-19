using CMS.User.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.User.Repository.Interface
{
    public interface RolePermissionMapRepository
    {
        void insert(RolePermissionMap role_permission_map);
        void update(RolePermissionMap role_permission_map);
        void delete(RolePermissionMap role_permission_map);
        List<RolePermissionMap> getAll();
        RolePermissionMap getById(long user_id);
        List<RolePermissionMap> getByRoleId(long role_id);
        List<RolePermissionMap> getByPermission(string permission_name);
        IQueryable<RolePermissionMap> getQueryable();
    }
}
