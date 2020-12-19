using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.AutomapperProfile
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Career, CareerDto>();
            CreateMap<Outlet, OutletDto>();
            CreateMap<FranchiseModel, FranchiseModelDto>();
            CreateMap<Gallery, GalleryDto>();
            CreateMap<Notice, NoticeDto>();
            CreateMap<PageCategory, PageCategoryDto>();
            CreateMap<PageCategoryDto, PageCategory>();
            CreateMap<Specialities, SpecialitiesDto>();
            CreateMap<SpecialitiesDto, Specialities>();
            CreateMap<Courses, CoursesDto>();
            CreateMap<Testimonial, TestimonialDto>();
            CreateMap<ReceivedEmail, ReceivedEmailDto>();
            CreateMap<Designation, DesignationDto>();
            CreateMap<FiscalYear, FiscalYearDto>();
            CreateMap<Member, MemberDto>();
            CreateMap<Event, EventDto>();
            CreateMap<Details, DetailsDto>();
            CreateMap<Blog, BlogDto>();
            CreateMap<BlogDto, Blog>();
            CreateMap<Video, VideoDto>();
            CreateMap<VideoDto, Video>();
            CreateMap<BlogComment, BlogCommentDto>();
            CreateMap<BlogCommentDto, BlogComment>();
            CreateMap<Services, ServicesDto>();
            CreateMap<ServicesDto, Services>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<ItemCategory, ItemCategoryDto>();
            CreateMap<ItemCategoryDto, ItemCategory>();
            CreateMap<Partners, PartnersDto>();
            CreateMap<PartnersDto, Partners>();

            CreateMap<Enquiry, EnquiryDto>();
            CreateMap<EnquiryDto, Enquiry>();

            CreateMap<Teacher, TeacherDto>();
            CreateMap<TeacherDto, Teacher>();

            CreateMap<Commerce, CommerceDto>();
            CreateMap<CommerceDto, Commerce>();

        }
    }
}
