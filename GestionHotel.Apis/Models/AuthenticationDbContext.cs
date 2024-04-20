using GestionHotel.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;

namespace DBContext
{
    public interface IAuthenticationDbContext
    {
        public DbSet<User> Users { get; set; }
    }

    public class AuthenticationDbContext : DbContext, IAuthenticationDbContext
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
            : base(options)
        {
            DefaultUserInitializer.Initialize(this);
        }
        public virtual DbSet<User> Users { get; set; }
    }
}