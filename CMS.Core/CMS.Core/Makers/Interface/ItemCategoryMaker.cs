using CMS.Core.Dto;
using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Interface
{
    public interface ItemCategoryMaker
    {
        void copy(ref ItemCategory item_category, ItemCategoryDto item_category_dto);
    }
}
