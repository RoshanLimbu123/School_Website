using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Web.Areas.Core.Models;
using CMS.Web.Areas.Admin.ViewModels;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.ViewModels;


namespace CMS.Web.Areas.Core.AutomapperProfiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<CareerDto, Career>();

            CreateMap<OutletModel, OutletDto>();
            CreateMap<OutletDto, Outlet>();
            CreateMap<Outlet, OutletModel>();


            CreateMap<SpecialitiesCategoryModel, SpecialitiesCategoryDto>();
            CreateMap<SpecialitiesCategoryDto, SpecialitiesCategoryModel>();
            CreateMap<specialitiesCategoryDetailModel, SpecialitiesCategory>();
            CreateMap<SpecialitiesCategory, specialitiesCategoryDetailModel>();

            CreateMap<Specialities, SpecialitiesModel>();
            CreateMap<SpecialitiesDto, Specialities>();
            CreateMap<SpecialitiesModel, Specialities>();
            CreateMap<SpecialitiesDetailModel, Specialities>();
            CreateMap<Specialities, SpecialitiesDetailModel>();

          
            CreateMap<FranchiseModels, FranchiseModelDto>();
            CreateMap<FranchiseModelDto, FranchiseModel>();
            CreateMap<FranchiseModel, FranchiseModels>();


            CreateMap<ClassesModel, ClassesDto>();
            CreateMap<ClassesDto, Classes>();
            CreateMap<Classes, ClassesModel>();


            CreateMap<GalleryModel, GalleryDto>();
            CreateMap<GalleryDto, Gallery>();
            CreateMap<Gallery, GalleryModel>();

            CreateMap<NoticeDto, Notice>();

            CreateMap<PageCategoryModel, PageCategoryDto>();
            CreateMap<PageCategoryDto, PageCategoryModel>();
            CreateMap<PageCategoryDetailModel, PageCategory>();
            CreateMap<PageCategory, PageCategoryDetailModel>();

            CreateMap<PageDto, PageModel>();
            CreateMap<PageDto, Page>();
            CreateMap<PageModel, Page>();
            CreateMap<PageDetailModel, Page>();
            CreateMap<Page, PageDetailModel>();

            CreateMap<ViewModels.CourseDetail, Courses>();
            CreateMap<Courses, ViewModels.CourseDetail>();
            CreateMap<CoursesDto, Courses>();

            CreateMap<Web.ViewModels.CoursesDetail, Courses>();
            CreateMap<Courses,Web.ViewModels.CoursesDetail>();

            CreateMap<Web.ViewModels.PageDetail, Page>();
            CreateMap<Page, Web.ViewModels.PageDetail>();

            CreateMap<TestimonialDto, Testimonial>();

            CreateMap<ReceivedEmailDto, ReceivedEmail>();

            CreateMap<EventDto, Event>();
            CreateMap<Event, EventDto>();

            CreateMap<DetailsDto, Details>();
            CreateMap<Details, DetailsDto>();


            CreateMap<Details, DetailsModel>();
            CreateMap<DetailsModel, Details>();

            CreateMap<DetailsDetailModel, Details>();
            CreateMap<Details, DetailsDetailModel>();

            CreateMap<Event, EventModel>();
            CreateMap<EventModel, Event>();

            CreateMap<EventDetailModel, Event>();
            CreateMap<Event, EventDetailModel>();

            CreateMap<Blog, BlogDto>();
            CreateMap<BlogDto, Blog>();

            CreateMap<ViewModels.BlogDetailModel, Blog>();
            CreateMap<Blog, ViewModels.BlogDetailModel>();

            CreateMap<BlogCommentDetailModel, BlogComment>();
            CreateMap<BlogComment, BlogCommentDetailModel>();

            CreateMap<GalleryImageDetailModel, GalleryImage>();
            CreateMap<GalleryImage, GalleryImageDetailModel>();


            CreateMap<GalleryImage, Gallery>();
            CreateMap<Gallery, GalleryImage>();

            CreateMap<GalleryDetailModel, Gallery>();
            CreateMap<Gallery, GalleryDetailModel>();


            CreateMap<RoutineModel, Routine>();
            CreateMap<Routine, RoutineModel>();

            CreateMap<BlogCommentDetailModel, BlogComment>();
            CreateMap<BlogComment, BlogCommentDetailModel>();

            CreateMap<VideoDetails, Video>();
            CreateMap<Video, VideoDetails>();

            CreateMap<ServicesDetails, Services>();
            CreateMap<Services, ServicesDetails>();

            CreateMap<EnquiryDetailModel, Enquiry>();
            CreateMap<Enquiry, EnquiryDetailModel>();

            CreateMap<ViewModels.ProductDetail, Product>();
            CreateMap<Product, ViewModels.ProductDetail>();

            CreateMap<TeacherDetail, Teacher>();
            CreateMap<Teacher, TeacherDetail>();

        }
    }
}
