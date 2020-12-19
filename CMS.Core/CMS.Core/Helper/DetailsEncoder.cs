using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Core.Helper
{
    public interface DetailsEncoder<T> where T:class
    {
        T encodeDetails(T entity);
    }
}
