using CMS.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.User.Service.Interface
{
    public interface UserRoleService
    {
        void save(UserRoleDto dto);
        void update(UserRoleDto dto);
    }
}
