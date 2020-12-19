using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
   public interface FacultyRepository
    {

        void insert(Faculty faculty);
        void update(Faculty faculty);
        void delete(Faculty faculty);
        Faculty getById(long faculty_id);

        List<Faculty> getAll();
        IQueryable<Faculty> getQueryable();
    }
}
