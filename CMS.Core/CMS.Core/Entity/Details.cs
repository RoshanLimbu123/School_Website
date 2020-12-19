using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
    public class Details
    {
       

        [Key]
        public long details_id { get; set; }
        public long value1 { get; set; }
        public long value2 { get; set; }

        public long value3 { get; set; }

        public long value4 { get; set; }
        public long value0 { get; set; }








    }
}
