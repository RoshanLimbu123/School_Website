using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
   public class GalleryServiceImpl: GalleryService
    {
        private readonly GalleryRepository _galleryRepository;
        private readonly GalleryMaker _galleryMaker;
        public GalleryServiceImpl(GalleryRepository galleryRepository, GalleryMaker galleryMaker  )
        { 
            _galleryRepository = galleryRepository;
            _galleryMaker = galleryMaker;
        }

        public void active(long gallery_id)
        {
            try
            {
                var gallery = _galleryRepository.getById(gallery_id);
                if (gallery == null)
                    throw new ItemNotFoundException($"Gallery with id {gallery_id} doesnot exist.");

                gallery.is_active = true;
                _galleryRepository.update(gallery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void delete(long gallery_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var gallerycategory = _galleryRepository.getById(gallery_id);
                    if (gallerycategory == null)
                    {
                        throw new ItemNotFoundException($"gallery1 Category With Id {gallerycategory} doesnot Exist.");
                    }

                    _galleryRepository.delete(gallerycategory);
                    tx.Complete();
                }
            }

            catch (Exception)
            {

                throw;
            }
        }

        public void inactive(long gallery_id)
        {
            try
            {
                var gallery = _galleryRepository.getById(gallery_id);
                if (gallery == null)
                    throw new ItemNotFoundException($"Gallery with id {gallery_id} doesnot exist.");

                gallery.is_active = false;
                _galleryRepository.update(gallery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void save(GalleryDto gallery1Dto)
        {

            try
            {
                using(TransactionScope txe =new TransactionScope(TransactionScopeOption.Required))
                {
                    var gallery = new Gallery();
                    _galleryMaker.copy(gallery,gallery1Dto);
                    _galleryRepository.insert(gallery);
                    txe.Complete();
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public void update(GalleryDto gallery1Dto)
        {
            try
            {
                using (TransactionScope txe = new TransactionScope(TransactionScopeOption.Required))
                {

                    Gallery gallery1 = _galleryRepository.getById(gallery1Dto.gallery_id);

                    if (gallery1 == null)
                    {
                        throw new ItemNotFoundException($"gallery with ID {gallery1Dto.gallery_id} doesnot Exit.");
                    }

                    _galleryMaker.copy(gallery1, gallery1Dto);
                    _galleryRepository.update(gallery1); ;
                    txe.Complete();
                }

            }
            
            catch (Exception)
            {

                throw;
            }
        }
    }
}
