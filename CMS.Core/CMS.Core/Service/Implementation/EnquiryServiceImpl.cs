using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
    public class EnquiryServiceImpl : EnquiryService
    {
        private readonly EnquiryMaker _enquiryMaker;
        private readonly EnquiryRepository _enquiryRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        public EnquiryServiceImpl(EnquiryMaker enquiryMaker, EnquiryRepository enquiryRepository, IHostingEnvironment hostingEnvironment)
        {
            _enquiryMaker = enquiryMaker;
            _enquiryRepository = enquiryRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public void delete(long enquiry_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var enquiryCategory = _enquiryRepository.getById(enquiry_id);
                    if (enquiryCategory == null)
                    {
                        throw new ItemNotFoundException($"Enquiry Category With Id {enquiryCategory} doesnot Exist.");
                    }

                    _enquiryRepository.delete(enquiryCategory);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void save(EnquiryDto enquiryDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Enquiry enquiry = new Enquiry();
                    _enquiryMaker.copy(enquiry, enquiryDto);
                    _enquiryRepository.insert(enquiry);
                    tx.Complete();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void update(EnquiryDto enquiryDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {


                    Enquiry enquiry = _enquiryRepository.getById(enquiryDto.enquiry_id);

                    if (enquiry == null)
                    {
                        throw new ItemNotFoundException($"Enquiry with ID {enquiryDto.enquiry_id} doesnot Exit.");
                    }
                    _enquiryMaker.copy(enquiry, enquiryDto);
                    _enquiryRepository.update(enquiry);
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
