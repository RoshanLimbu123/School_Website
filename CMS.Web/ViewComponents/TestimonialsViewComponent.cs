using CMS.Core.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.ViewComponents
{
    [ViewComponent(Name = "Testimonials")]
    public class TestimonialsViewComponent : ViewComponent
    {
        private readonly TestimonialRepository _testimonialRepo;

        public TestimonialsViewComponent(TestimonialRepository testimonialRepo)
        {
            _testimonialRepo = testimonialRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var testimonials = _testimonialRepo.getQueryable().Where(a => a.is_visible == true).ToList();

            return View(testimonials);
        }
    }
}
