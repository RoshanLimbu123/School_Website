using CMS.Core.Dto;
using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface ItemCategoryService
    {
        void save(ItemCategoryDto item_category_dto);
        void update(ItemCategoryDto item_category_dto);
        void delete(long item_category_id);
        void disable(long item_category_id);
        void enable(long item_category_id);
    }
}
