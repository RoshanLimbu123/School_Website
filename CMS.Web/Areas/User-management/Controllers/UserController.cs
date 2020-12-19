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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CMS.Web.Areas.User_management.FilterModel;
using CMS.Web.Areas.User_management.Models;
using CMS.Web.Areas.User_management.ViewModels;
using CMS.Web.Controllers;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;

namespace CMS.Web.Areas.User_management.Controllers
{
    [Authorize]
    [Area("user-management")]
    [Route("user-management/user")]
    public class UserController : BaseController
    {
        public readonly UserRepository _userRepo;
        private readonly AuthenticationRepository _authenticationRepo;
        private readonly UserRoleRepository _userRoleRepo;
        private readonly IMapper _mapper;
        private readonly RoleRepository _roleRepo;
        private readonly UserService _userService;
        private readonly FileHelper _fileHelper;
        private readonly RolePermissionMapRepository _rolePermissionMapRepo;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly AuthenticationService _authenticationService;

        public UserController(UserRepository userRepo, IMapper mapper, AuthenticationRepository authenticationRepo, UserRoleRepository userRoleRepo, RoleRepository roleRepo, UserService userService, FileHelper fileHelper, RolePermissionMapRepository rolePermissionMapRepo, AuthenticationService authenticationService,PaginatedMetaService paginatedMetaService) : base()
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _authenticationRepo = authenticationRepo;
            _userRoleRepo = userRoleRepo;
            _roleRepo = roleRepo;
            _userService = userService;
            _fileHelper = fileHelper;
            _rolePermissionMapRepo = rolePermissionMapRepo;
            _authenticationService = authenticationService;
            _paginatedMetaService = paginatedMetaService;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(UserFilter filter=null)
        {
            try
            {
                var users = _userRepo.getQueryable();

                if (!string.IsNullOrWhiteSpace(filter.name))
                {
                    users = users.Where(a => a.full_name.Contains(filter.name));
                }

                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(users.Count(), filter.page, filter.number_of_rows);


                users = users.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

                var userDetails = users.ToList();

                UserIndexViewModel userIndexVM = getUserIndexVMFromUserList(userDetails);

                return View(userIndexVM);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("/home");
            }
        }

