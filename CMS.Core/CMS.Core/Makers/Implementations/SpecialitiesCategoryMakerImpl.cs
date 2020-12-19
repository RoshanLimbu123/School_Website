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
    public class SpecialitiesCategoryMakerImpl : SpecialitiesCategoryMaker
    {
        private readonly SlugGenerator _slugGenerator;

        public SpecialitiesCategoryMakerImpl(SlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }

    
        public void copy(ref SpecialitiesCategory specialitiesCategory, SpecialitiesCategoryDto specialitiesCategoryDto)
        {
            specialitiesCategory.specialities_category_id = specialitiesCategoryDto.specialities_category_id;
            specialitiesCategory.name = specialitiesCategoryDto.name.Trim();
            specialitiesCategory.description = specialitiesCategoryDto.description;
            if (!string.IsNullOrWhiteSpace(specialitiesCategoryDto.image_name))
            {
                specialitiesCategory.image_name = specialitiesCategoryDto.image_name;
            }
            specialitiesCategory.is_enabled = specialitiesCategoryDto.is_enabled;
            specialitiesCategory.slug = _slugGenerator.generate(specialitiesCategoryDto.name);
           
        }
    }
}
