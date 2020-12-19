using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface FiscalYearService
    {
        void save(FiscalYearDto fiscal_year_dto);
        void update(FiscalYearDto fiscal_year_dto);
        void delete(long fiscal_year_id);
        void active(long fiscal_year_id);
        
    }
}
