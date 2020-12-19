using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class FranchiseModelMakerImpl : FranchiseModelMaker
    {
        private readonly SlugGenerator _slugGenerator;

        public FranchiseModelMakerImpl(SlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }


        public void copy(FranchiseModel franchiseModel, FranchiseModelDto franchiseModelDto)
        {
            franchiseModel.franchise_model_id = franchiseModelDto.franchise_model_id;
            franchiseModel.title = franchiseModelDto.title;
            franchiseModel.description = franchiseModelDto.description;
            franchiseModel.is_enabled = franchiseModelDto.is_enabled;
            if (!string.IsNullOrWhiteSpace(franchiseModelDto.file_name))
            {
                franchiseModel.file_name = franchiseModelDto.file_name;
            }
            franchiseModel.slug = _slugGenerator.generate(franchiseModelDto.title);

        }
    }
}
