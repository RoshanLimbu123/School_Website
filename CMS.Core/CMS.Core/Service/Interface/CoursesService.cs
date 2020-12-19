using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface CoursesService
    {
        void save(CoursesDto product_dto);
        void update(CoursesDto product_dto);
        void delete(long product_id);
        void disable(long product_id);
        void enable(long product_id);
    }
}
