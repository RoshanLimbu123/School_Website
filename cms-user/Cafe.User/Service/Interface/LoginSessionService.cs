using CMS.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.User.Service.Interface
{
    public interface LoginSessionService
    {
        void save(LoginSessionDto session_dto);
    }
}
