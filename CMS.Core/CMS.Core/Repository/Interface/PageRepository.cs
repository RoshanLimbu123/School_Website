using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface PageRepository
    {
        void insert(Page page);
        void update(Page page);
        void delete(Page page);
        List<Page> getAll();
        Page getById(long page_id);
        Page getBySlug(string slug);
        List<Page> getByName(string title);
        IQueryable<Page> getQueryable();
        Page getHomePage();
    }
}
