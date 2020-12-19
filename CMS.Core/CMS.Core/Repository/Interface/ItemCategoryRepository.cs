using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface ItemCategoryRepository
    {
        void insert(ItemCategory item_category);
        void update(ItemCategory item_category);
        void delete(ItemCategory item_category);
        List<ItemCategory> getAll();
        ItemCategory getById(long gallery_id);
        ItemCategory getBySlug(string slug);
        ItemCategory getByName(string name);
        IQueryable<ItemCategory> getQueryable();
    }
}
