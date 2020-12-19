using CMS.User.Dto;
using CMS.User.Exceptions;
using CMS.User.Makers.Interface;
using CMS.User.Repository.Interface;
using CMS.User.Service.Interface;
using System;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using CMS.User.Helper;

namespace CMS.User.Service.Implementations
{
    using userEntity = Entity.User;
    public class UserServiceImpl : UserService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserRepository _userRepo;
        private readonly RoleRepository _userRoleRepo;
        private readonly AuthenticationRepository _authenticationRepo;
        private readonly AuthenticationService _authenticationService;
        private readonly UserMaker _userMaker;
        private readonly UserRoleService _userRoleService;
        private readonly TransactionManager _transactionManager;


        public UserServiceImpl(UserRepository userRepo, RoleRepository userRoleRepo, AuthenticationRepository authenticationRepo, AuthenticationService authenticationService, UserMaker userMaker, UserRoleService userRoleService, IHostingEnvironment hostingEnvironment, TransactionManager transactionManager)
        {
            _userRepo = userRepo;
            _userRoleRepo = userRoleRepo;
            _authenticationRepo = authenticationRepo;
            _authenticationService = authenticationService;
            _userMaker = userMaker;
            _userRoleService = userRoleService;
            _hostingEnvironment = hostingEnvironment;
            _transactionManager = transactionManager;
        }

        public void disable(long user_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var user = _userRepo.getById(user_id) ?? throw new ItemNotFoundException($"User with the id {user_id} doesnot exist.");

                user.disable();
                _userRepo.update(user);

                _authenticationService.disable(user_id, Enums.UserType.user);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void enable(long user_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var user = _userRepo.getById(user_id) ?? throw new ItemNotFoundException($"User with the id {user_id} doesnot exist.");

                user.enable();
                _userRepo.update(user);

                _authenticationService.enable(user_id, Enums.UserType.user);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void save(UserDto user_dto)
        {
            try
            {
                _transactionManager.beginTransaction();

                if (user_dto.role_ids.Count == 0)
                {
                    throw new InvalidValueException("At least one role must be specified.");
                }

                bool isUsernameValid = checkNameValidity(user_dto);
               

                if (!isUsernameValid)
                {
                    throw new DuplicateItemException("User with same name already exists.");
                }

                userEntity user = new userEntity();
                _userMaker.copy(user, user_dto);
                user.created_date = DateTime.Now;
                _userRepo.insert(user);

                UserRoleDto userRoleDto = new UserRoleDto()
                {
                    type = Enums.UserType.user,
                    type_id = user.user_id,
                    role_ids = user_dto.role_ids.ToArray()
                };

                _userRoleService.save(userRoleDto);

                AuthenticationDto authenticationDto = new AuthenticationDto()
                {
                    username = user_dto.username,
                    password = user_dto.password,
                    type = Enums.UserType.user,
                    type_id = user.user_id
                };

                _authenticationService.save(authenticationDto);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void update(UserDto user_dto)
        {
            try
            {
                _transactionManager.beginTransaction();
                if (user_dto.role_ids.Count == 0)
                {
                    throw new InvalidValueException("At least one role must be specified.");
                }

                userEntity user = _userRepo.getById(user_dto.user_id) ?? throw new ItemNotFoundException($"User with the id {user_dto.user_id} doesnot exist.");

                bool isUsernameValid = checkNameValidity(user_dto);

                if (!isUsernameValid)
                {
                    throw new DuplicateItemException("User with same name already exists.");
                }

                if (!string.IsNullOrWhiteSpace(user_dto.image_path))
                {
                    if (!string.IsNullOrWhiteSpace(user.image_path))
                    {
                        deleteImage(user.image_path);
                    }
                }

                _userMaker.copy(user, user_dto);
                _userRepo.update(user);
                _userRoleService.update(new UserRoleDto()
                {
                    type = Enums.UserType.user,
                    type_id = user.user_id,
                    role_ids = user_dto.role_ids.ToArray()
                });

                _authenticationService.updateUsername(user_dto.username, user_dto.user_id);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        protected void deleteImage(string image_path)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/custom");
            if (File.Exists(Path.Combine(filePath, image_path)))
            {
                File.Delete(Path.Combine(filePath, image_path));
            }
        }

        protected bool checkNameValidity(UserDto user_dto)
        {
            var userWithSameUsername = _authenticationRepo.getByUsername(user_dto.username);

            if (userWithSameUsername == null || userWithSameUsername.type_id == user_dto.user_id)
            {
                return true;
            }
            return false;
        }
    }
}
