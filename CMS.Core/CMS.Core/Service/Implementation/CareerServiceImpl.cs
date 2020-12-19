using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Helper;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CMS.Core.Service.Implementation
{
    public class CareerServiceImpl : CareerService
    {
        private readonly TransactionManager _transactionManager;
        private readonly CareerRepository _careerRepo;
        private readonly CareerMaker _careerMaker;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CareerServiceImpl(TransactionManager transactionManager, CareerRepository careerRepo, CareerMaker careerMaker, IHostingEnvironment hostingEnvironment)
        {
            _transactionManager = transactionManager;
            _careerRepo = careerRepo;
            _careerMaker = careerMaker;
            _hostingEnvironment = hostingEnvironment;
        }

        public void close(long career_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var career = _careerRepo.getById(career_id);

                if (career == null)
                {
                    throw new ItemNotFoundException($"Career with id {career_id} doesnot exist.");
                }

                career.markClosed();
                _careerRepo.update(career);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void unclose(long career_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var career = _careerRepo.getById(career_id);

                if (career == null)
                {
                    throw new ItemNotFoundException($"Career with id {career_id} doesnot exist.");
                }

                career.markUnclosed();
                _careerRepo.update(career);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void save(CareerDto career_dto)
        {
            try
            {
                _transactionManager.beginTransaction();
                Career career = new Career();
                _careerMaker.copy(ref career, career_dto);
                _careerRepo.insert(career);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void update(CareerDto career_dto)
        {
            try
            {
                _transactionManager.beginTransaction();
                Career career = _careerRepo.getById(career_dto.career_id);

                string oldImage = career.image_name;

                _careerMaker.copy(ref career, career_dto);
                _careerRepo.update(career);

                if (!string.IsNullOrWhiteSpace(career_dto.image_name))
                {
                    if (!string.IsNullOrWhiteSpace(oldImage))
                    {
                        deleteImage(oldImage);
                    }
                }
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        protected void deleteImage(string image_path)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/custom");
            if (File.Exists(Path.Combine(filePath, image_path)))
            {
                File.Delete(Path.Combine(filePath, image_path));
            }
        }

        public void delete(long career_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var career = _careerRepo.getById(career_id);

                if (career == null)
                {
                    throw new ItemNotFoundException($"Career with id {career_id} doesnot exist.");
                }

                _careerRepo.delete(career);

                deleteImage(career.image_name);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }
    }
}
