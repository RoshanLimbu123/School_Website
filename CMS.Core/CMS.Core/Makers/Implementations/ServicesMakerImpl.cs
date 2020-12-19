using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class ServicesMakerImpl : ServicesMaker
    {
        private readonly SlugGenerator _slugGenerator;

        public ServicesMakerImpl(SlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }
        public void copy(Services services, ServicesDto servicesDto)
        {
            services.service_id = servicesDto.service_id;
            services.name = servicesDto.name;
            services.description = servicesDto.description;
            if (!string.IsNullOrWhiteSpace(servicesDto.image))
            {
                services.image = servicesDto.image;
            }
            services.is_active = servicesDto.is_active;
            services.slug = _slugGenerator.generate(servicesDto.name);

        }
    }
}
