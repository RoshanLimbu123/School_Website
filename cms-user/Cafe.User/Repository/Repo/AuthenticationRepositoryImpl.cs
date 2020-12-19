using CMS.User.Data;
using CMS.User.Entity;
using CMS.User.Enums;
using CMS.User.Repository.Interface;
using System.Linq;

namespace CMS.User.Repository.Repo
{
    public class AuthenticationRepositoryImpl : BaseRepositoryImpl<Authentication>, AuthenticationRepository
    {
        private readonly UserDbContext _userDbContext;

        public AuthenticationRepositoryImpl(UserDbContext userDbContext) : base(userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public Authentication getByType(long type_id, UserType type)
        {
            return _userDbContext.authentications.Where(a => a.type == type && a.type_id == type_id).SingleOrDefault();
        }

        public Authentication getByUsername(string username)
        {
            return _userDbContext.authentications.Where(a => a.username == username).SingleOrDefault();
        }
    }
}
