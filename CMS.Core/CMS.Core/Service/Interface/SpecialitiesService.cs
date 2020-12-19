using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface SpecialitiesService
    {
        void delete(long specialities_id);
        void save(SpecialitiesDto specialitiesDto);
        void update(SpecialitiesDto specialitiesDto);
        void enable(long specialities_id);
        void disable(long specialities_id);
    }
}
