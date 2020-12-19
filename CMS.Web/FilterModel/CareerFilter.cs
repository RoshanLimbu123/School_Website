using CMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.FilterModel
{
    public class CareerFilter:PaginationFilter
    {
        public string name { get; set; }
    }
}
