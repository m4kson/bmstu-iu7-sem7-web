using ProdMonitor.Domain.Models;

namespace ProdMonitor.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(UserCreate user);

        Task<List<User>> GetAllUsersAsync(UserFilter filter);

        Task<User?> GetUserByIdAsync(Guid id);

        Task<User?> GetUserByEmailAsync(string email);

        Task<User> UpdateUserRoleAsync(Guid userId, UserUpdateRole role);

        Task DeleteUserAsync(Guid userId);
        
        Task<User> UpdateUserAsync(Guid userId, UserCreate userData);
    }
}
