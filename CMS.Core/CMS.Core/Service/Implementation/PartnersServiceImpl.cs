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
    public class PartnersServiceImpl : PartnersService
    {
        private readonly PartnersRepository _partnersRepository;
        private readonly PartnersMaker _partnersMaker;
        public PartnersServiceImpl(PartnersRepository partnersRepository, PartnersMaker partnersMaker)
        {
            _partnersRepository = partnersRepository;
            _partnersMaker = partnersMaker;
        }

        public void delete(long partners_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var partners = _partnersRepository.getById(partners_id);
                    if (partners == null)
                    {
                        throw new ItemNotFoundException($" {partners_id}  not found");
                    }
                    _partnersRepository.delete(partners);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void disable(long partners_id)
        {
            try
            {

                var partners = _partnersRepository.getById(partners_id);
                if (partners == null)
                {
                    throw new ItemNotFoundException($" {partners_id } not found");
                }
                partners.is_active = false;
                _partnersRepository.update(partners);


            }
            catch (Exception)
            {

                throw;
            }
        }

        public void enable(long partners_id)
        {
            try
            {

                var partners = _partnersRepository.getById(partners_id);
                if (partners == null)
                {
                    throw new ItemNotFoundException($" {partners_id } not found");
                }
                partners.is_active = true;
                _partnersRepository.update(partners);


            }
            catch (Exception)
            {

                throw;
            }
        }

        public void save(PartnersDto partnersDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Partners partners = new Partners();
                    _partnersMaker.copy(partners, partnersDto);
                    _partnersRepository.insert(partners);
                    tx.Complete();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void update(PartnersDto partnersDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {


                    Partners partners  = _partnersRepository.getById(partnersDto.partners_id);

                    if (partners == null)
                    {
                        throw new ItemNotFoundException($"Partners with ID {partners.partners_id} doesnot Exit.");
                    }
                    if (partnersDto.logo_name == null)
                    {
                        partnersDto.logo_name = partners.logo_name;
                    }

                    _partnersMaker.copy(partners, partnersDto);

                    _partnersRepository.update(partners);
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
