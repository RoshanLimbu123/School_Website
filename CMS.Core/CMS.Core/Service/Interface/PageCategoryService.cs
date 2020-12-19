using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
     public interface PageCategoryService
    {
        void delete(long page_category_id);
        void save(PageCategoryDto pageCategoryDto);
        void update(PageCategoryDto pageCategory);
        void enable(long page_category_id);
        void disable(long page_category_id);
    }
}
