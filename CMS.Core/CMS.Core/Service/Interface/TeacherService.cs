using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
  public interface TeacherService
    {
        void save(TeacherDto teacherDto);
        void update(TeacherDto teacherDto);
        void delete(long teacher_id);
        void disable(long teacher_id);
        void enable(long teacher_id);
    }
}
