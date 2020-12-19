using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
   public interface FacultyService
    {
        void save(FacultyDto facultyDto);
        void update(FacultyDto facultyDto);
        void delete(long faculty_id);
    }
}
