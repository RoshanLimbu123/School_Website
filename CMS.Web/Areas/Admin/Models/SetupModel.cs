using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.Models
{
    public class SetupModel
    {
        private string _key, _value;

        public string key
        {
            get => _key;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new CustomException("Key is required.");
                }
                _key = value;
            }
        }
        public string value
        {
            get => _value;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new CustomException("Value is required.");
                }
                _value = value;
            }
        }
    }
}
