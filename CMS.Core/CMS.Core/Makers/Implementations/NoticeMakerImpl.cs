using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class NoticeMakerImpl : NoticeMaker
    {
        private readonly SlugGenerator _slugGenerator;

        public NoticeMakerImpl(SlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }
        public void copy(ref Notice notice, NoticeDto notice_dto)
        {
            notice.notice_id = notice_dto.notice_id;
            notice.notice_date = notice_dto.notice_date;
            notice.notice_expiry_date = notice_dto.notice_expiry_date;
            notice.description = notice_dto.description.Trim();
            if (!string.IsNullOrWhiteSpace(notice_dto.image_name))
            {
                notice.image_name = notice_dto.image_name;
            }

            notice.title = notice_dto.title.Trim();
            notice.is_closed = notice_dto.is_closed;
            notice.slug = _slugGenerator.generate(notice_dto.title);
        }
    }
}
