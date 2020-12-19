using CMS.User.Dto;
using CMS.User.Entity;
using CMS.User.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.User.Makers.Implementations
{
    public class LoginSessionMakerImpl : LoginSessionMaker
    {
        public void copy(LoginSession session, LoginSessionDto session_dto)
        {
            session.date_time = DateTime.Now;
            session.authentication_id = session_dto.authentication_id;
            session.type = session_dto.type;
        }
    }
}
