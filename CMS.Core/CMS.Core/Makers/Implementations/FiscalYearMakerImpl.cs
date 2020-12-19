using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class FiscalYearMakerImpl : FiscalYearMaker
    {
        public void copy(ref FiscalYear fiscalYear, FiscalYearDto fiscalYearDto)
        {
            fiscalYear.fiscal_year_id = fiscalYearDto.fiscal_year_id;
            fiscalYear.name = fiscalYearDto.name;
            fiscalYear.is_current = fiscalYearDto.is_current;
        }
    }
}
