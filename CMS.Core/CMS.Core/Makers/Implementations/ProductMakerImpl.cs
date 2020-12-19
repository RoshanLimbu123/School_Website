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
    public class ProductMakerImpl : ProductMaker
    {
        private readonly SlugGenerator _slugGenerator;

        public ProductMakerImpl(SlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }

        public void copy(ref Product product, ProductDto product_dto)
        {
            product.product_id = product_dto.product_id;
            product.item_category_id = product_dto.item_category_id;
            product.name = product_dto.name.Trim();
            product.description = product_dto.description.Trim();
            product.specification = product_dto.specification.Trim();
            product.features = product_dto.features.Trim();
            product.is_enabled = product_dto.is_enabled;
            if (!string.IsNullOrWhiteSpace(product_dto.file_name))
            {
                product.file_name = product_dto.file_name;
            }
            product.slug = _slugGenerator.generate(product_dto.name);
        }
    }
}
