using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class PartnersMakerImpl : PartnersMaker
    {
        public void copy(Partners partners, PartnersDto partnersDto)
        {
            partners.partners_id = partnersDto.partners_id;
            partners.name = partnersDto.name;
            partners.logo_name = partnersDto.logo_name;
            partners.web_url = partnersDto.web_url;
            partners.is_active = partnersDto.is_active;
        }
    }
}
