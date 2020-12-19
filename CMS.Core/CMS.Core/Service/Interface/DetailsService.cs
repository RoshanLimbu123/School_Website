using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
   public interface DetailsService
    {
        void save(DetailsDto Details_dto);
        void update(DetailsDto Details_dto);
        void delete(long Designation_id);
    }
}
