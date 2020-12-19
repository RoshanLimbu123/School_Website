using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
   public interface EnquiryService
    {
        void save(EnquiryDto enquiryDto);
        void update(EnquiryDto enquiryDto);
        void delete(long enquiry_id);
    }
}
