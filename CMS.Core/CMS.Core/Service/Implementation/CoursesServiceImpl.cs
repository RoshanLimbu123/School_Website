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
    public class CoursesServiceImpl : CoursesService
    {
        private readonly TransactionManager _transactionManager;
        private readonly CoursesRepository _productRepo;
        private readonly CoursesMaker _productMaker;
        private readonly FacultyRepository _facultyRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CoursesServiceImpl(TransactionManager transactionManager, CoursesRepository productRepo, CoursesMaker productMaker, FacultyRepository facultyRepository, IHostingEnvironment hostingEnvironment)
        {
            _facultyRepository = facultyRepository;
            _transactionManager = transactionManager;
            _productRepo = productRepo;
            _productMaker = productMaker;
            _hostingEnvironment = hostingEnvironment;
        }

        public void delete(long product_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var product = _productRepo.getById(product_id);

                if (product == null)
                {
                    throw new ItemNotFoundException($"Courses with id {product_id} doesnot exist.");
                }

                _productRepo.delete(product);

                deleteImage(product.file_name);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void disable(long product_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var product = _productRepo.getById(product_id);

                if (product == null)
                {
                    throw new ItemNotFoundException($"Courses with id {product_id} doesnot exist.");
                }

                product.disable();
                _productRepo.update(product);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void enable(long product_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var product = _productRepo.getById(product_id);

                if (product == null)
                {
                    throw new ItemNotFoundException($"Courses with id {product_id} doesnot exist.");
                }

                product.enable();
                _productRepo.update(product);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void save(CoursesDto product_dto)
        {
            try
            {
                _transactionManager.beginTransaction();

                Courses product = new Courses();
                _productMaker.copy(ref product, product_dto);
                if (product.faculty_id != 0)
                {
                    product.faculty = _facultyRepository.getById(product.faculty_id) ?? throw new ItemNotFoundException($"Faculty with the id {product.faculty_id} doesnot exist.");
                }

                _productRepo.insert(product);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void update(CoursesDto product_dto)
        {
            try
            {
                _transactionManager.beginTransaction();

                Courses product =_productRepo.getById(product_dto.course_id);

                string oldImage = product.file_name;
              
                _productMaker.copy(ref product, product_dto);
                if (product.faculty_id != 0)
                {
                    product.faculty = _facultyRepository.getById(product.faculty_id) ?? throw new ItemNotFoundException($"Faculty with the id {product.faculty_id} doesnot exist.");
                }
                _productRepo.update(product);

                if (!string.IsNullOrWhiteSpace(product_dto.file_name))
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

        private void deleteImage(string file_name)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/custom");
            if (File.Exists(Path.Combine(filePath, file_name)))
            {
                File.Delete(Path.Combine(filePath, file_name));
            }
        }

    }
}
