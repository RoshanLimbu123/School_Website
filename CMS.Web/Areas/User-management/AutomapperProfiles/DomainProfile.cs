using AutoMapper;
using CMS.User.Dto;
using CMS.User.Entity;
using CMS.Web.Areas.User_management.Models;
using CMS.Web.Areas.User_management.ViewModels;

namespace CMS.Web.Areas.User_management.AutomapperProfiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Role, RoleDetailModel>();
            CreateMap<RoleModel, Role>();
            CreateMap<Role, RoleModel>();
            CreateMap<User.Entity.User, UserDetailModel>();
            CreateMap<UserModel, UserDto>();
            CreateMap<User.Entity.User, UserModel>();
        }
    }
}
