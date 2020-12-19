using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CMS.Core.Helper
{
    public class DetailsEncoderImpl<T> : DetailsEncoder<T> where T : class
    {
        private HtmlEncoder _htmlEncoder;

        public DetailsEncoderImpl(HtmlEncoder htmlEncoder)
        {
            _htmlEncoder = htmlEncoder;
        }

        public T encodeDetails(T entity)
        {
            List<string> values = new List<string>();
            PropertyInfo[] properties = entity.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var val = prop.GetValue(entity, null);
                if (val != null && prop.PropertyType==typeof(string))
                    prop.SetValue(entity, Convert.ChangeType(_htmlEncoder.Encode(val.ToString()), prop.PropertyType), null);
            }
            return entity;
        }
    }
}
