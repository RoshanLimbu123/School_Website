using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Core.Helper
{
    public interface TransactionManager
    {
        void beginTransaction();
        void commitTransaction();
        void rollbackTransaction();
    }
}
