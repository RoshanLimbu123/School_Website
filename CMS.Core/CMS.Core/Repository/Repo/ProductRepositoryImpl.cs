using CMS.Core.Data;
using CMS.Core.Entity;
using CMS.Core.Helper;
using CMS.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Repo
{
    public class ProductRepositoryImpl : BaseRepositoryImpl<Product>, ProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepositoryImpl(AppDbContext context, DetailsEncoder<Product> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }

        public List<Product> getByName(string name)
        {
          return  _appDbContext.products.Where(a => a.name == name).ToList();
        }

        public Product getBySlug(string slug)
        {
            return _appDbContext.products.Where(a => a.slug == slug).SingleOrDefault();
        }
    }
}