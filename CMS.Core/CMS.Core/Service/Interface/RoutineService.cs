using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
   public interface RoutineService
    {
        void delete(long routine_id);
        void update(RoutineDto routineDto);
        void save(RoutineDto routineDto);
    }
}
