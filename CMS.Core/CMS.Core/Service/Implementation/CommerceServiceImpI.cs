using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
   public class CommerceServiceImpI : CommerceService
    {
        private readonly CommerceMaker _commerceMaker;
        private readonly CommerceRepository _commerceRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CommerceServiceImpI(CommerceMaker commerceMaker,CommerceRepository commerceRepository,IHostingEnvironment hostingEnvironment)
        {
            _commerceMaker = commerceMaker;
            _commerceRepository = commerceRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public void delete(long commerce_id)
        {
            try
            {
                  using(TransactionScope tx=new TransactionScope(TransactionScopeOption.Required))
                {
                    var commerce = _commerceRepository.getById(commerce_id);
                    if(commerce==null)
                    {
                        throw new ItemNotFoundException($"{commerce_id} Not Found");
                    }
                    _commerceRepository.delete(commerce);
                    deleteImage(commerce.image_name);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void disable(long commerce_id)
        {
            try
            {
                var commerce = _commerceRepository.getById(commerce_id);
                if(commerce==null)
                {
                    throw new ItemNotFoundException($"{commerce_id} Not found");
                }
                commerce.disable();
                _commerceRepository.update(commerce);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void enable(long commerce_id)
        {
            try
            {
                var commerce = _commerceRepository.getById(commerce_id);
                if(commerce==null)
                {
                    throw new ItemNotFoundException($"{commerce_id} not found");
                }
                commerce.enable();
                _commerceRepository.update(commerce);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void save(CommerceDto commerceDto)
        {
            try
            {
               using(TransactionScope tx=new TransactionScope(TransactionScopeOption.Required))
                {
                    Commerce commerce = new Commerce();
                    _commerceMaker.copy(ref commerce, commerceDto);
                    _commerceRepository.insert(commerce);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void update(CommerceDto commerceDto)
        {
            try
            {
                using(TransactionScope tx=new TransactionScope(TransactionScopeOption.Required))
                {
                    var commerce = _commerceRepository.getById(commerceDto.commerce_id);
                    string oldImage = commerce.image_name;
                    _commerceMaker.copy(ref commerce, commerceDto);
                    _commerceRepository.update(commerce);
                    if(!string.IsNullOrWhiteSpace(commerceDto.image_name))
                    {
                        if(!string.IsNullOrWhiteSpace(oldImage))
                        {
                            deleteImage(oldImage);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void deleteImage(string image_name)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/custom");
            if(File.Exists(Path.Combine(filePath,image_name)))
            {
                File.Delete(Path.Combine(filePath, image_name));
            }
        }
    }
}
