using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface MembersRepository
    {
        void insert(Member member);
        void update(Member member);
        void delete(Member member);
        List<Member> getAll();
        Member getById(long member_id);
        IQueryable<Member> getQueryable();
    }
}