        [Route("new")]
        [HttpGet]
        public IActionResult add()
        {
            try
            {
                var roles = _roleRepo.getAll();

                List<AssignedRole> availableRoles = new List<AssignedRole>();

                foreach (var role in roles)
                {
                    availableRoles.Add(new AssignedRole()
                    {
                        role_name = role.name,
                        role_id = role.role_id
                    });
                }
                ViewBag.roles = availableRoles;
                return View();
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("/home");
            }
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(UserModel user_model, List<AssignedRole> roles, IFormFile file)
        {
            if (string.IsNullOrWhiteSpace(user_model.password))
            {
                throw new Exception("Password is required.");
            }
            if (user_model.password != user_model.confirm_paswword)
            {
                throw new Exception("Passwords didnot match.");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    UserDto userDto = getUserDtoFromProvidedDatas(user_model, roles);

                    if (file != null)
                    {
                        string fileName = userDto.full_name;
                        userDto.image_path = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _userService.save(userDto);
                    AlertHelper.setMessage(this, "User saved successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            finally
            {
                var roleDatas = _roleRepo.getAll();
                List<AssignedRole> availableRoles = new List<AssignedRole>();
                foreach (var role in roleDatas)
                {
                    availableRoles.Add(new AssignedRole()
                    {
                        role_name = role.name,
                        role_id = role.role_id
                    });
                }
                ViewBag.roles = availableRoles;
            }
            return View();
        }

        [Route("edit/{user_id}")]
        [HttpGet]
        public IActionResult edit(long user_id)
        {
            try
            {
                var roles = _roleRepo.getAll();

                List<AssignedRole> availableRoles = new List<AssignedRole>();

                var assignedRoles = _userRoleRepo.getByTypeId(CMS.User.Enums.UserType.user, user_id);

                var assignedRoleNames = assignedRoles.Select(a => a.role).ToList();

                foreach (var role in roles)
                {
                    var roleDetail = new AssignedRole()
                    {
                        role_name = role.name,
                        role_id = role.role_id
                    };

                    roleDetail.is_checked = assignedRoleNames.Contains(role);

                    availableRoles.Add(roleDetail);
                }
                ViewBag.roles = availableRoles;

                var userDetail = _userRepo.getById(user_id);

                var userModel = getUserModelFromUserDetail(userDetail);
                return View(userModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("/admin");
            }
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult edit(UserModel user_model, List<AssignedRole> roles, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserDto userDto = getUserDtoFromProvidedDatas(user_model, roles);

                    if (file != null)
                    {
                        string fileName = userDto.full_name;
                        userDto.image_path = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _userService.update(userDto);
                    AlertHelper.setMessage(this, "User updated successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            finally
            {
                var roleDatas = _roleRepo.getAll();
                List<AssignedRole> availableRoles = new List<AssignedRole>();
                foreach (var role in roleDatas)
                {
                    availableRoles.Add(new AssignedRole()
                    {
                        role_name = role.name,
                        role_id = role.role_id
                    });
                }
                ViewBag.roles = availableRoles;
            }
            return View();
        }

        [HttpGet]
        [Route("enable/{user_id}")]
        public IActionResult enable(long user_id)
        {
            try
            {
                _userService.enable(user_id);
                AlertHelper.setMessage(this, "User enabled successfully.");
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{user_id}")]
        public IActionResult disable(long user_id)
        {
            try
            {
                _userService.disable(user_id);
                AlertHelper.setMessage(this, "User disabled successfully.");
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("change-password/{authentication_id}")]
        public IActionResult changePassword(long authentication_id)
        {
            try
            {
                var authentication = _authenticationRepo.getById(authentication_id);

                if (!userIsAllowedForAction(authentication))
                {
                    throw new Exception("You do not have permission to perform the specified action.");
                }

                var changePasswordDto = new UpdatePasswordDto();
                changePasswordDto.type_id = authentication.type_id;
                changePasswordDto.type = authentication.type;
                return View(changePasswordDto);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [Route("change-password")]
        public IActionResult changePassword(UpdatePasswordDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _authenticationService.updatePassword(model);
                    AlertHelper.setMessage(this, "Password change successful.", messageType.success);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    AlertHelper.setMessage(this, ex.Message, messageType.error);
                }
            }
            return View(model);
        }

        private UserModel getUserModelFromUserDetail(CMS.User.Entity.User userDetail)
        {
            var userModel = _mapper.Map<UserModel>(userDetail);
            userModel.username = _authenticationRepo.getByType(userDetail.user_id, CMS.User.Enums.UserType.user).username;
            return userModel;
        }

        private bool userIsAllowedForAction(Authentication authentication)
        {
            if (authentication.authentication_id == getLoggedInAuthenticationId())
            {
                return true;
            }

            var rolesOfUser = _userRoleRepo.getByTypeId(authentication.type, authentication.type_id);

            var assignedPermissions = _rolePermissionMapRepo.getQueryable().Where(a => rolesOfUser.Select(b => b.role).Contains(a.role)).Select(a => a.permission_name).ToList();

            if (assignedPermissions.Contains(CMS.Web.Areas.User_management.Constants.Permissions.getUserManagementPermissionName()))
            {
                return true;
            }
            return false;
        }

        private UserIndexViewModel getUserIndexVMFromUserList(List<CMS.User.Entity.User> userDetails)
        {
            UserIndexViewModel VM = new UserIndexViewModel();
            VM.logged_in_authentication_id = getLoggedInAuthenticationId();
            VM.user_details = new List<UserDetailModel>();
            foreach (var user in userDetails)
            {
                var userDetail = _mapper.Map<UserDetailModel>(user);

                var authenticationDetail = _authenticationRepo.getByType(user.user_id, CMS.User.Enums.UserType.user);

                if (authenticationDetail != null)
                {
                    userDetail.username = authenticationDetail.username;
                    userDetail.authentication_id = authenticationDetail.authentication_id;
                }

                userDetail.roles = _userRoleRepo.getByTypeId(CMS.User.Enums.UserType.user, user.user_id).Select(a => a.role.name).ToList();
                VM.user_details.Add(userDetail);

            }
            return VM;
        }

        private UserDto getUserDtoFromProvidedDatas(UserModel user_model, List<AssignedRole> roles)
        {
            UserDto userDto = _mapper.Map<UserDto>(user_model);
            userDto.created_by = getLoggedInUserId();

            userDto.role_ids = roles.Where(a => a.is_checked == true).Select(a => a.role_id).ToList();
            return userDto;
        }
    }
}