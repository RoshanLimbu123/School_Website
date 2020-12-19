using CMS.Core.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace CMS.Core.Helper
{
    public class TransactionManagerImpl : TransactionManager
    {
        [ThreadStatic]
        private static int count = 0;

        private IDbContextTransaction transaction;

        private AppDbContext _appDbContext;
        public TransactionManagerImpl(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void beginTransaction()
        {
            if (count == 0)
            {
                transaction = _appDbContext.Database.BeginTransaction();
            }
            count++;
        }

        public void commitTransaction()
        {
            count--;
            if (count == 0)
            {
                transaction.Commit();
            }
        }

        public void rollbackTransaction()
        {
            count--;
            if (count == 0)
            {
                transaction.Rollback();
            }
        }
    }
}
