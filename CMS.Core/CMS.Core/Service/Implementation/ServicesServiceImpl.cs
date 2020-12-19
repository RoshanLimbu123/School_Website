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
    public class ServicesServiceImpl : ServicesService
    {
        private readonly ServicesRepository _servicesRepository;
        private readonly ServicesMaker _servicesMaker;
        public ServicesServiceImpl(ServicesRepository servicesRepository, ServicesMaker servicesMaker)
        {
            _servicesRepository = servicesRepository;
            _servicesMaker = servicesMaker;


        }

        public void delete(long service_id)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required))
                {
                    var service = _servicesRepository.getById(service_id);
                    if (service == null)
                    {
                        throw new ItemNotFoundException($" {service_id}  not found");
                    }
                    _servicesRepository.delete(service);
                    transaction.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void disable(long service_id)
        {
            try
            {

                var service = _servicesRepository.getById(service_id);
                if (service == null)
                {
                    throw new ItemNotFoundException("service not found");

                }
                service.is_active = false;
                _servicesRepository.update(service);
            }

            catch (Exception)
            {

                throw;
            }
        }

        public void enable(long service_id)
        {
            try
            {
                var service = _servicesRepository.getById(service_id);
                if (service == null)
                    throw new ItemNotFoundException($"Service with id {service_id} doesnot exist.");

                service.is_active = true;
                _servicesRepository.update(service);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void save(ServicesDto servicesDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Services services = new Services();
                    _servicesMaker.copy(services, servicesDto);
                    _servicesRepository.insert(services);

                    tx.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void update(ServicesDto servicesDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Services services = _servicesRepository.getById(servicesDto.service_id);
                    if (services == null)
                    {
                        throw new ItemNotFoundException($"Services with ID {servicesDto.service_id} doesnot Exit.");
                    }
                    _servicesMaker.copy(services, servicesDto);
                    _servicesRepository.update(services);

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
