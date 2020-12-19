using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
   public interface FranchiseModelRepository
    {
        void insert(FranchiseModel franchiseModel);
        void update(FranchiseModel franchiseModel);
        void delete(FranchiseModel franchiseModel);
        List<FranchiseModel> getAll();
        FranchiseModel getBySlug(string slug);

        FranchiseModel getById(long franchise_model_id);
        FranchiseModel getByName(string name);
        IQueryable<FranchiseModel> getQueryable();
    }
}
