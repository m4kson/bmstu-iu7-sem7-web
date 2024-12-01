using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Domain.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<User> LoginAsync(LoginModel authModel);

        Task<User> RegisterAsync(RegisterModel authModel);

        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);

        void EnsureUserHasRole(User user, RoleType requiredRole);
    }
}
