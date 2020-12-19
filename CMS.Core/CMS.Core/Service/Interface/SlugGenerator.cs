using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface SlugGenerator
    {
        string generate(string title);
    }
}
