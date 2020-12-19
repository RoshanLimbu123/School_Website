using CMS.User.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.User.Helper
{
    public class TransactionManagerImpl : TransactionManager
    {
        [ThreadStatic]
        private static int count = 0;

        private UserDbContext _appDbContext;
        public TransactionManagerImpl(UserDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void beginTransaction()
        {
            if (count == 0)
            {
                //if (_appDbContext.Database.CurrentTransaction == null)
                //{
                _appDbContext.Database.BeginTransaction();
                // }
            }
            count++;
        }

        public void commitTransaction()
        {
            count--;
            if (count == 0)
            {
                //if (_appDbContext.Database.CurrentTransaction != null)
                //{
                _appDbContext.SaveChanges();
                _appDbContext.Database.CommitTransaction();
                // }
            }
        }

        public void rollbackTransaction()
        {
            count--;
            if (count == 0)
            {
                //if (_appDbContext.Database.CurrentTransaction != null)
                //{
                    _appDbContext.Database.RollbackTransaction();
               // }
            }
        }
    }
}
