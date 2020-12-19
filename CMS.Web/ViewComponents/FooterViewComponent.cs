using CMS.Core.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CMS.Web.ViewComponents
{
    [ViewComponent(Name = "FooterView")]
    public class FooterViewComponent : ViewComponent
    {
        public GalleryRepository _galleryRepo { get; set; }
        public NoticeRepository _noticeRepo { get; set; }
        public SetupRepository _setupRepo { get; set; }
        public PageCategoryRepository _pageCategoryRepo { get; set; }
        public FooterViewComponent(GalleryRepository galleryRepo, NoticeRepository noticeRepo, SetupRepository setupRepo, PageCategoryRepository pageCategoryRepo)
        {
            _galleryRepo = galleryRepo;
            _noticeRepo = noticeRepo;
            _setupRepo = setupRepo;
            _pageCategoryRepo = pageCategoryRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var setupValues = _setupRepo.getQueryable().ToList();
            ViewBag.setup = setupValues;

            var pageCategory = _pageCategoryRepo.getQueryable().Where(a => a.is_enabled == true).ToList();
            ViewBag.pageCategories = pageCategory;
            var notice = _noticeRepo.getQueryable().Where(n => n.notice_expiry_date.Date >= TimeZoneInfo.ConvertTime(DateTime.Now,
      TimeZoneInfo.FindSystemTimeZoneById("Nepal Standard Time")).Date && n.is_closed == false).ToList().Take(3);
            ViewBag.notices = notice;

            return View();
        }
    }
}
