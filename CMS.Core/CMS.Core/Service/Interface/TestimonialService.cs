using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface TestimonialService
    {
        void save(TestimonialDto testimonial_dto);
        void update(TestimonialDto testimonial_dto);
        void delete(long testimonial_id);
        void makeVisible(long testimonial_id);
        void makeInvisible(long testimonial_id);
    }
}
