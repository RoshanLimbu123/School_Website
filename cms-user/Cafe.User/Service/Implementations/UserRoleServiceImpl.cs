using CMS.User.Dto;
using CMS.User.Entity;
using CMS.User.Exceptions;
using CMS.User.Repository.Interface;
using CMS.User.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.User.Service.Implementations
{
    public class UserRoleServiceImpl : UserRoleService
    {
        private readonly UserRoleRepository _userRoleRepo;
        private readonly RoleRepository _roleRepo;
        private readonly Helper.TransactionManager _transactionManager;

        public UserRoleServiceImpl(UserRoleRepository userRoleRepo, RoleRepository roleRepo, Helper.TransactionManager transactionManager)
        {
            _userRoleRepo = userRoleRepo;
            _roleRepo = roleRepo;
            _transactionManager = transactionManager;
        }

        public void save(UserRoleDto dto)
        {
            try
            {
                _transactionManager.beginTransaction();
                //var previousAssignedRoles = _userRoleRepo.getByTypeId(dto.type, dto.type_id);

                //bool isRoleAlreadyAssignedToUser = previousAssignedRoles.Count > 0;

                ////if (isRoleAlreadyAssignedToUser)
                ////{
                //throw new InvalidValueException("Roles have already been assigned to the user.");
                //// }

                foreach (var roleId in dto.role_ids.Distinct())
                {
                    UserRole user_role = new UserRole();
                    user_role.role_id = roleId;
                    user_role.role = _roleRepo.getById(roleId) ?? throw new ItemNotFoundException($"Role with the role id {roleId} doesnot exist.");

                    user_role.type = dto.type;
                    user_role.type_id = dto.type_id;
                    _userRoleRepo.insert(user_role);
                    _transactionManager.commitTransaction();
                }
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void update(UserRoleDto dto)
        {
            try
            {
                _transactionManager.beginTransaction();
                var previousAssignedRoles = _userRoleRepo.getByTypeId(dto.type, dto.type_id);

                var removedUserRoles = previousAssignedRoles.Where(l1 => !dto.role_ids.Any(role_id => l1.role_id == role_id)).ToList();

                foreach (var removedUserRole in removedUserRoles)
                {
                    _userRoleRepo.delete(removedUserRole);
                }

                var addedRoleIds = dto.role_ids.Where(l1 => !previousAssignedRoles.Any(user_roles => l1 == user_roles.role_id)).ToList();

                foreach (var roleId in addedRoleIds.Distinct())
                {
                    UserRole user_role = new UserRole();
                    user_role.role_id = roleId;
                    user_role.role = _roleRepo.getById(roleId) ?? throw new ItemNotFoundException($"Role with the role id {roleId} doesnot exist.");

                    user_role.type = dto.type;
                    user_role.type_id = dto.type_id;
                    _userRoleRepo.insert(user_role);
                }
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }
    }
}
