using CMS.User.Dto;
using CMS.User.Entity;
using CMS.User.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.User.Makers.Implementations
{
    public class UserMakerImpl : UserMaker
    {
        public void copy(Entity.User user, UserDto user_dto)
        {
            user.user_id = user_dto.user_id;
           
            user.full_name = user_dto.full_name;
            user.address_line_1 = user_dto.address_line_1;
            user.address_line_2 = user_dto.address_line_2;
            user.primary_contact = user_dto.primary_contact;
            user.secondary_contact = user_dto.secondary_contact;
            user.email = user_dto.email;
            user.created_by = user_dto.created_by;
            user.is_active = user_dto.is_active;
            user.image_path = user_dto.image_path;
        }
    }
}
