using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface SpecialitiesCategoryRepository
    {
        void insert(SpecialitiesCategory specialitiesCategory);
        void update(SpecialitiesCategory specialitiesCategory);
        void delete(SpecialitiesCategory specialitiesCategory);
        List<SpecialitiesCategory> getAll();
        SpecialitiesCategory getById(long gallery_id);
        SpecialitiesCategory getByName(string name);
        IQueryable<SpecialitiesCategory> getQueryable();
    }
}
