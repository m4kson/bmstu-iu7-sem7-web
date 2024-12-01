using ProdMonitor.Domain.Models;

namespace ProdMonitor.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync(UserFilter filter);

        Task<User> GetUserById(Guid id);

        Task<User> UpdateUserRole(Guid userId, UserUpdateRole role);
        
        Task<User> UpdateUser(Guid userId, RegisterModel userData);
    }
}
