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
    public class GalleryImageServiceImpl : GalleryImageService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private GalleryImageRepository _galleryRepo;
        private TransactionManager _transactionManager;
        private readonly GalleryImageMaker _galleryMaker;


        public GalleryImageServiceImpl(GalleryImageRepository galleryRepo, TransactionManager transactionManager, GalleryImageMaker galleryMaker, IHostingEnvironment hostingEnvironment)
        {
            _galleryRepo = galleryRepo;
            _transactionManager = transactionManager;
            _galleryMaker = galleryMaker;
            _hostingEnvironment = hostingEnvironment;
        }

        public void delete(long gallery_image_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var gallery = _galleryRepo.getById(gallery_image_id);
                if (gallery == null)
                {
                    throw new ItemNotFoundException($"Gallery with id {gallery_image_id} doesn't exist.");
                }
                string oldImage = gallery.image_name;
                _galleryRepo.delete(gallery);
                deleteImage(oldImage);
                _transactionManager.commitTransaction();

            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void save(GalleryImageDto galleryDto)
        {
            try
            {
                _transactionManager.beginTransaction();
              
                GalleryImage gallery = new GalleryImage();
                _galleryMaker.copy(gallery, galleryDto);
                _galleryRepo.insert(gallery);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }


        public void update(GalleryImageDto galleryDto)
        {
            try
            {
                _transactionManager.beginTransaction();

                var gallery = _galleryRepo.getById(galleryDto.gallery_id);
                if (gallery == null)
                {
                    throw new ItemNotFoundException($"Gallery with id {galleryDto.gallery_id} doesnot exist.");
                }

                string oldImage = gallery.image_name;
              
                _galleryMaker.copy(gallery, galleryDto);
                _galleryRepo.update(gallery);

                if (!string.IsNullOrWhiteSpace(galleryDto.image_name))
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

        public void enable(long gallery_image_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var gallery = _galleryRepo.getById(gallery_image_id);
                if (gallery == null)
                    throw new ItemNotFoundException($"Image with id {gallery_image_id} doesnot exist.");

                gallery.enable();
                _galleryRepo.update(gallery);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void disable(long gallery_image_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var gallery = _galleryRepo.getById(gallery_image_id);
                if (gallery == null)
                    throw new ItemNotFoundException($"Image with id {gallery_image_id} doesnot exist.");

                gallery.disable();
                _galleryRepo.update(gallery);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void makeSliderImage(long gallery_image_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var gallery = _galleryRepo.getById(gallery_image_id);
                if (gallery == null)
                {
                    throw new ItemNotFoundException($"Image with id {gallery_image_id} doesn't exist.");
                }
                  
                gallery.markSliderImage();

                _galleryRepo.update(gallery);
                _transactionManager.commitTransaction();

            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void removeFromSliderImage(long gallery_image_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var gallery = _galleryRepo.getById(gallery_image_id);
                if (gallery == null)
                {
                    throw new ItemNotFoundException($"Image with id {gallery_image_id} doesn't exist.");
                }

                gallery.removeFromSliderImage();

                _galleryRepo.update(gallery);
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

        public void custom(long gallery_image_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var gallery = _galleryRepo.getById(gallery_image_id);
                if (gallery == null)
                    throw new ItemNotFoundException($"Image with id {gallery_image_id} doesnot exist.");

                gallery.custom();
                _galleryRepo.update(gallery);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }
    

    public void default1(long gallery_image_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var gallery = _galleryRepo.getById(gallery_image_id);
                if (gallery == null)
                    throw new ItemNotFoundException($"Image with id {gallery_image_id} doesnot exist.");

                gallery.default1();
                _galleryRepo.update(gallery);
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
