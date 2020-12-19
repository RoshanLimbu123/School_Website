using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface FiscalYearRepository
    {
        void insert(FiscalYear fiscalYear);
        void update(FiscalYear fiscalYear);
        void delete(FiscalYear fiscalYear);
        List<FiscalYear> getAll();
        FiscalYear getById(long fiscalYear_id);
        IQueryable<FiscalYear> getQueryable();
        FiscalYear getByCurrent();
    }
}
