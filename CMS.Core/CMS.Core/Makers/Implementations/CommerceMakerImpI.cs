using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class CommerceMakerImpI : CommerceMaker
    {
        private readonly SlugGenerator _slugGenerator;

        public CommerceMakerImpI(SlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }
        public void copy(ref Commerce commerce, CommerceDto commerceDto)
        {
            commerce.commerce_id = commerceDto.commerce_id;
            commerce.title = commerceDto.title;
            commerce.description = commerceDto.description;
            commerce.is_enabled = commerceDto.is_enabled;
            if(!string.IsNullOrWhiteSpace(commerceDto.image_name))
            {
                commerce.image_name = commerceDto.image_name;
            }
            commerce.slug = _slugGenerator.generate(commerceDto.title);
        }
    }
}
