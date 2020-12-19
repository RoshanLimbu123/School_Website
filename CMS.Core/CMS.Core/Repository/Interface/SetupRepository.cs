using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface SetupRepository
    {
        void insert(Setup setup);
        void update(Setup setup);
        void delete(Setup setup);
        List<Setup> getAll();
        Setup getById(long setup_id);
        Setup getByKey(string key);
        IQueryable<Setup> getQueryable();
    }
}
