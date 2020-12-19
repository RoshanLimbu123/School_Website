using CMS.User.Dto;
using CMS.User.Entity;
using CMS.User.Exceptions;
using CMS.User.Makers.Interface;
using CMS.User.Repository.Interface;
using CMS.User.Service.Interface;

namespace CMS.User.Service.Implementations
{
    public class LoginSessionServiceImpl : LoginSessionService
    {
        private readonly LoginSessionMaker _loginSessionMaker;
        private readonly LoginSessionRepository _loginSessionRepo;
        private readonly AuthenticationRepository _authenticationRepo;
        private readonly Helper.TransactionManager _transactionManager;

        public LoginSessionServiceImpl(LoginSessionMaker loginSessionMaker, LoginSessionRepository loginSessionRepo, AuthenticationRepository authenticationRepo, Helper.TransactionManager transactionManager)
        {
            _loginSessionMaker = loginSessionMaker;
            _loginSessionRepo = loginSessionRepo;
            _authenticationRepo = authenticationRepo;
            _transactionManager = transactionManager;
        }

        public void save(LoginSessionDto session_dto)
        {
            try
            {
                _transactionManager.beginTransaction();
                var authentication = _authenticationRepo.getById(session_dto.authentication_id);
                LoginSession sessionDetail = new LoginSession();
                _loginSessionMaker.copy(sessionDetail, session_dto);

                sessionDetail.authentication = authentication ?? throw new ItemNotFoundException($"Authentication with the id {session_dto.authentication_id} doesnot exist.");

                _loginSessionRepo.insert(sessionDetail);
                _transactionManager.commitTransaction();
            }
            catch (System.Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }

        }
    }
}
