using CMS.Core.Dto;
using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Interface
{
    public interface PageCategoryMaker
    {
        void copy(ref PageCategory page_category, PageCategoryDto page_category_dto);
    }
}
