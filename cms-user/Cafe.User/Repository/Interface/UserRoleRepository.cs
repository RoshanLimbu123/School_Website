using CMS.User.Entity;
using CMS.User.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.User.Repository.Interface
{
    public interface UserRoleRepository
    {
        void insert(UserRole user_role);
        void update(UserRole user_role);
        void delete(UserRole user_role);
        List<UserRole> getAll();
        List<UserRole> getByRoleId(long role_id);
        List<UserRole> getByTypeId(UserType type,long type_id);

        UserRole getById(long user_id);
        IQueryable<UserRole> getQueryable();
    }
}
