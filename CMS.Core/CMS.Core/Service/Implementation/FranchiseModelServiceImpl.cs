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
    public class FranchiseModelServiceImpl : FranchiseModelService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly FranchiseModelRepository _franchiseModelRepository;
        private readonly FranchiseModelMaker _franchiseModelMaker;


        public FranchiseModelServiceImpl(FranchiseModelRepository franchiseModelRepository, FranchiseModelMaker franchiseModelMaker, IHostingEnvironment hostingEnvironment)
        {
            _franchiseModelRepository = franchiseModelRepository;
            _franchiseModelMaker = franchiseModelMaker;
            _hostingEnvironment = hostingEnvironment;
        }
        public void delete(long franchise_model_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var franchise = _franchiseModelRepository.getById(franchise_model_id);
                    if (franchise == null)
                    {
                        throw new ItemNotFoundException($"Franchise with Id {franchise_model_id} doesnot exist.");
                    }

                    _franchiseModelRepository.delete(franchise);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void disable(long franchise_model_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var franchise = _franchiseModelRepository.getById(franchise_model_id);
                    if (franchise == null)
                    {
                        throw new ItemNotFoundException($"Franchise with Id {franchise_model_id} doesnot exist.");
                    }
                    franchise.disable();
                    _franchiseModelRepository.update(franchise);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void enable(long franchise_model_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var franchise = _franchiseModelRepository.getById(franchise_model_id);
                    if (franchise == null)
                    {
                        throw new ItemNotFoundException($"Franchise with Id {franchise_model_id} doesnot exist.");
                    }
                    franchise.enable();
                    _franchiseModelRepository.update(franchise);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void save(FranchiseModelDto franchiseModelDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    FranchiseModel franchiseModel = new FranchiseModel();
                    _franchiseModelMaker.copy(franchiseModel, franchiseModelDto);
                    _franchiseModelRepository.insert(franchiseModel);
                    tx.Complete();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void update(FranchiseModelDto franchiseModelDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {


                    FranchiseModel franchiseModel = _franchiseModelRepository.getById(franchiseModelDto.franchise_model_id);

                    if (franchiseModel == null)
                    {
                        throw new ItemNotFoundException($"Event with ID {franchiseModelDto.franchise_model_id} doesnot Exit.");
                    }
                    if (franchiseModelDto.file_name == null)
                    {
                        franchiseModelDto.file_name = franchiseModel.file_name;
                    }

                    if (franchiseModelDto.file_name == null)
                    {
                        franchiseModelDto.file_name = franchiseModel.file_name;
                    }
                    _franchiseModelMaker.copy(franchiseModel, franchiseModelDto);

                    _franchiseModelRepository.update(franchiseModel);
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
