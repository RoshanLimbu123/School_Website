using CMS.User.Entity;
using CMS.User.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.User.Repository.Interface
{
    public interface AuthenticationRepository
    {
        void insert(Authentication authentication);
        void update(Authentication authentication);
        List<Authentication> getAll();
        Authentication getById(long authentication_id);
        Authentication getByUsername(string username);
        Authentication getByType(long type_id,UserType type);
        IQueryable<Authentication> getQueryable();
    }
}
