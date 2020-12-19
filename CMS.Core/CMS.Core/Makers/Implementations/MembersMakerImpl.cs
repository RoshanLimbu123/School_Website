using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class MembersMakerImpl : MembersMaker
    {
        public void copy(ref Member member, MemberDto memberDto)
        {
            member.member_id = memberDto.member_id;

            member.Designation_id = memberDto.Designation_id;

            member.fiscal_year_id = memberDto.fiscal_year_id;
            member.full_name = memberDto.full_name;
            member.address = memberDto.address;
            member.is_contact_enabled = memberDto.is_contact_enabled;
            member.contact_number = memberDto.contact_number;
            member.description = memberDto.description;
            if (!string.IsNullOrWhiteSpace(memberDto.image_url))
            {
                member.image_url = memberDto.image_url;
            }
        }
    }
}
