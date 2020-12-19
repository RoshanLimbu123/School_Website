using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class TeacherMakerImpI : TeacherMaker
    {
        private readonly SlugGenerator _slugGenerator;

        public TeacherMakerImpI(SlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }
        public void copy(ref Teacher teacher, TeacherDto teacherDto)
        {
            teacher.teacher_id = teacherDto.teacher_id;
            teacher.item_category_id = teacherDto.item_category_id;
            teacher.name = teacherDto.name.Trim();
            teacher.description = teacherDto.description.Trim();
            teacher.is_enabled = teacherDto.is_enabled;
            if(!string.IsNullOrWhiteSpace(teacherDto.file_name))
            {
                teacher.file_name = teacherDto.file_name;
            }
            teacher.slug = _slugGenerator.generate(teacherDto.name);
        }
    }
}
