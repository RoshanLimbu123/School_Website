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
using System.Linq;
using System.Text;

namespace CMS.Core.Service.Implementation
{
    public class SpecialitiesServiceImpl : SpecialitiesService
    {
        private SpecialitiesRepository _specialitiesRepository;
        private TransactionManager _transactionManager;
        private readonly SpecialitiesMaker _specialitiesMaker;
        private readonly IHostingEnvironment _hostingEnvironment;

        public SpecialitiesServiceImpl(IHostingEnvironment hostingEnvironment, SpecialitiesRepository specialitiesRepository, TransactionManager transactionManager, SpecialitiesMaker specialitiesMaker)
        {
            _specialitiesRepository = specialitiesRepository;
            _transactionManager = transactionManager;
            _specialitiesMaker = specialitiesMaker;
            _hostingEnvironment = hostingEnvironment;
        }

        public void delete(long specialities_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var specialities = _specialitiesRepository.getById(specialities_id);
                if (specialities == null)
                {
                    throw new ItemNotFoundException($"Specialities Category with id {specialities_id} doesn't exist.");
                }
                string oldImage = specialities.image_name;

                _specialitiesRepository.delete(specialities);
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

      

        public void enable(long specialities_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var specialities = _specialitiesRepository.getById(specialities_id);
                if (specialities == null)
                    throw new ItemNotFoundException($"Specialities with id {specialities_id} doesnot exist.");

                specialities.enable();
                _specialitiesRepository.update(specialities);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void disable(long specialities_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var specialities = _specialitiesRepository.getById(specialities_id);
                if (specialities == null)
                    throw new ItemNotFoundException($"Specialities with id {specialities_id} doesnot exist.");

                specialities.disable();
                _specialitiesRepository.update(specialities);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void save(SpecialitiesDto specialitiesDto)
        {
            try
            {
                _transactionManager.beginTransaction();
                bool isNameValid = checkNameValidity(specialitiesDto);
                if (!isNameValid)
                {
                    throw new DuplicateItemException("Specialities with same name already exist.");
                }
                Specialities specialities = new Specialities();
                _specialitiesMaker.copy(ref specialities, specialitiesDto);
                _specialitiesRepository.insert(specialities);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void update(SpecialitiesDto specialitiesDto)
        {
            try
            {
                _transactionManager.beginTransaction();

                var specialities = _specialitiesRepository.getById(specialitiesDto.specialities_id);
                if (specialities == null)
                {
                    throw new ItemNotFoundException($"Specialities with id {specialitiesDto.specialities_id} doesnot exist.");
                }

                bool isNameValid = checkNameValidity(specialitiesDto);
                if (!isNameValid)
                {
                    throw new DuplicateItemException("Specialities with same name already exist.");
                }

                string oldImage = specialities.image_name;

                _specialitiesMaker.copy(ref specialities, specialitiesDto);
                _specialitiesRepository.update(specialities);

                if (!string.IsNullOrWhiteSpace(specialitiesDto.image_name))
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

        private bool checkNameValidity(SpecialitiesDto specialitiesDto)
        {
            List<Specialities> specialitiesWithSameName = _specialitiesRepository.getByName(specialitiesDto.title.ToLower());
            var specialitiesWithSameNameInSameCategory = specialitiesWithSameName.Where(a => a.specialities_category_id == specialitiesDto.specialities_category_id).SingleOrDefault();

            if (specialitiesWithSameNameInSameCategory == null || specialitiesWithSameNameInSameCategory.specialities_id == specialitiesDto.specialities_id)
            {
                return true;
            }
            return false;
        }

        protected void deleteImage(string image_path)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/custom");
            if (File.Exists(Path.Combine(filePath, image_path)))
            {
                File.Delete(Path.Combine(filePath, image_path));

            }
        }
    }
}
