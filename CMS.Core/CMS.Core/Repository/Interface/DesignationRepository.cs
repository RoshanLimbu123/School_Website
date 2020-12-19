using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface DesignationRepository
    {
        void insert(Designation Designation);
        void update(Designation Designation);
        void delete(Designation Designation);
        List<Designation> getAll();
        Designation getById(long Designation_id);
        IQueryable<Designation> getQueryable();
        Designation getByPosition(string position);
    }
}
