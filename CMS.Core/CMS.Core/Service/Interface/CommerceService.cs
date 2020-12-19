using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
  public  interface CommerceService
    {
        void save(CommerceDto commerceDto);
        void update(CommerceDto commerceDto);
        void delete(long commerce_id);
        void disable(long commerce_id);
        void enable(long commerce_id);
    }
}
