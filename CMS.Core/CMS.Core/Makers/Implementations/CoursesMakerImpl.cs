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
    public class CoursesMakerImpl : CoursesMaker
    {
        private readonly SlugGenerator _slugGenerator;

        public CoursesMakerImpl(SlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }

        public void copy(ref Courses product, CoursesDto product_dto)
        {
            product.course_id = product_dto.course_id;
            product.faculty_id = product_dto.faculty_id;
            product.name = product_dto.name.Trim();
            product.description = product_dto.description.Trim();
            product.specification = product_dto.specification.Trim();
            product.features = product_dto.features.Trim();
            product.is_enabled = product_dto.is_enabled;
            if (product_dto.file_name != null)
            {
                product.file_name = product_dto.file_name;
            }
            product.slug = _slugGenerator.generate(product_dto.name);
        }
    }
}
