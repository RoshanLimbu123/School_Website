using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
    public class FiscalYearServiceImpl : FiscalYearService
    {

        private readonly FiscalYearRepository _fiscalYearRepo;
        private readonly FiscalYearMaker _fiscalYearMaker;
        private readonly IHostingEnvironment _hostingEnvironment;

        public FiscalYearServiceImpl(FiscalYearRepository fiscalYearRepo, FiscalYearMaker fiscalYearMaker, IHostingEnvironment hostingEnvironment)
        {
            _fiscalYearRepo = fiscalYearRepo;
            _fiscalYearMaker = fiscalYearMaker;
            _hostingEnvironment = hostingEnvironment;
        }

        public void active(long fiscal_year_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    FiscalYear fiscalYear = _fiscalYearRepo.getById(fiscal_year_id);
                    if(fiscalYear == null)
                    {
                        throw new ItemNotFoundException($"Fiscal Year With Id {fiscal_year_id} is not Found.");
                    }
                    FiscalYear oldFiscalYear = _fiscalYearRepo.getByCurrent();
                    if (oldFiscalYear != null)
                    {
                        oldFiscalYear.is_current = false;
                        _fiscalYearRepo.update(fiscalYear);
                    }
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }

       
        public void delete(long fiscal_year_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    FiscalYear fiscalYear = _fiscalYearRepo.getById(fiscal_year_id);
                    if (fiscalYear == null)
                    {
                        throw new ItemNotFoundException($"Fiscal Year with ID {fiscal_year_id} doesnot exist.");
                    }
                    _fiscalYearRepo.delete(fiscalYear);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

      

        public void save(FiscalYearDto fiscal_year_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    FiscalYear fiscalYear = new FiscalYear();
                    _fiscalYearMaker.copy(ref fiscalYear, fiscal_year_dto);
                    _fiscalYearRepo.insert(fiscalYear);
                    
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void update(FiscalYearDto fiscal_year_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    FiscalYear fiscalYear = _fiscalYearRepo.getById(fiscal_year_dto.fiscal_year_id);
                    if (fiscalYear == null)
                    {
                        throw new ItemNotFoundException($"Fiscal Year with ID {fiscal_year_dto.fiscal_year_id} doesnot Exit.");
                    }

                    _fiscalYearMaker.copy(ref fiscalYear, fiscal_year_dto);
                    _fiscalYearRepo.update(fiscalYear);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

