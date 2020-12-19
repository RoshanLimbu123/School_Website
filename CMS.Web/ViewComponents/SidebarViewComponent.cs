using CMS.Core.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.ViewComponents
{
    [ViewComponent(Name = "Sidebar")]
    public class SidebarViewConponent : ViewComponent
    {
        public SetupRepository _setupRepo { get; set; }

        public SidebarViewConponent(SetupRepository setupRepo)
        {
            _setupRepo = setupRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var setup = _setupRepo.getQueryable().ToList();

            return View(setup);
        }
    }
}
