using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface DesignationService
    {
        void save(DesignationDto Designation_dto);
        void update(DesignationDto Designation_dto);
        void delete(long Designation_id);
    }
}
