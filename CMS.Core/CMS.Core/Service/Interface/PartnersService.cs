using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface PartnersService
    {
        void save(PartnersDto partnersDto);
        void update(PartnersDto partnersDto );
        void delete(long partners_id);
        void enable(long partners_id);
        void disable(long partners_id);

    }
}
