using CMS.User.Dto;
using CMS.User.Entity;
using CMS.User.Exceptions;
using CMS.User.Helper;
using CMS.User.Repository.Interface;
using CMS.User.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.User.Service.Implementations
{
    public class RolePermissionMapServiceImpl : RolePermissionMapService
    {
        private readonly RolePermissionMapRepository _rolePermissionMapRepo;
        private readonly RoleRepository _roleRepo;
        private readonly TransactionManager _transactionManager;

        public RolePermissionMapServiceImpl(RolePermissionMapRepository rolePermissionMapRepo, RoleRepository roleRepo, TransactionManager transactionManager)
        {
            _rolePermissionMapRepo = rolePermissionMapRepo;
            _roleRepo = roleRepo;
            _transactionManager = transactionManager;
        }

        public void deletePermissionsByRoleId(long role_id)
        {
            var permissions = _rolePermissionMapRepo.getByRoleId(role_id);
            foreach (var permission in permissions)
            {
                _rolePermissionMapRepo.delete(permission);
            }
        }

        public void saveOrUpdate(RolePermissionMapDto dto)
        {
            try
            {
                _transactionManager.beginTransaction();
                if (dto.permissions.Count == 0)
                {
                    throw new InvalidValueException("At least one permission is required.");
                }

                var previousSavePermissions = _rolePermissionMapRepo.getByRoleId(dto.role_id);
                if (previousSavePermissions.Count == 0)
                {
                    save(dto);
                }
                else
                {
                    update(dto);
                }
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        protected void save(RolePermissionMapDto dto)
        {
            if (dto.permissions.Count == 0)
            {
                throw new InvalidValueException("At least one permission is required.");
            }

            var role = _roleRepo.getById(dto.role_id) ?? throw new ItemNotFoundException($"User role with the id {dto.role_id} doesnot exist.");

            foreach (var permission in dto.permissions)
            {
                RolePermissionMap rolePermissionMap = new RolePermissionMap()
                {
                    role = role,
                    permission_name = permission
                };
                _rolePermissionMapRepo.insert(rolePermissionMap);
            }
        }

        protected void update(RolePermissionMapDto dto)
        {
            var previousSavePermissions = _rolePermissionMapRepo.getByRoleId(dto.role_id) ?? throw new ItemNotFoundException($"Permissions has not been assigned to specified role.");

            List<string> previouslyAssignedPermissions = previousSavePermissions.Select(a => a.permission_name).ToList();

            var userRole = _roleRepo.getById(dto.role_id) ?? throw new ItemNotFoundException($"User role with the id {dto.role_id} doesnot exist.");

            var removedPermissions = previousSavePermissions.Where(l1 => !dto.permissions.Any(permission => l1.permission_name == permission)).ToList();

            foreach (var rolePermissionMap in removedPermissions)
            {
                _rolePermissionMapRepo.delete(rolePermissionMap);
            }

            var addedPermissions = dto.permissions.Where(l1 => !previousSavePermissions.Any(permission => l1 == permission.permission_name)).ToList();

            dto.removeAllPermissions();

            foreach (var addedPermission in addedPermissions)
            {
                dto.addPermission(addedPermission);
            }

            if (dto.permissions.Count > 0)
            {
                save(dto);
            }
        }
    }
}
