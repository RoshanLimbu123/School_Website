using CMS.User.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.User.Repository.Interface
{
    public interface RoleRepository
    {
        void insert(Role role);
        void update(Role role);
        void delete(Role role);
        List<Role> getAll();
        Role getById(long role_id);
        Role getByName(string role_name);
        IQueryable<Role> getQueryable();
    }
}
