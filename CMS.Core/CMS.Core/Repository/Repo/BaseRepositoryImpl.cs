using CMS.Core.Data;
using CMS.Core.Helper;
using CMS.Core.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Core.Repository.Repo
{
    public class BaseRepositoryImpl<T> : BaseRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly DetailsEncoder<T> _detailsEncoder;
        private readonly HtmlEncodingClassHelper _htmlEncodingClassHelper;

        public BaseRepositoryImpl(AppDbContext appDbContext, DetailsEncoder<T> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper)
        {
            _appDbContext = appDbContext;
            _detailsEncoder = detailsEncoder;
            _htmlEncodingClassHelper = htmlEncodingClassHelper;
        }

        public void delete(T entity)
        {
            var dbset = _appDbContext.Set<T>().Remove(entity);
            _appDbContext.SaveChanges();
        }

        public List<T> getAll()
        {
            return _appDbContext.Set<T>().ToList();
        }

        public T getById(long id)
        {
            return _appDbContext.Set<T>().Find(id);
        }

        public IQueryable<T> getQueryable()
        {
            return _appDbContext.Set<T>();
        }

        public void insert(T entity)
        {
            List<string> classesToBeExcluded = _htmlEncodingClassHelper.getAllClassNamesToBeExcluded();

            if (classesToBeExcluded.Contains(typeof(T).Name))
            {
                _appDbContext.Set<T>().Add(entity);
            }
            else
            {
                _appDbContext.Set<T>().Add(_detailsEncoder.encodeDetails(entity));
            }
            _appDbContext.SaveChanges();
        }

        public void update(T entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            List<string> classesToBeExcluded = _htmlEncodingClassHelper.getAllClassNamesToBeExcluded();

            if (classesToBeExcluded.Contains(typeof(T).Name))
            {
                _appDbContext.Set<T>().Update(entity);
            }
            else
            {
                _appDbContext.Set<T>().Update(_detailsEncoder.encodeDetails(entity));
            }
            
            _appDbContext.SaveChanges();
        }

    }
}
