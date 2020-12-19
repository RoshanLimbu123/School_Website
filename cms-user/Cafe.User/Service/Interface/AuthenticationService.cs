using CMS.User.Dto;
using CMS.User.Entity;
using CMS.User.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.User.Service.Interface
{
    public interface AuthenticationService
    {
        void enable(long type_id, UserType type = UserType.user);
        void disable(long type_id, UserType type = UserType.user);
        void save(AuthenticationDto authentication_dto);
        void updateUsername(string new_name, long type_id, UserType type = UserType.user);
        void updatePassword(UpdatePasswordDto dto);

        Authentication validateUser(string username, string password);
    }
}
