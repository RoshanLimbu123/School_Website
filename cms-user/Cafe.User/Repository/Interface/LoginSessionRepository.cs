using CMS.User.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.User.Repository.Interface
{
    public interface LoginSessionRepository
    {
        void insert(LoginSession login_session);
        List<LoginSession> getAll();
        IQueryable<LoginSession> getQueryable();
    }
}
