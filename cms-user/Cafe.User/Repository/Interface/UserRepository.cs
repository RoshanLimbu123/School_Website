using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.User.Repository.Interface
{
    using userEntity = User.Entity.User;

    public interface UserRepository
    {
        void insert(userEntity user);
        void update(userEntity user);
        List<userEntity> getAll();
        userEntity getById(long user_id);
        List<userEntity> getByName(string user_name);
        IQueryable<userEntity> getQueryable();
    }
}
