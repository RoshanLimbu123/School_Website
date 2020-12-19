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
    public class ItemCategoryMakerImpl : ItemCategoryMaker
    {
        private readonly SlugGenerator _slugGenerator;

        public ItemCategoryMakerImpl(SlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }

        public void copy(ref ItemCategory item_category, ItemCategoryDto item_category_dto)
        {
            item_category.item_category_id = item_category_dto.item_category_id;
            item_category.name = item_category_dto.name.Trim();
            item_category.description = item_category_dto.description.Trim();
            item_category.is_enabled = item_category_dto.is_enabled;
            item_category.slug = _slugGenerator.generate(item_category_dto.name);
        }
    }
}
