using CMS.User.Dto;
using CMS.User.Entity;
using CMS.User.Enums;
using CMS.User.Exceptions;
using CMS.User.Library;
using CMS.User.Makers.Interface;
using CMS.User.Repository.Interface;
using CMS.User.Service.Interface;
using System;
using System.Transactions;

namespace CMS.User.Service.Implementations
{
    public class AuthenticationServiceImpl : AuthenticationService
    {
        private readonly AuthenticationRepository _authenticationRepo;
        private readonly AuthenticationMaker _authenticationMaker;
        private readonly PasswordHash _passwordHash;
        private readonly Helper.TransactionManager _transactionManager;

        public AuthenticationServiceImpl(AuthenticationRepository authenticationRepo, EncryptDecrypt encryptDecrypt, AuthenticationMaker authenticationMaker, PasswordHash passwordHash, Helper.TransactionManager transactionManager)
        {
            _passwordHash = passwordHash;
            _authenticationRepo = authenticationRepo;
            _authenticationMaker = authenticationMaker;
            _transactionManager = transactionManager;
        }

        public void disable(long type_id, UserType type = UserType.user)
        {
            try
            {
                _transactionManager.beginTransaction();

                var authentication = _authenticationRepo.getByType(type_id, type) ?? throw new ItemNotFoundException($"Authentication detail doesnot exist.");

                authentication.deactivate();
                _authenticationRepo.update(authentication);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void enable(long type_id, UserType type = UserType.user)
        {
            try
            {
                _transactionManager.beginTransaction();
                var authentication = _authenticationRepo.getByType(type_id, type) ?? throw new ItemNotFoundException($"Authentication detail doesnot exist.");

                authentication.activate();
                _authenticationRepo.update(authentication);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void save(AuthenticationDto authentication_dto)
        {
            try
            {
                _transactionManager.beginTransaction();
                var authentication = _authenticationRepo.getByType(authentication_dto.type_id, authentication_dto.type);

                if (authentication != null)
                {
                    throw new DuplicateItemException("Authentication for specified person already exists.");
                }

                var authenticationWithSameName = _authenticationRepo.getByUsername(authentication_dto.username);

                if (authenticationWithSameName != null)
                {
                    throw new DuplicateItemException("Authentication with same username already exists.");
                }

                authentication = new Authentication();

                _authenticationMaker.copy(authentication, authentication_dto);

                authentication.password = _passwordHash.CreateHash(authentication_dto.password);

                _authenticationRepo.insert(authentication);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void updatePassword(UpdatePasswordDto dto)
        {
            try
            {
                _transactionManager.beginTransaction();
                var authentication = _authenticationRepo.getByType(dto.type_id, dto.type);

                bool isOldPasswordCorrect = _passwordHash.ValidatePassword(dto.old_password, authentication.password);

                if (!isOldPasswordCorrect)
                {
                    throw new InvalidValueException($"Old password is incorrect.");
                }

                authentication.password = _passwordHash.CreateHash(dto.new_password);

                _authenticationRepo.update(authentication);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }

            //    tx.Complete();
            //}
        }

        public void updateUsername(string new_name, long type_id, UserType type = UserType.user)
        {
            try
            {
                _transactionManager.beginTransaction();
                var authentication = _authenticationRepo.getByType(type_id, type) ?? throw new ItemNotFoundException($"Authentication detail doesnot exist.");

                var authenticationWithSameName = _authenticationRepo.getByUsername(new_name);

                bool isUsernameAllowed = authenticationWithSameName == null || authenticationWithSameName.authentication_id == authentication.authentication_id;

                if (!isUsernameAllowed)
                {
                    throw new DuplicateItemException("Authentication with same username already exists.");
                }

                authentication.username = new_name;
                _authenticationRepo.update(authentication);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }

        }

        public Authentication validateUser(string username, string password)
        {
            var authentication = _authenticationRepo.getByUsername(username);
            if (authentication == null)
            {
                return null;
            }
            if (!_passwordHash.ValidatePassword(password, authentication.password))
            {
                return null;
            }

            return authentication;
        }
    }
}
