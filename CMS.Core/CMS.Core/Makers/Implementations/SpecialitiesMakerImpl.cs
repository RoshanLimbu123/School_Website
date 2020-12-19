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
    public class SpecialitiesMakerImpl : SpecialitiesMaker
    {
        private readonly SlugGenerator _slugGenerator;

        public SpecialitiesMakerImpl(SlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }

     
        public void copy(ref Specialities specialities, SpecialitiesDto specialitiesDto)
        {
            specialities.specialities_id = specialitiesDto.specialities_id;
            specialities.specialities_category_id = specialitiesDto.specialities_category_id;
            specialities.title = specialitiesDto.title.Trim();
            specialities.description = specialitiesDto.description.Trim();
            if (!string.IsNullOrWhiteSpace(specialitiesDto.image_name))
            {
                specialities.image_name = specialitiesDto.image_name;
            }
            specialities.is_enabled = specialitiesDto.is_enabled;
            specialities.slug = _slugGenerator.generate(specialitiesDto.title);
        }
    }
}
