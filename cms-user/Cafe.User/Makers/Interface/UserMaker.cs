using CMS.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.User.Makers.Interface
{
    using userEntity = User.Entity.User;
    public interface UserMaker
    {
        void copy(userEntity user,UserDto user_dto);
    }
}
