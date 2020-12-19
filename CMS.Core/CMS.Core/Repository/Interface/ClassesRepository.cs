using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
   public interface ClassesRepository
    {
        void insert(Classes class_id);
        void update(Classes class_id);
        void delete(Classes class_id);
        Classes getById(long class_id);

        List<Classes> getAll();
        IQueryable<Classes> getQueryable();
    }
}
