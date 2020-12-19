using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class FacultyMakerImpl : FacultyMaker
    {
        public void copy(Faculty faculty, FacultyDto facultyDto)
        {
            faculty.faculty_id = facultyDto.faculty_id;
            faculty.is_active = facultyDto.is_active;
            faculty.name = facultyDto.name;

        }
    }
}
