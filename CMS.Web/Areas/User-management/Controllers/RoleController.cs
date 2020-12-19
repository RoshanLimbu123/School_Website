using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.User.Dto;
using CMS.User.Entity;
using CMS.User.Repository.Interface;
using CMS.User.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMS.Web.Areas.User_management.Models;
using CMS.Web.Areas.User_management.ViewModels;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;

namespace CMS.Web.Areas.User_management.Controllers
{
    [Authorize]
    [Area("user-management")]
    [Route("user-management/role")]
    public class RoleController : Controller
    {
        private RoleRepository _roleRepo;
        private readonly RoleService _roleService;
        private PaginatedMetaService _paginatedMetaService;

        private readonly IMapper _mapper;

        public RoleController(RoleRepository roleRepo, IMapper mapper, RoleService roleService, PaginatedMetaService paginatedMetaService)
        {
            _roleRepo = roleRepo;
            _mapper = mapper;
            _roleService = roleService;
            _paginatedMetaService = paginatedMetaService;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            try
            {
                var role = _roleRepo.getQueryable();
                var roles = role.ToList();
                var roleIndexVM = getViewModelFrom(roles);
                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(50, 2, 6);
                return View(roleIndexVM);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("/home");
            }
        }

        [HttpGet]
        [Route("new")]
        public IActionResult add()
        {
            try
            {
                RoleModel roleModel = new RoleModel();
                roleModel.permission_datas = Constants.Permissions.getPermissions();
                return View(roleModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(RoleModel model, List<PermissionModel> permission)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RoleDto roleDto = new RoleDto()
                    {
                        name = model.name,
                        permissions = permission.Where(a => a.is_checked).Select(a => a.permission_name).ToList(),
                        is_active = model.is_enabled
                    };
                    _roleService.save(roleDto);
                    AlertHelper.setMessage(this, "Role saved successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            finally
            {
                model.permission_datas = permission;
            }
            return View(model);
        }

        [HttpGet]
        [Route("edit/{role_id}")]
        public IActionResult edit(long role_id)
        {
            try
            {
                Role role = _roleRepo.getById(role_id);
                RoleModel roleModel = _mapper.Map<RoleModel>(role);
                var permissions = Constants.Permissions.getPermissions();

                foreach (var permission in role.permissions)
                {
                    permissions.Where(a => a.permission_name == permission.permission_name).SingleOrDefault().is_checked = true;
                }
                roleModel.permission_datas = permissions;
                RouteData.Values.Remove("role_id");
                return View(roleModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }

        }

        [HttpPost]
        [Route("edit")]
        public IActionResult edit(RoleModel model, List<PermissionModel> permission)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RoleDto roleDto = new RoleDto()
                    {
                        role_id = model.role_id,
                        name = model.name,
                        permissions = permission.Where(a => a.is_checked).Select(a => a.permission_name).ToList(),
                        is_active = model.is_enabled
                    };
                    _roleService.update(roleDto);
                    AlertHelper.setMessage(this, "Role updated successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(model);
        }

        [HttpGet]
        [Route("enable/{role_id}")]
        public IActionResult enable(long role_id)
        {
            try
            {
                _roleService.enable(role_id);
                AlertHelper.setMessage(this, "Role enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{role_id}")]
        public IActionResult disable(long role_id)
        {
            try
            {
                _roleService.disable(role_id);
                AlertHelper.setMessage(this, "Role disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("delete/{role_id}")]
        public IActionResult delete(long role_id)
        {
            try
            {
                _roleService.delete(role_id);
                AlertHelper.setMessage(this, "Role deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        private RoleIndexViewModel getViewModelFrom(List<Role> roles)
        {
            RoleIndexViewModel vm = new RoleIndexViewModel();
            vm.role_details = new List<RoleDetailModel>();
            foreach (var role in roles)
            {
                var roleDetail = _mapper.Map<RoleDetailModel>(role);
                roleDetail.is_active = role.is_enabled;
                roleDetail.permissions = role.permissions.Select(a => a.permission_name).ToList();

                vm.role_details.Add(roleDetail);
            }

            return vm;
        }
    }
}
