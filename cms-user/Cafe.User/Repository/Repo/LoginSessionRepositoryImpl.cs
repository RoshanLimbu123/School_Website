using CMS.User.Data;
using CMS.User.Entity;
using CMS.User.Repository.Interface;

namespace CMS.User.Repository.Repo
{
    public class LoginSessionRepositoryImpl : BaseRepositoryImpl<LoginSession>, LoginSessionRepository
    {
        private readonly UserDbContext _userDbContext;

        public LoginSessionRepositoryImpl(UserDbContext userDbContext) : base(userDbContext)
        {
            _userDbContext = userDbContext;
        }
    }
}
