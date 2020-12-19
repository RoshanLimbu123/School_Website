using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
    public class EmailSetupServiceImpl : EmailSetupService
    {
        private readonly EmailsetupRepository _emailSetupRepository;
        private readonly EmailSetupMaker _emailSetupMaker;
        public EmailSetupServiceImpl(EmailsetupRepository emailsetupRepository, EmailSetupMaker emailSetupMaker)
        {
            _emailSetupRepository = emailsetupRepository;
            _emailSetupMaker = emailSetupMaker;
        }
        public void delete(long email_setup_id)
        {
            try
            {
                using(TransactionScope txe =new TransactionScope(TransactionScopeOption.Required))
                {
                    var emailSetupCategory = _emailSetupRepository.getById(email_setup_id);
                    if(emailSetupCategory==null)
                    {
                        throw new ItemNotFoundException($" emailsetup id {email_setup_id} doesnot exist");
                    }
                    _emailSetupRepository.delete(emailSetupCategory);
                    txe.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void save(EmailSetupDto emailSetupDto)
        {
            try
            {
                using (TransactionScope txe = new TransactionScope(TransactionScopeOption.Required))
                {
                    var emailSetupCategory = _emailSetupRepository.getById(emailSetupDto.email_setup_id);
                    if (emailSetupCategory == null)
                    {
                        throw new ItemNotFoundException($"email setup Category With Id {emailSetupCategory.email_setup_id} doesnot Exist.");
                    }
                    _emailSetupMaker.copy(emailSetupCategory, emailSetupDto);
                    _emailSetupRepository.insert(emailSetupCategory);
                    txe.Complete();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void update(EmailSetupDto emailSetupDto)
        {
            try
            {
             using (TransactionScope txe =new TransactionScope(TransactionScopeOption.Required))
                {
                    EmailSetup emailTerm = _emailSetupRepository.getById(emailSetupDto.email_setup_id);
                   if(emailTerm==null)
                    {
                        throw new ItemNotFoundException($"emailterm id {emailSetupDto.email_setup_id} doesnot exist");
                    }
                    _emailSetupMaker.copy(emailTerm, emailSetupDto);
                    _emailSetupRepository.update(emailTerm);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
