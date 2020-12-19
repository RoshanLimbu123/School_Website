using CMS.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.User.Service.Interface
{
    public interface UserService
    {
        void save(UserDto user_dto);
        void update(UserDto user_dto);
        void enable(long user_id);
        void disable(long user_id);
    }
}
