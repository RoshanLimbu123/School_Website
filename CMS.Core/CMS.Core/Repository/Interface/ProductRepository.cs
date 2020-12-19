using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface ProductRepository
    {
        void insert(Product product);
        void update(Product product);
        void delete(Product product);
        List<Product> getAll();
        List<Product> getByName(string name);
        Product getById(long gallery_id);
        Product getBySlug(string slug);
        IQueryable<Product> getQueryable();
    }
}
