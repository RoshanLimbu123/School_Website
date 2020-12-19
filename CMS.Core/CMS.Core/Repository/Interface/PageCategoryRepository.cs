using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface PageCategoryRepository
    {
        void insert(PageCategory page_category);
        void update(PageCategory page_category);
        void delete(PageCategory page_category);
        List<PageCategory> getAll();
        PageCategory getById(long gallery_id);
        PageCategory getByName(string name);
        IQueryable<PageCategory> getQueryable();
    }
}
