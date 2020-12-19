using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Helper
{
    public class HtmlEncodingClassHelperImpl : HtmlEncodingClassHelper
    {
        public List<string> getAllClassNamesToBeExcluded()
        {
            return new List<string>()
            {
              "Career",
              "FileUpload",
              "Gallery",
              "Notice",
              "Page",
              "Courses"
            };
        }
    }
}
