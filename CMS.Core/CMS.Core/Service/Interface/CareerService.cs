using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface CareerService
    {
        void save(CareerDto career_dto);
        void update(CareerDto career_dto);
        void delete(long career_id);
        void close(long career_id);
        void unclose(long career_id);
    }
}
