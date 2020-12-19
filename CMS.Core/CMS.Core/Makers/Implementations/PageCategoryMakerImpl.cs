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
    public class PageCategoryMakerImpl : PageCategoryMaker
    {
        private readonly SlugGenerator _slugGenerator;

        public PageCategoryMakerImpl(SlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }

        public void copy(ref PageCategory page_category, PageCategoryDto page_category_dto)
        {
            page_category.page_category_id = page_category_dto.page_category_id;
            page_category.name = page_category_dto.name.Trim();
            page_category.is_enabled = page_category_dto.is_enabled;
            page_category.slug = _slugGenerator.generate(page_category_dto.name);
        }
    }
}
