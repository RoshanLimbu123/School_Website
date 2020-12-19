using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class RoutineMakerImpl : RoutineMaker
    {
        public void copy(Routine routine, RoutineDto routineDto)
        {
            routine.routine_id = routineDto.routine_id;
            routine.title = routineDto.title;
            routine.class_id = routineDto.class_id;
            routine.start_date = routineDto.start_date;
            routine.end_date = routineDto.end_date;
            routine.image = routineDto.image;
            routine.is_active = routineDto.is_active;
        }
    }
}
