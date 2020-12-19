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
    public class NoticeServiceImpl : NoticeService
    {
        private readonly TransactionManager _transactionManager;
        private readonly NoticeRepository _noticeRepo;
        private readonly NoticeMaker _noticeMaker;
        private readonly IHostingEnvironment _hostingEnvironment;

        public NoticeServiceImpl(TransactionManager transactionManager, NoticeRepository noticeRepo, NoticeMaker noticeMaker, IHostingEnvironment hostingEnvironment)
        {
            _transactionManager = transactionManager;
            _noticeRepo = noticeRepo;
            _noticeMaker = noticeMaker;
            _hostingEnvironment = hostingEnvironment;
        }

        public void close(long notice_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var notice = _noticeRepo.getById(notice_id);

                if (notice == null)
                {
                    throw new ItemNotFoundException($"Notice with id {notice_id} doesnot exist.");
                }

                notice.close();
                _noticeRepo.update(notice);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void unclose(long notice_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var notice = _noticeRepo.getById(notice_id);

                if (notice == null)
                {
                    throw new ItemNotFoundException($"Notice with id {notice_id} doesnot exist.");
                }

                if(notice.notice_expiry_date< DateTime.Now)
                {
                    throw new InvalidDataException($"Please update expiry date of notice first.");
                }

                notice.unclose();
                _noticeRepo.update(notice);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void delete(long notice_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var notice = _noticeRepo.getById(notice_id);

                if (notice == null)
                {
                    throw new ItemNotFoundException($"Notice with id {notice_id} doesnot exist.");
                }

                string oldImage = notice.image_name;

                _noticeRepo.delete(notice);

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

        public void save(NoticeDto notice_dto)
        {
            try
            {
                _transactionManager.beginTransaction();
               
                Notice notice = new Notice();
                _noticeMaker.copy(ref notice, notice_dto);
                _noticeRepo.insert(notice);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void update(NoticeDto notice_dto)
        {
            try
            {
                _transactionManager.beginTransaction();
               
                Notice notice = _noticeRepo.getById(notice_dto.notice_id);

                string oldImage = notice.image_name;

                _noticeMaker.copy(ref notice, notice_dto);
                _noticeRepo.update(notice);

                if (!string.IsNullOrWhiteSpace(notice_dto.image_name))
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
