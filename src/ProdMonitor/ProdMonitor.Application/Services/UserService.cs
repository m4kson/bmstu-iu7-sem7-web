using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Interfaces.Repositories;
using Serilog;
using Serilog.Core;

namespace ProdMonitor.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UserService(IUserRepository userRepository, 
            ILogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<List<User>> GetAllUsersAsync(UserFilter filter)
        {
            _logger.Information("Attempting to retrieve all users with the specified filter.");
            try
            {
                var users = await _userRepository.GetAllUsersAsync(filter);
                _logger.Information("Successfully retrieved {UserCount} users.", users.Count);
                return users;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to retrieve users.");
                throw new UserServiceException("Failed to get users", ex);
            }
        }

        public async Task<User> GetUserById(Guid id)
        {
            _logger.Information("Attempting to retrieve user with ID {UserId}.", id);
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    _logger.Warning("User with ID {UserId} not found.", id);
                    throw new UserNotFoundException($"User with id {id} not found");
                }
                _logger.Information("Successfully retrieved user with ID {UserId}.", id);
                return user;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to retrieve user with ID {UserId}.", id);
                throw new UserServiceException("Failed to get user", ex);
            }
        }

        public async Task<User> UpdateUserRole(Guid userId, UserUpdateRole role)
        {
            _logger.Information("Attempting to update role for user with ID {UserId} to {Role}.", userId, role.Role);
            try
            {
                var updatedUser = await _userRepository.UpdateUserRoleAsync(userId, role);
                _logger.Information("Successfully updated role for user with ID {UserId} to {Role}.", userId, role.Role);
                return updatedUser;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to update role for user with ID {UserId}.", userId);
                throw new UserServiceException("Failed to update users status", ex);
            }
        }
    }
}
