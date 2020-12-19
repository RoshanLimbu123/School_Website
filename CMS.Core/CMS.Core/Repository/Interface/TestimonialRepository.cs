using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface TestimonialRepository
    {
        void insert(Testimonial testimonial);
        void update(Testimonial testimonial);
        void delete(Testimonial testimonial);
        List<Testimonial> getAll();
        Testimonial getById(long testimonial_id);
        IQueryable<Testimonial> getQueryable();
    }
}
