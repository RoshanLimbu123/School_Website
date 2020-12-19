using CMS.User.Data;
using CMS.User.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.User.Repository.Repo
{
    public class BaseRepositoryImpl<T> : BaseRepository<T> where T : class
    {
        private readonly UserDbContext appDbContext;
        public BaseRepositoryImpl(UserDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }

        public void delete(T entity)
        {
            var dbset = appDbContext.Set<T>().Remove(entity);
        }

        public List<T> getAll()
        {
            return appDbContext.Set<T>().ToList();
        }

        public T getById(long id)
        {
            return appDbContext.Set<T>().Find(id);
        }

        public IQueryable<T> getQueryable()
        {
            return appDbContext.Set<T>();
        }

        public void insert(T entity)
        {
            if (appDbContext.Database.CurrentTransaction != null)
            {
                appDbContext.Set<T>().Add(entity);
            }
            else
            {
                using (var transaction = appDbContext.Database.BeginTransaction())
                {
                    appDbContext.Set<T>().Add(entity);
                }
            }
            appDbContext.SaveChanges();
        }

        public void update(T entity)
        {
            if (appDbContext.Database.CurrentTransaction != null)
            {
                appDbContext.SaveChanges();
            }
            else
            {
                using (var transaction = appDbContext.Database.BeginTransaction())
                {
                    appDbContext.SaveChanges();
                }
            }
        }
    }
}
