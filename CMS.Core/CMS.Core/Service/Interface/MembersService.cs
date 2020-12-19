using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface MembersService
    {
        void save(MemberDto member_dto);
        void update(MemberDto member_dto);
        void delete(long member_id);
    }
}
