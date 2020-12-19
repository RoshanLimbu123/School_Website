using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
     public interface SpecialitiesCategoryService
    {
        void delete(long specialities_category_id);
        void save(SpecialitiesCategoryDto specialitiesCategoryDto);
        void update(SpecialitiesCategoryDto specialitiesCategoryDto);
        void enable(long specialities_category_id);
        void disable(long specialities_category_id);
    }
}
