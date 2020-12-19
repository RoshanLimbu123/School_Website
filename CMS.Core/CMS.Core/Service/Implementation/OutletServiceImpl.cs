using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Helper;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace CMS.Core.Service.Implementation
{
    public class OutletServiceImpl : OutletService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private OutletRepository _outletRepository;
        private TransactionManager _transactionManager;
        private readonly OutletMaker _outletMaker;


        public OutletServiceImpl(OutletRepository outletRepository, TransactionManager transactionManager, OutletMaker outletMaker, IHostingEnvironment hostingEnvironment)
        {
            _outletRepository = outletRepository;
            _transactionManager = transactionManager;
            _outletMaker = outletMaker;
            _hostingEnvironment = hostingEnvironment;
        }

        public void delete(long outlet_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var outlet = _outletRepository.getById(outlet_id);

                if (outlet == null)
                {
                    throw new ItemNotFoundException($"Notice with id {outlet_id} doesnot exist.");
                }

                string oldImage = outlet.file_name;

                _outletRepository.delete(outlet);

                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    deleteImage(oldImage);
                }

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void save(OutletDto outletDto)
        {
            try
            {
                _transactionManager.beginTransaction();
            
                Outlet fileUpload = new Outlet();
                _outletMaker.copy( fileUpload, outletDto);
                _outletRepository.insert(fileUpload);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }


        public void update(OutletDto outletDto)
        {
            try
            {
                _transactionManager.beginTransaction();

                Outlet outlet = _outletRepository.getById(outletDto.outlet_id);

                string oldImage = outlet.file_name;

                _outletMaker.copy( outlet, outletDto);
                _outletRepository.update(outlet);

                if (!string.IsNullOrWhiteSpace(outletDto.file_name))
                {
                    deleteImage(oldImage);
                }

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void enable(long outlet_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var files = _outletRepository.getById(outlet_id);
                if (files == null)
                    throw new ItemNotFoundException($"Outlet with id {outlet_id} doesnot exist.");

                files.enable();
                _outletRepository.update(files);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void disable(long outlet_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var files = _outletRepository.getById(outlet_id);
                if (files == null)
                    throw new ItemNotFoundException($"File with id {outlet_id} doesnot exist.");

                files.disable();
                _outletRepository.update(files);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        private void deleteImage(string image_name)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/custom");
            if (File.Exists(Path.Combine(filePath, image_name)))
            {
                File.Delete(Path.Combine(filePath, image_name));
            }
        }
    }
}
