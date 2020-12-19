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
    public class SpecialitiesCategoryServiceImpl : SpecialitiesCategoryService
    {
        private SpecialitiesCategoryRepository _specialitiesCategoryRepository;
        private TransactionManager _transactionManager;
        private readonly SpecialitiesCategoryMaker _specialitiesCategoryMaker;
        private readonly IHostingEnvironment _hostingEnvironment;


        public SpecialitiesCategoryServiceImpl(IHostingEnvironment hostingEnvironment, SpecialitiesCategoryRepository specialitiesCategoryRepository, TransactionManager transactionManager, SpecialitiesCategoryMaker specialitiesCategoryMaker)
        {
            _specialitiesCategoryRepository = specialitiesCategoryRepository;
            _transactionManager = transactionManager;
            _specialitiesCategoryMaker = specialitiesCategoryMaker;
            _hostingEnvironment = hostingEnvironment;
        }

        public void delete(long specialities_category_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var specialitiesCategory = _specialitiesCategoryRepository.getById(specialities_category_id);
                if (specialitiesCategory == null)
                {
                    throw new ItemNotFoundException($"Specialities Category with id {specialities_category_id} doesn't exist.");
                }
               

                _specialitiesCategoryRepository.delete(specialitiesCategory);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }
      
            public void save(SpecialitiesCategoryDto specialitiesCategoryDto)
        {
            try
            {
                _transactionManager.beginTransaction();
                var specialitiesWithSameName = _specialitiesCategoryRepository.getByName(specialitiesCategoryDto.name);
                if (specialitiesWithSameName != null)
                {
                    throw new DuplicateItemException("specialities category with same name already exist.");
                }

                SpecialitiesCategory specialitiesCategory = new SpecialitiesCategory();
                _specialitiesCategoryMaker.copy(ref specialitiesCategory, specialitiesCategoryDto);
                _specialitiesCategoryRepository.insert(specialitiesCategory);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void update(SpecialitiesCategoryDto specialitiesCategoryDto)
        {
            try
            {
                _transactionManager.beginTransaction();

                var specialitiesCategoryId = _specialitiesCategoryRepository.getById(specialitiesCategoryDto.specialities_category_id);

                if (specialitiesCategoryId == null)
                {
                    throw new ItemNotFoundException($"specialities Category with id {specialitiesCategoryDto.specialities_category_id} doesnot exist.");
                }

                var specialitiesCategoryWithSameName = _specialitiesCategoryRepository.getByName(specialitiesCategoryDto.name);

                bool isNameAllowed = specialitiesCategoryWithSameName == null || specialitiesCategoryWithSameName.specialities_category_id == specialitiesCategoryDto.specialities_category_id;
                if (!isNameAllowed)
                {
                    throw new DuplicateItemException("specialities category with same name already exist.");
                }
                string oldImage = specialitiesCategoryId.image_name;

                _specialitiesCategoryMaker.copy(ref specialitiesCategoryId, specialitiesCategoryDto);
                _specialitiesCategoryRepository.update(specialitiesCategoryId);

            
                _specialitiesCategoryMaker.copy(ref specialitiesCategoryId, specialitiesCategoryDto);
                _specialitiesCategoryRepository.update(specialitiesCategoryId);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void enable(long specialities_category_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var specialitiesCategory = _specialitiesCategoryRepository.getById(specialities_category_id);
                if (specialitiesCategory == null)
                {
                    throw new ItemNotFoundException($"specialities Category with id {specialities_category_id} doesnot exist.");
                }

                specialitiesCategory.enable();
                _specialitiesCategoryRepository.update(specialitiesCategory);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void disable(long specialities_category_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var specialitiesCategory = _specialitiesCategoryRepository.getById(specialities_category_id);
                if (specialitiesCategory == null)
                {
                    throw new ItemNotFoundException($"specialities Category with id {specialities_category_id} doesnot exist.");
                }

                specialitiesCategory.disable();
                _specialitiesCategoryRepository.update(specialitiesCategory);
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
