using CMS.User.Dto;
using CMS.User.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.User.Makers.Interface
{
    public interface AuthenticationMaker
    {
        void copy(Authentication authentication, AuthenticationDto authentication_dto);
    }
}
