using CMS.User.Data;
using CMS.User.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.User.Repository.Repo
{
    using CMS.User.Entity;
    using System.Linq;
    using userEntity = User.Entity.User;

    public class UserRepositoryImpl : BaseRepositoryImpl<userEntity>, UserRepository
    {
        private readonly UserDbContext _userDbContext;
        public UserRepositoryImpl(UserDbContext userDbContext) : base(userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public List<userEntity> getByName(string user_name)
        {
            return _userDbContext.users.Where(a => a.full_name == user_name).ToList();
        }
    }
}
