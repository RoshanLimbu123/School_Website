using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;

namespace CMS.Core.Makers.Implementations
{
    public class TestimonialMakerImpl : TestimonialMaker
    {
        public void copy(ref Testimonial testimonial, TestimonialDto testimonial_dto)
        {
            testimonial.testimonial_id = testimonial_dto.testimonial_id;
            testimonial.person_name = testimonial_dto.person_name.Trim();
            testimonial.statement = testimonial_dto.statement.Trim();
            testimonial.designation = testimonial_dto.designation.Trim();
            testimonial.associated_company_name = testimonial_dto.associated_company_name.Trim();
            if (!string.IsNullOrWhiteSpace(testimonial_dto.image_name))
            {
                testimonial.image_name = testimonial_dto.image_name;
            }
            testimonial.is_visible = testimonial_dto.is_visible;

        }
    }
}
