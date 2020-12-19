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
    public class RoleServiceImpl : RoleService
    {
        private readonly RoleRepository _roleRepo;
        private readonly RolePermissionMapService _rolePermissionMapService;
        private readonly UserRoleRepository _userRoleRepo;
        private readonly Helper.TransactionManager _transactionManager;

        public RoleServiceImpl(RoleRepository roleRepo, RolePermissionMapService rolePermissionMapService, UserRoleRepository userRoleRepo, Helper.TransactionManager transactionManager)
        {
            _roleRepo = roleRepo;
            _rolePermissionMapService = rolePermissionMapService;
            _userRoleRepo = userRoleRepo;
            _transactionManager = transactionManager;

        }

        public void delete(long role_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var role = _roleRepo.getById(role_id);

                if (role == null)
                {
                    throw new ItemNotFoundException($"User Role with the id {role_id} doesnot exist.");
                }

                var usersWithSpecifiedRole = _userRoleRepo.getByRoleId(role_id);

                bool usersWithRoleExists = usersWithSpecifiedRole.Count > 0;
                if (usersWithRoleExists)
                {
                    throw new ItemUsedException("The specified role has already been assigned to some users.");
                }
                _roleRepo.delete(role);
                _rolePermissionMapService.deletePermissionsByRoleId(role_id);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void disable(long role_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var role = _roleRepo.getById(role_id);

                if (role == null)
                {
                    throw new ItemNotFoundException($"User Role with the id {role_id} doesnot exist.");
                }
                role.disable();
                _roleRepo.update(role);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void enable(long role_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var role = _roleRepo.getById(role_id);

                if (role == null)
                {
                    throw new ItemNotFoundException($"User Role with the id {role_id} doesnot exist.");
                }
                role.enable();

                _roleRepo.update(role);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }

        }

        public void save(RoleDto role_dto)
        {
            try
            {
                _transactionManager.beginTransaction();
                var roleWithSameName = _roleRepo.getByName(role_dto.name.Trim());

                if (roleWithSameName != null)
                {
                    throw new DuplicateItemException($"User Role with same name already exists.");
                }

                Role role = new Role()
                {
                    name = role_dto.name,
                    is_enabled = role_dto.is_active
                };
                _roleRepo.insert(role);

                var rolePermissionDto = new RolePermissionMapDto();
                rolePermissionDto.role_id = role.role_id;
                foreach (var permission in role_dto.permissions)
                {
                    rolePermissionDto.addPermission(permission);
                }
                _rolePermissionMapService.saveOrUpdate(rolePermissionDto);

                _transactionManager.commitTransaction();

            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void update(RoleDto role_dto)
        {
            try
            {
                _transactionManager.beginTransaction();
                var roleWithSameName = _roleRepo.getByName(role_dto.name.Trim());

                if (roleWithSameName != null && roleWithSameName.role_id != role_dto.role_id)
                {
                    throw new DuplicateItemException($"User Role with same name already exists.");
                }

                roleWithSameName.is_enabled = role_dto.is_active;
                roleWithSameName.name = role_dto.name;
                _roleRepo.update(roleWithSameName);

                updateRolePermissions(role_dto);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
            
        }

        private void updateRolePermissions(RoleDto role_dto)
        {
            RolePermissionMapDto rolePermissionMapDto = new RolePermissionMapDto();
            rolePermissionMapDto.role_id = role_dto.role_id;
            foreach (var permission in role_dto.permissions.Distinct())
            {
                rolePermissionMapDto.addPermission(permission);
            }
            _rolePermissionMapService.saveOrUpdate(rolePermissionMapDto);
        }
    }
}
