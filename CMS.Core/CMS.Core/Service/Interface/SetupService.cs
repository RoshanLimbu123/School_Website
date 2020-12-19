using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface SetupService
    {
        void saveOrUpdate(string key, string value);
        void saveOrUpdate(List<Setup> keyValue);
    }
}
