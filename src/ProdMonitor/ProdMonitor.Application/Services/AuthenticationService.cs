using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.Domain.Exceptions;
using Serilog;
using ProdMonitor.Application.Services.Configurations;
using Microsoft.Extensions.Options;

namespace ProdMonitor.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly IEmailService _emailService;
        private readonly AuthenticationServiceConfiguration _authenticationServiceConfiguration;

        public AuthenticationService(IUserRepository userRepository,
            ILogger logger,
            IEmailService emailService,
            IOptions<AuthenticationServiceConfiguration> authenticationServiceConfiguration)
        {
            _userRepository = userRepository;
            _logger = logger;
            _emailService = emailService;
            _authenticationServiceConfiguration = authenticationServiceConfiguration.Value;
        }

        public async Task<User> LoginAsync(LoginModel authModel)
        {
            try
            {
                _logger.Information("Attempt to login with email: {Email}", authModel.Email);
                var user = await _userRepository.GetUserByEmailAsync(authModel.Email);
                if (user == null)
                {
                    _logger.Warning("Login failed: User with email {Email} not found", authModel.Email);
                    throw new UserNotFoundException($"User with Email {authModel.Email} not found");
                }

                if (!VerifyPasswordHash(authModel.Password, user.PasswordHash, user.PasswordSalt))
                {
                    _logger.Warning("Login failed: Incorrect password for email {Email}", authModel.Email);
                    throw new WrongPasswordException("Wrong password");
                }
                
                _logger.Information("User {Email} successfully logged in", authModel.Email);
                return user;
            }
            catch (UserNotFoundException)
            {
                throw;
            }
            catch (WrongPasswordException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Login failed for email {Email}", authModel.Email);
                throw new LoginException("Failed to login", ex);
            }
        }

        public async Task<User> RegisterAsync(RegisterModel authModel)
        {
            try
            {
                _logger.Information("Attempt to register new user with email: {Email}", authModel.Email);
                if (authModel.Password.Length < _authenticationServiceConfiguration.MinPasswordLength)
                {
                    _logger.Error(
                        "Registration failed: Please ensure your password are longer than {MinPasswordLength}",
                        _authenticationServiceConfiguration.MinPasswordLength);
                    throw new ArgumentException(
                        $"Please ensure your password are longer than {_authenticationServiceConfiguration.MinPasswordLength}");
                }

                var existingUser = await _userRepository.GetUserByEmailAsync(authModel.Email);
                if (existingUser != null)
                {
                    _logger.Warning("Registration failed: User with email {Email} already exists", authModel.Email);
                    throw new UserAlreadyExistException($"User with Email {authModel.Email} already exist");
                }

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(authModel.Password, out passwordHash, out passwordSalt);

                var newUser = new UserCreate(
                    name: authModel.Name,
                    surname: authModel.Surname,
                    patronymic: authModel.Patronymic,
                    department: authModel.Department,
                    email: authModel.Email,
                    passwordHash: passwordHash,
                    passwordSalt: passwordSalt,
                    birthDay: authModel.BirthDay,
                    sex: authModel.Sex,
                    role: RoleType.Verification);

                var createdUser = await _userRepository.CreateUserAsync(newUser);
                _logger.Information("User with email {Email} successfully registered", authModel.Email);
                return createdUser;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (UserAlreadyExistException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Registration failed for email {Email}", authModel.Email);
                throw new RegisterException("Failed to register", ex);
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

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }

        public void EnsureUserHasRole(User user, RoleType requiredRole)
        {
            if (user.Role != requiredRole)
            {
                _logger.Warning(
                    "Unauthorized access attempt by user {Email}. Required role: {RequiredRole}, actual role: {ActualRole}",
                    user.Email, requiredRole, user.Role);
                throw new UnauthorizedAccessException($"User does not have the required role: {requiredRole}");
            }
        }

        private string GenerateTwoFactorCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public async Task SendTwoFactorCode(User user)
        {
            try
            {
                var code = GenerateTwoFactorCode();

                var updatedUser = new UserCreate(name: user.Name,
                    surname: user.Surname,
                    patronymic: user.Patronymic,
                    department: user.Department,
                    email: user.Email,
                    passwordHash: user.PasswordHash,
                    passwordSalt: user.PasswordSalt,
                    birthDay: user.BirthDay,
                    sex: user.Sex,
                    role: user.Role,
                    twoFactorCode: code,
                    twoFactorExpiration: DateTime.UtcNow.AddMinutes(10));

                await _userRepository.UpdateUserAsync(user.Id, updatedUser);
                
                await _emailService.SendEmailAsync(user.Email, "Ваш код", $"Ваш код 2FA: {code}");

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to send 2FA code to user with ID {UserId}.", user.Id);
                throw new UserServiceException("Failed to send 2FA code", ex);
            }
        }

        public async Task<bool> VerifyTwoFactorCodeAsync(Guid userId, string code)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    _logger.Warning("User with ID {UserId} not found.", userId);
                    throw new UserNotFoundException($"User with id {userId} not found");
                }

                if (user.TwoFactorCode != code || user.TwoFactorExpiration < DateTime.UtcNow)
                {
                    return false;
                }

                var updatedUser = new UserCreate(name: user.Name,
                    surname: user.Surname,
                    patronymic: user.Patronymic,
                    department: user.Department,
                    email: user.Email,
                    passwordHash: user.PasswordHash,
                    passwordSalt: user.PasswordSalt,
                    birthDay: user.BirthDay,
                    sex: user.Sex,
                    role: user.Role,
                    twoFactorCode: null,
                    twoFactorExpiration: null);

                await _userRepository.UpdateUserAsync(user.Id, updatedUser);

                return true;
            }
            catch (UserNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to verify 2FA code for user with ID {UserId}.", userId);
                throw new UserServiceException("Failed to verify 2FA code", ex);
            }
        }
    }
}
