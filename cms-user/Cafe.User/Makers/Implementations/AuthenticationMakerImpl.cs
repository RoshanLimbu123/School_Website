using CMS.User.Dto;
using CMS.User.Entity;
using CMS.User.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.User.Makers.Implementations
{
    public class AuthenticationMakerImpl : AuthenticationMaker
    {
        public void copy(Authentication authentication, AuthenticationDto authentication_dto)
        {
            authentication.is_enabled = authentication_dto.is_active;
            authentication.username = authentication_dto.username;
            authentication.type = authentication_dto.type;
            authentication.type_id = authentication_dto.type_id;
        }
    }
}
