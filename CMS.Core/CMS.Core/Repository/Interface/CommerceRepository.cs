using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
 public   interface CommerceRepository
    {
        void insert(Commerce commerce);
        void update(Commerce commerce);
        void delete(Commerce commerce);
        List<Commerce> getAll();
        List<Commerce> getByName(string title);
        Commerce getById(long commerce_id);
        Commerce getBySlug(string slug);
        IQueryable<Commerce> getQueryable();
    }
}
