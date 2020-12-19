using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface OutletRepository
    {
        void insert(Outlet outlet);
        void update(Outlet outlet);
        void delete(Outlet outlet);
        List<Outlet> getAll();
        Outlet getById(long outlet_id);
        Outlet getByName(string name);
        IQueryable<Outlet> getQueryable();
    }
}
