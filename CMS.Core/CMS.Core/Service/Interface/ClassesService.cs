using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
  public  interface ClassesService
    {
        void save(ClassesDto classesDto);
        void update(ClassesDto classesDto);
        void delete(long class_id);


    }
}
