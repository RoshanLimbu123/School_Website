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
    public class DetailsServiceImpl : DetailsService
    {

        private readonly DetailsRepository _detailsRepository;
        private readonly DetailsMaker _detailsMaker;
        private readonly IHostingEnvironment _hostingEnvironment;

        public DetailsServiceImpl(DetailsRepository DetailsRepository, DetailsMaker DetailsMaker, IHostingEnvironment hostingEnvironment)
        {

            _detailsMaker = DetailsMaker;
            _detailsRepository = DetailsRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public void delete(long Details_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var DetailCategory = _detailsRepository.getById(Details_id);

                    if (DetailCategory == null)
                    {
                        throw new ItemNotFoundException($"Details category with id {Details_id} doesnot exist.");
                    }

                    _detailsRepository.delete(DetailCategory);
                    tx.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void save(DetailsDto Details_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                   
                    Details Detail = new Details();
                    var designation_position = _detailsRepository.getAll();

                    _detailsMaker.copy(ref Detail, Details_dto);
                    _detailsRepository.insert(Detail);

                    tx.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void update(DetailsDto Details_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                   
                    Details Detail = _detailsRepository.getById(Details_dto.details_id);

                    if (Detail == null)
                    {
                        throw new ItemNotFoundException($"Details with ID {Details_dto.details_id} doesnot Exit.");
                    }

                    _detailsMaker.copy(ref Detail, Details_dto);
                    _detailsRepository.update(Detail);
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

       

