using ProdMonitor.DataAccess.Context;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Models;
using ProdMonitor.DataAccess.Models;
using ProdMonitor.DataAccess.Models.Converters;
using Microsoft.EntityFrameworkCore;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.DataAccess.Models.Enums;
using ProdMonitor.DataAccess.Models.Converters.Enums;

namespace ProdMonitor.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ProdMonitorContext _context;

        public UserRepository(ProdMonitorContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(UserCreate user)
        {
            try
            {
                var newUser = new UserDb(
                    id: Guid.NewGuid(),
                    name: user.Name,
                    surname: user.Surname,
                    patronymic: user.Patronymic,
                    department: user.Department,
                    email: user.Email,
                    passwordHash: user.PasswordHash,
                    passwordSalt: user.PasswordSalt,
                    birthDay: user.BirthDay,
                    sex: SexTypeConverter.ToDb(user.Sex),
                    role: RoleTypeConverter.ToDb(user.Role)
                );

                //var userDb = UserConverter.ToDb(newUser);

                _context.Users.Add(newUser!);
                await _context.SaveChangesAsync();

                return UserConverter.ToDomain(newUser)!;
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("Failed to create user", ex);
            }
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    throw new UserNotFoundException($"User with ID {userId} not found");
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("Failed to delete user", ex);
            }
        }

        public async Task<List<User>> GetAllUsersAsync(UserFilter filter)
        {
            try
            {
                IQueryable<UserDb> query = _context.Users;

                if (!string.IsNullOrEmpty(filter.Department))
                {
                    query = query.Where(u => u.Department == filter.Department);
                }

                if (filter.BirthDay.HasValue)
                {
                    query = query.Where(u => u.BirthDay == filter.BirthDay.Value);
                }

                if (filter.Sex.HasValue)
                {
                    query = query.Where(u => u.Sex == (SexTypeDb)filter.Sex.Value);
                }

                if (filter.Role.HasValue)
                {
                    query = query.Where(u => u.Role == (RoleTypeDb)filter.Role.Value);
                }

                var users = await query
                    .Skip(filter.Skip)
                    .Take(filter.Limit)
                    .AsNoTracking()
                    .ToListAsync();

                return users.ConvertAll(UserConverter.ToDomain)!;
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("Failed to retrieve users", ex);
            }
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            try
            {
                var userDb = await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Email == email);

                return UserConverter.ToDomain(userDb);
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("Failed to retrieve user by email", ex);
            }
        }


        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            try
            {
                var userDb = await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == id);

                return UserConverter.ToDomain(userDb);
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("Failed to retrieve user by ID", ex);
            }
        }

        public async Task<User> UpdateUserRoleAsync(Guid userId, UserUpdateRole roleUpdate)
        {
            try
            {
                var userDb = await _context.Users.FindAsync(userId);
                if (userDb == null)
                {
                    throw new UserNotFoundException($"User with ID {userId} not found");
                }

                userDb.Role = (RoleTypeDb)roleUpdate.Role;
                await _context.SaveChangesAsync();

                return UserConverter.ToDomain(userDb)!;
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("Failed to update user role", ex);
            }
        }
    }
}
