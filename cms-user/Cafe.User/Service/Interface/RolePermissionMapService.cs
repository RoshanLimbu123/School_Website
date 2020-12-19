using CMS.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.User.Service.Interface
{
    public interface RolePermissionMapService
    {
        void saveOrUpdate(RolePermissionMapDto dto);
        void deletePermissionsByRoleId(long role_id);
    }
}
