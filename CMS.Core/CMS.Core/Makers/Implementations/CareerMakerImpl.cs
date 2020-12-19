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
    public class CareerMakerImpl : CareerMaker
    {
        private readonly SlugGenerator _slugGenerator;
        public CareerMakerImpl(SlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }
        public void copy(ref Career career, CareerDto career_dto)
        {
            career.career_id = career_dto.career_id;
            career.title = career_dto.title.Trim();
            career.opening_date = career_dto.opening_date;
            career.closing_date = career_dto.closing_date ;
            career.description = career_dto.description.Trim();
            if (!string.IsNullOrWhiteSpace(career_dto.image_name))
            {
                career.image_name = career_dto.image_name;
            }
            career.is_closed = career_dto.is_closed;
            career.slug = _slugGenerator.generate(career_dto.title);

        }
    }
}
