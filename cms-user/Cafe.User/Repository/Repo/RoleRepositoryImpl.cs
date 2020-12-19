using CMS.User.Data;
using CMS.User.Entity;
using CMS.User.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.User.Repository.Repo
{
    public class RoleRepositoryImpl : BaseRepositoryImpl<Role>, RoleRepository
    {
        private readonly UserDbContext _userDbContext;
        public RoleRepositoryImpl(UserDbContext userDbContext) : base(userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public Role getByName(string role_name)
        {
            return _userDbContext.roles.Where(a => a.name == role_name).SingleOrDefault();
        }
    }
}
