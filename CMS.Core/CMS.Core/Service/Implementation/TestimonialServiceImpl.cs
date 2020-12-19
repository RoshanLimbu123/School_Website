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
    public class TestimonialServiceImpl : TestimonialService
    {
        private readonly TestimonialRepository _testimonialRepo;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly TransactionManager _transactionManager;
        private readonly TestimonialMaker _testimonialMaker;

        public TestimonialServiceImpl(TestimonialRepository testimonialRepo, IHostingEnvironment hostingEnvironment, TransactionManager transactionManager, TestimonialMaker testimonialMaker)
        {
            _testimonialRepo = testimonialRepo;
            _hostingEnvironment = hostingEnvironment;
            _transactionManager = transactionManager;
            _testimonialMaker = testimonialMaker;
        }

        public void delete(long testimonial_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var testimonial = _testimonialRepo.getById(testimonial_id);

                if (testimonial == null)
                {
                    throw new ItemNotFoundException($"Testimonial with id {testimonial_id} doesnot exist.");
                }

                string oldImage = testimonial.image_name;
                _testimonialRepo.delete(testimonial);

                deleteImage(oldImage);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void makeInvisible(long testimonial_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var testimonial = _testimonialRepo.getById(testimonial_id);

                if (testimonial == null)
                {
                    throw new ItemNotFoundException($"Testimonial with id {testimonial_id} doesnot exist.");
                }
                testimonial.makeInvisible();
                _testimonialRepo.update(testimonial);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void makeVisible(long testimonial_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var testimonial = _testimonialRepo.getById(testimonial_id);

                if (testimonial == null)
                {
                    throw new ItemNotFoundException($"Testimonial with id {testimonial_id} doesnot exist.");
                }
                testimonial.makeVisible();
                _testimonialRepo.update(testimonial);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void save(TestimonialDto testimonial_dto)
        {
            try
            {
                _transactionManager.beginTransaction();

                Testimonial testimonial = new Testimonial();
                _testimonialMaker.copy(ref testimonial, testimonial_dto);

                _testimonialRepo.insert(testimonial);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void update(TestimonialDto testimonial_dto)
        {
            try
            {
                _transactionManager.beginTransaction();


                Testimonial testimonial = _testimonialRepo.getById(testimonial_dto.testimonial_id);

                string oldImage = testimonial.image_name;

                _testimonialMaker.copy(ref testimonial, testimonial_dto);

                _testimonialRepo.update(testimonial);

                if (!string.IsNullOrWhiteSpace(testimonial_dto.image_name))
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
