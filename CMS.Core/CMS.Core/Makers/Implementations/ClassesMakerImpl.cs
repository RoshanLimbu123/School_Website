using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class ClassesMakerImpl : ClassesMaker
    {
        public void copy(Classes classes, ClassesDto classesDto)
        {
            classes.class_id = classesDto.class_id;
            classes.is_active = classesDto.is_active;
            classes.name = classesDto.name;
        }
    }
}
