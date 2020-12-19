using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class EnquiryMakerImpl : EnquiryMaker
    {
        public void copy(Enquiry enquiry, EnquiryDto enquiryDto)
        {
            enquiry.enquiry_id = enquiryDto.enquiry_id;
            enquiry.name = enquiryDto.name;
            enquiry.address = enquiryDto.address;
            enquiry.contact_number = enquiryDto.contact_number;
            enquiry.description = enquiryDto.description;
            enquiry.email = enquiryDto.email;
            enquiry.enquiry_date = enquiryDto.enquiry_date;
        }
    }
}
