using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface FranchiseModelService
    {
        void save(FranchiseModelDto franchiseModelDto);
        void update(FranchiseModelDto franchiseModelDto);
        void delete(long franchise_model_id);
        void enable(long franchise_model_id);
        void disable(long franchise_model_id);
    }
}