using CMS.User.Data;
using CMS.User.Entity;
using CMS.User.Enums;
using CMS.User.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.User.Repository.Repo
{
    public class UserRoleRepositoryImpl : BaseRepositoryImpl<UserRole>, UserRoleRepository
    {
        private readonly UserDbContext _userDbContext;

        public UserRoleRepositoryImpl(UserDbContext userDbContext):base(userDbContext)
        {
            _userDbContext = userDbContext;
        }
        public List<UserRole> getByRoleId(long role_id)
        {
            return _userDbContext.user_roles.Where(a => a.role_id == role_id).ToList();
        }

        public List<UserRole> getByTypeId(UserType type, long type_id)
        {
            return _userDbContext.user_roles.Where(a => a.type == type && a.type_id==type_id).ToList();
        }
    }
}
