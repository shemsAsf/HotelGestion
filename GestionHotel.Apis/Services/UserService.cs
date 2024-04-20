using DBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService
    {
        public Task<bool> IsUserInDatabase(string username);
    }

    public class UserService : IUserService
    {
        private readonly IAuthenticationDbContext _dbContext;

        public UserService(IAuthenticationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsUserInDatabase(string username)
        {
            if (username.IsNullOrEmpty())
                return false;

            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);
            return user != null;
        }
    }
}

