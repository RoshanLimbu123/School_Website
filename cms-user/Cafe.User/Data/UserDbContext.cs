using CMS.User.Entity;
using Microsoft.EntityFrameworkCore;

namespace CMS.User.Data
{
    public class UserDbContext:DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        public DbSet<LoginSession> login_sessions { get; set; }
        public DbSet<Authentication> authentications { get; set; }
        public DbSet<RolePermissionMap> role_permission_maps { get; set; }
        public DbSet<Entity.User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<UserRole> user_roles { get; set; }
    }
}
