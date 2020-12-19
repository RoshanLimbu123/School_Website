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
    public class PageMakerImpl : PageMaker
    {
        private readonly SlugGenerator _slugGenerator;

        public PageMakerImpl(SlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }

        public void copy(ref Page page, PageDto page_dto)
        {
            page.page_id = page_dto.page_id;
            page.page_category_id = page_dto.page_category_id;
            page.title = page_dto.title.Trim();
            page.description = page_dto.description.Trim();
            if (!string.IsNullOrWhiteSpace(page_dto.image_name))
            {
                page.image_name = page_dto.image_name;
            }
            page.is_enabled = page_dto.is_enabled;
            page.slug = _slugGenerator.generate(page_dto.title);
        }
    }
}
