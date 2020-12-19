using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
 public   interface RoutineRepository
    {
        void insert(Routine routine_id);
        void update(Routine routine_id);
        void delete(Routine routine_id);
        Routine getById(long routine_id);

        List<Routine> getAll();
        IQueryable<Routine> getQueryable();
    }
}
