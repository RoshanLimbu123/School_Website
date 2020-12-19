using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface OutletService
    {
        void save(OutletDto outletDto);
        void update(OutletDto outletDto);
        void delete(long outlet_id);
        void enable(long outlet_id);
        void disable(long outlet_id);
    }
}
