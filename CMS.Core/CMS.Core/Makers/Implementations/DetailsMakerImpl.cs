using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
   public class DetailsMakerImpl : DetailsMaker
    {

       public void copy(ref Details details, DetailsDto details_dto)
        {
            details.details_id = details_dto.details_id;
            details.value0 = details_dto.value0;

            details.value1 = details_dto.value1;

            details.value4 = details_dto.value4;

            details.value2 = details_dto.value2;

            details.value3 = details_dto.value3;

        }
    }
}
