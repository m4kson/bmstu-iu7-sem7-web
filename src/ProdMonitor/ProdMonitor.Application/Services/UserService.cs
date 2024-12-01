using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Interfaces.Repositories;
using Serilog;
using ProdMonitor.Application.Services;
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
                if (!users.Any())
                {
                    _logger.Warning("No users found with the specified filter.");
                    throw new UserNotFoundException("No users found with the specified filter.");
                }
                _logger.Information("Successfully retrieved {UserCount} users.", users.Count);
                return users;
            }
            catch (UserNotFoundException)
            {
                throw;
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
            catch (UserNotFoundException)
            {
                throw;
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
            catch (UserNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to update role for user with ID {UserId}.", userId);
                throw new UserServiceException("Failed to update users status", ex);
            }
        }

        public async Task<User> UpdateUser(Guid userId, RegisterModel userData)
        {
            try
            {
                
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    _logger.Warning("User with ID {UserId} not found.", userId);
                    throw new UserNotFoundException($"User with id {userId} not found");
                }
                
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(userData.Password, out passwordHash, out passwordSalt);

                var newUser = new UserCreate(
                    name: userData.Name,
                    surname: userData.Surname,
                    patronymic: userData.Patronymic,
                    department: userData.Department,
                    email: userData.Email,
                    passwordHash: passwordHash,
                    passwordSalt: passwordSalt,
                    birthDay: userData.BirthDay,
                    sex: userData.Sex,
                    role: user.Role);
                
                var updatedUser = await _userRepository.UpdateUserAsync(userId, newUser);
                return updatedUser;
            }
            catch (UserNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to update user with ID {UserId}.", userId);
                throw new UserServiceException("Failed to update user data", ex);
            }
        }
        
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
