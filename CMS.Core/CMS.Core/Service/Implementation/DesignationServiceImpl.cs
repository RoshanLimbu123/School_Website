using CMS.Core.Dto;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Core.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using CMS.Core.Makers.Interface;
using Microsoft.AspNetCore.Hosting;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
    public class DesignationServiceImpl : DesignationService
    {
        private readonly DesignationRepository _DesignationRepository;
        private readonly DesignationMaker _DesignationMaker;
        private readonly IHostingEnvironment _hostingEnvironment;

        public DesignationServiceImpl(DesignationRepository DesignationRepository, DesignationMaker DesignationMaker, IHostingEnvironment hostingEnvironment)
        {
            _DesignationMaker = DesignationMaker;
            _DesignationRepository = DesignationRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public void delete(long Designation_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var DesignationCategory = _DesignationRepository.getById(Designation_id);

                    if (DesignationCategory == null)
                    {
                        throw new ItemNotFoundException($"Designation category with id {Designation_id} doesnot exist.");
                    }

                    _DesignationRepository.delete(DesignationCategory);
                    tx.Complete();

                }
                

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void save(DesignationDto Designation_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var DesignationWithSamePosition = _DesignationRepository.getByPosition(Designation_dto.position);
                    if (DesignationWithSamePosition != null)
                    {
                        throw new ItemUsedException("This position is already used.");
                    }
                    Designation Designation = new Designation();
                    var designation_position = _DesignationRepository.getAll();
                    
                    _DesignationMaker.copy(ref Designation, Designation_dto);
                    _DesignationRepository.insert(Designation);
                    
                    tx.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void update(DesignationDto Designation_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                 
                    Designation Designation = _DesignationRepository.getById(Designation_dto.Designation_id);
                    
                    if (Designation == null)
                    {
                        throw new ItemNotFoundException($"Designation with ID {Designation_dto.Designation_id} doesnot Exit.");
                    }
                    
                    _DesignationMaker.copy(ref Designation, Designation_dto);
                    _DesignationRepository.update(Designation);
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
