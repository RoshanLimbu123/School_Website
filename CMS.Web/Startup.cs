using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using CMS.Core.Data;
using CMS.Core.Helper;
using CMS.Core.Makers.Implementations;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Repository.Repo;
using CMS.Core.Service.Implementation;
using CMS.Core.Service.Interface;
using CMS.User.Data;
using CMS.User.Library;
using CMS.User.Makers.Implementations;
using CMS.User.Makers.Interface;
using CMS.User.Repository.Interface;
using CMS.User.Repository.Repo;
using CMS.User.Service.Implementations;
using CMS.User.Service.Interface;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace CMS.Web
{
    public class Startup
    {
        public IContainer ApplicationContainer { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("CMS.User"));
                options.ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning));
            });

            services.AddDbContext<AppDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("CMS.Core")));

            registerElements(services);
            services.AddAuthentication("userDetails")
               .AddCookie("userDetails", options =>
               {
                   // Cookie settings
                   options.Cookie.HttpOnly = false;
                   //  options.Cookie.Expiration = TimeSpan.FromDays(30);
                   options.LoginPath = "/account/login";
                   options.LogoutPath = "/account/logout";
                   options.AccessDeniedPath = "/error";

               });
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

                options.Filters.Add(new RequireHttpsAttribute());

            }).AddControllersAsServices()
              .AddJsonOptions(jsonOptions =>
              {
                  jsonOptions.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

              }).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options => options.DataAnnotationLocalizerProvider = (t, f) => f.Create(typeof(SharedResource)));

            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            Mapper.Initialize(x =>
            {
                x.AddProfile<Areas.Core.AutomapperProfiles.DomainProfile>();
                x.AddProfile<Areas.User_management.AutomapperProfiles.DomainProfile>();
                x.AddProfile<CMS.Web.AutomapperProfiles.DomainProfile>();
            });

            services.AddAutoMapper();
            services.AddResponseCaching();
            //for compressing data
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "imagejpeg", "png", "jpeg", "jpg" });
            });
            services.AddSession();

            services.Configure<GzipCompressionProviderOptions>(option =>
            option.Level = System.IO.Compression.CompressionLevel.Optimal);

            services.Configure<RequestLocalizationOptions>(
      opts =>
      {
          var supportedCultures = new List<CultureInfo>
          {

                new CultureInfo("en"),
                new CultureInfo("ne-NP"),
          };

          opts.DefaultRequestCulture = new RequestCulture("en");
          // Formatting numbers, dates, etc.
          opts.SupportedCultures = supportedCultures;
          // UI strings that we have localized.
          opts.SupportedUICultures = supportedCultures;
      });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error/{0}");
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true
            });
            app.UseResponseCaching();
            app.UseAuthentication();
            app.UseResponseCompression();

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Page}/{action=Index}/{id?}");

            });
        }

        private void registerElements(IServiceCollection services)
        {
            registerServices(services);
            registerRepos(services);
            registerMakers(services);
            registerHelpers(services);
            registerLibraries(services);

        }

        private void registerLibraries(IServiceCollection services)
        {
            registerUserLibraries(services);
            services.AddSingleton<PaginatedMetaService, PaginatedMetaServiceImpl>();
        }

        private void registerUserLibraries(IServiceCollection services)
        {
            services.AddScoped<EncryptDecrypt, EncryptDecryptImpl>();
            services.AddScoped<PasswordHash, PasswordHashImpl>();
        }

        private void registerHelpers(IServiceCollection services)
        {
            services.AddScoped<FileHelper, FileHelperImpl>();
            services.AddScoped<TransactionManager, TransactionManagerImpl>(); services.AddScoped(typeof(DetailsEncoder<>), typeof(DetailsEncoderImpl<>));
            services.AddScoped<HtmlEncodingClassHelper, HtmlEncodingClassHelperImpl>();
            services.AddScoped<SlugGenerator, SlugGeneratorImpl>();
            registerUserHelpers(services);

        }

        private void registerUserHelpers(IServiceCollection services)
        {
            services.AddScoped<CMS.User.Helper.TransactionManager, CMS.User.Helper.TransactionManagerImpl>();
        }

        private void registerMakers(IServiceCollection services)
        {
            registerUserMakers(services);
            registerCoreMakers(services);
        }

        private void registerCoreMakers(IServiceCollection services)
        {
            services.AddScoped<PageCategoryMaker, PageCategoryMakerImpl>();
            services.AddScoped<PageMaker, PageMakerImpl>();
            services.AddScoped<CareerMaker, CareerMakerImpl>();
            services.AddScoped<OutletMaker, OutletMakerImpl>();
            services.AddScoped<NoticeMaker, NoticeMakerImpl>();
            services.AddScoped<CoursesMaker, CoursesMakerImpl>();
            services.AddScoped<GalleryMaker, GalleryMakerImpl>();
            services.AddScoped<TestimonialMaker, TestimonialMakerImpl>();
            services.AddScoped<ReceivedEmailMaker, ReceivedEmailMakerImpl>();
            services.AddScoped<DesignationMaker, DesignationMakerImpl>();
            services.AddScoped<FiscalYearMaker, FiscalYearMakerImpl>();
            services.AddScoped<MembersMaker, MembersMakerImpl>();
            services.AddScoped<EventMaker, EventMakerImpl>();
            services.AddScoped<DetailsMaker, DetailsMakerImpl>();
            services.AddScoped<BlogMaker, BlogMakerImpl>();
            services.AddScoped<BlogCommentMaker, BlogCommentMakerImpl>();
            services.AddScoped<ClassesMaker, ClassesMakerImpl>();
            services.AddScoped<RoutineMaker, RoutineMakerImpl>();
            services.AddScoped<GalleryMaker, GalleryMakerImpl>();
            services.AddScoped<GalleryImageMaker, GalleryImageMakerImpl>();
            services.AddScoped<EmailSetupMaker, EmailSetupMakerImpl>();
            services.AddScoped<ExamTermMaker, ExamTermmakerImpl>();
            services.AddScoped<FacultyMaker, FacultyMakerImpl>();
            services.AddScoped<ExamTermMaker, ExamTermmakerImpl>();
            services.AddScoped<VideoMaker, VideoMakerImpl>();
            services.AddScoped<ServicesMaker, ServicesMakerImpl>();
            services.AddScoped<ProductMaker, ProductMakerImpl>();
            services.AddScoped<ItemCategoryMaker, ItemCategoryMakerImpl>();
            services.AddScoped<PartnersMaker, PartnersMakerImpl>();
            services.AddScoped<FranchiseModelMaker, FranchiseModelMakerImpl>();
            services.AddScoped<SpecialitiesMaker, SpecialitiesMakerImpl>();
            services.AddScoped<EnquiryMaker, EnquiryMakerImpl>();
            services.AddScoped<TeacherMaker, TeacherMakerImpI>();
            services.AddScoped<SpecialitiesCategoryMaker, SpecialitiesCategoryMakerImpl>();

        }

        private void registerUserMakers(IServiceCollection services)
        {
            services.AddScoped<AuthenticationMaker, AuthenticationMakerImpl>();
            services.AddScoped<LoginSessionMaker, LoginSessionMakerImpl>();
            services.AddScoped<UserMaker, UserMakerImpl>();
        }

        private void registerRepos(IServiceCollection services)
        {
            registerUserRepos(services);
            registerCoreRepos(services);
        }



        private void registerServices(IServiceCollection services)
        {
            registerUserServices(services);
            registerCoreServices(services);
        }



        private void registerUserRepos(IServiceCollection services)
        {
            services.AddScoped<AuthenticationRepository, AuthenticationRepositoryImpl>();
            services.AddScoped<LoginSessionRepository, LoginSessionRepositoryImpl>();
            services.AddScoped<RolePermissionMapRepository,
            RolePermissionMapRepositoryImpl>();
            services.AddScoped<UserRepository, UserRepositoryImpl>();
            services.AddScoped<UserRoleRepository, UserRoleRepositoryImpl>();
            services.AddScoped<RoleRepository, RoleRepositoryImpl>();
            services.AddScoped(typeof(User.Repository.Interface.BaseRepository<>), typeof(User.Repository.Repo.BaseRepositoryImpl<>));
        }

        private void registerCoreRepos(IServiceCollection services)
        {
            services.AddScoped<PageRepository, PageRepositoryImpl>();
            services.AddScoped<PageCategoryRepository, PageCategoryRepositoryImpl>();
            services.AddScoped<CareerRepository, CareerRepositoryImpl>();
            services.AddScoped<OutletRepository, OutletRepositoryImpl>();
            services.AddScoped<NoticeRepository, NoticeRepositoryImpl>();
            services.AddScoped<CoursesRepository, CoursesRepositoryImpl>();
            services.AddScoped<SetupRepository, SetupRepositoryImpl>();
            services.AddScoped<GalleryRepository, GalleryRepositoryImpl>();
            services.AddScoped<TestimonialRepository, TestimonialRepositoryImpl>();
            services.AddScoped<ReceivedEmailRepository, ReceivedEmailRepositoryImpl>();
            services.AddScoped<DesignationRepository, DesignationRepositoryImpl>();
            services.AddScoped<FiscalYearRepository, FiscalYearRepositoryImpl>();
            services.AddScoped<MembersRepository, MembersRepositoryImpl>();
            services.AddScoped<EventRepository, EventRepositoryImpl>();
            services.AddScoped<DetailsRepository, DetailsRepositoryImpl>();
            services.AddScoped<BlogRepository, BlogRepositoryImpl>();
            services.AddScoped<TeacherRepository, TeacherRepositoryImpI>();
            services.AddScoped<BlogCommentRepository, BlogCommentRepositoryImpl>();
            services.AddScoped<ClassesRepository, ClassesRepositoryImpl>();
            services.AddScoped<EmailsetupRepository, EmailsetupRepositoryImpl>();
            services.AddScoped<ExamTermRepository, ExamTermRepositoryImpl>();
            services.AddScoped<RoutineRepository, RoutineRepositoryImpl>();
            services.AddScoped<GalleryRepository, GalleryRepositoryImpl>();
            services.AddScoped<GalleryImageRepository, GalleryImageRepositoryImpl>();
            services.AddScoped<ExamTermRepository, ExamTermRepositoryImpl>();
            services.AddScoped<FacultyRepository, FacultyRepositoryImpl>();
            services.AddScoped<VideoRepository, VideoRepositoryImpl>();
            services.AddScoped<ServicesRepository, ServicesRepositoryImpl>();
            services.AddScoped<ProductRepository, ProductRepositoryImpl>();
            services.AddScoped<ItemCategoryRepository, ItemCategoryRepositoryImpl>();
            services.AddScoped<PartnersRepository, PartnersRepositoryImpl>();
            services.AddScoped<FranchiseModelRepository, FranchiseModelRepositoryImpl>();
            services.AddScoped<SpecialitiesRepository, SpecialitiesRepositoryImpl>();
            services.AddScoped<EnquiryRepository, EnquiryRepositoryImpl>();
            services.AddScoped<SpecialitiesCategoryRepository, SpecialitiesCategoryRepositoryImpl>();
            services.AddScoped(typeof(Core.Repository.Interface.BaseRepository<>), typeof(Core.Repository.Repo.BaseRepositoryImpl<>));
        }

        private void registerUserServices(IServiceCollection services)
        {
            services.AddScoped<AuthenticationService, AuthenticationServiceImpl>();
            services.AddScoped<LoginSessionService, LoginSessionServiceImpl>();
            services.AddScoped<RolePermissionMapService,
        RolePermissionMapServiceImpl>();
            services.AddScoped<UserRoleService, UserRoleServiceImpl>();
            services.AddScoped<RoleService, RoleServiceImpl>();
            services.AddScoped<UserService, UserServiceImpl>();
        }
        private void registerCoreServices(IServiceCollection services)
        {
            services.AddScoped<SlugGenerator, SlugGeneratorImpl>();
            services.AddScoped<PageCategoryService, PageCategoryServiceImpl>();
            services.AddScoped<PageService, PageServiceImpl>();
            services.AddScoped<CareerService, CareerServiceImpl>();
            services.AddScoped<OutletService, OutletServiceImpl>();
            services.AddScoped<GalleryService, GalleryServiceImpl>();
            services.AddScoped<NoticeService, NoticeServiceImpl>();
            services.AddScoped<CoursesService, CoursesServiceImpl>();
            services.AddScoped<SetupService, SetupServiceImpl>();
            services.AddScoped<TestimonialService, TestimonialServiceImpl>();
            services.AddScoped<EmailSenderService, EmailSenderServiceImpl>();
            services.AddScoped<DesignationService, DesignationServiceImpl>();
            services.AddScoped<FiscalYearService, FiscalYearServiceImpl>();
            services.AddScoped<MembersService, MembersServiceImpl>();
            services.AddScoped<EventService, EventServiceImpl>();
            services.AddScoped<DetailsService, DetailsServiceImpl>();
            services.AddScoped<BlogService, BlogServiceImpl>();
            services.AddScoped<BlogCommentService, BlogCommentServiceImpl>();
            services.AddScoped<ClassesService, ClassesServiceImpl>();
            services.AddScoped<EmailSetupService, EmailSetupServiceImpl>();
            services.AddScoped<GalleryService, GalleryServiceImpl>();
            services.AddScoped<GalleryImageService, GalleryImageServiceImpl>();
            services.AddScoped<ExamTermService, ExamTermServiceImpl>();
            services.AddScoped<RoutineService, RoutineServiceImpl>();
            services.AddScoped<FacultyService, FacultyServiceImpl>();
            services.AddScoped<ExamTermService, ExamTermServiceImpl>();
            services.AddScoped<VideoService, VideoServiceImpl>();
            services.AddScoped<ServicesService, ServicesServiceImpl>();
            services.AddScoped<ProductService, ProductServiceImpl>();
            services.AddScoped<ItemCategoryService, ItemCategoryServiceImpl>();
            services.AddScoped<PartnersService, PartnersServiceImpl>();
            services.AddScoped<FranchiseModelService, FranchiseModelServiceImpl>();
            services.AddScoped<SpecialitiesCategoryService, SpecialitiesCategoryServiceImpl>();
            services.AddScoped<SpecialitiesService, SpecialitiesServiceImpl>();
            services.AddScoped<EnquiryService, EnquiryServiceImpl>();
            services.AddScoped<TeacherService, TeacherServiceImpI>();
        }
    }
}
