using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface ServicesService
    {
        void save(ServicesDto servicesDto);
        void delete(long service_id);
        void update(ServicesDto servicesDto);
        void enable(long service_id);
        void disable(long service_id);

    }
}
