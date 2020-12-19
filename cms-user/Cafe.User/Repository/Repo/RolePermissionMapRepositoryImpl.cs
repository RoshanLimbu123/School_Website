using CMS.User.Data;
using CMS.User.Entity;
using CMS.User.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.User.Repository.Repo
{
    public class RolePermissionMapRepositoryImpl : BaseRepositoryImpl<RolePermissionMap>, RolePermissionMapRepository
    {
        private readonly UserDbContext _userDbContext;
        public RolePermissionMapRepositoryImpl(UserDbContext userDbContext) : base(userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public List<RolePermissionMap> getByPermission(string permission_name)
        {
            return _userDbContext.role_permission_maps.Where(a => a.permission_name == permission_name).ToList();
        }

        public List<RolePermissionMap> getByRoleId(long role_id)
        {
            return _userDbContext.role_permission_maps.Where(a => a.role_id == role_id).ToList();
        }
    }
}
