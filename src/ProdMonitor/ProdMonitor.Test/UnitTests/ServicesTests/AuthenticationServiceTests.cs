using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ProdMonitor.Application.Services;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.Domain.Interfaces.Services;
using Serilog;
using Microsoft.Extensions.Options;
using ProdMonitor.Application.Services.Configurations;

namespace ProdMonitor.Test.UnitTests
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ILogger> _mockLogger;
        private readonly AuthenticationService _authService;
        private readonly Mock<IOptions<AuthenticationServiceConfiguration>> _mockConfig;

        public AuthenticationServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mockLogger = new Mock<ILogger>();
            
            _mockConfig = new Mock<IOptions<AuthenticationServiceConfiguration>>();
            _mockConfig.Setup(c => c.Value).Returns(new AuthenticationServiceConfiguration
            {
                MinPasswordLength = 8
            });

            _authService = new AuthenticationService(_userRepositoryMock.Object, _mockLogger.Object, _mockConfig.Object);
        }

        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var email = "test@example.com";
            var password = "Password123";
            byte[] passwordHash;
            byte[] passwordSalt;
            _authService.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User(Guid.NewGuid(), "John", "Doe", "Middle", "Dept", email, passwordHash, passwordSalt, DateOnly.FromDateTime(DateTime.Now), SexType.Male, RoleType.Admin);

            _userRepositoryMock
                .Setup(repo => repo.GetUserByEmailAsync(email))
                .ReturnsAsync(user);

            // Act
            var result = await _authService.LoginAsync(new LoginModel(email, password));

            // Assert
            Assert.Equal(user, result);
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(email), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_WrongPassword_ThrowsWrongPasswordException()
        {
            // Arrange
            var email = "test@example.com";
            var password = "Password123123";
            var wrongPassword = "WrongPassword123";
            var passwordHash = new byte[64]; 
            var passwordSalt = new byte[128];
            var user = new User(Guid.NewGuid(), "John", "Doe", "Middle", "Dept", email, passwordHash, passwordSalt, DateOnly.FromDateTime(DateTime.Now), SexType.Male, RoleType.Admin);

            _userRepositoryMock
                .Setup(repo => repo.GetUserByEmailAsync(email))
                .ReturnsAsync(user);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<WrongPasswordException>(() => _authService.LoginAsync(new LoginModel(email, wrongPassword)));
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(email), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_UserNotFound_ThrowsUserNotFoundException()
        {
            // Arrange
            var email = "nonexistent1@example.com";
            var password = "Password123";

            _userRepositoryMock
                .Setup(repo => repo.GetUserByEmailAsync(email))
                .ReturnsAsync((User)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UserNotFoundException>(() => _authService.LoginAsync(new LoginModel(email, password)));
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(email), Times.Once);
        }

        [Fact]
        public async Task RegisterAsync_NewUser_ReturnsCreatedUser()
        {
            // Arrange
            string email = "newuser@example.com";
            var registerModel = new RegisterModel(
                email: email,
                password: "Password123",
                name: "John",
                surname: "Doe",
                patronymic: "Middle",
                sex: SexType.Male,
                department: "Dept",
                birthDay: DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)));

            var createdUser = new User(Guid.NewGuid(), "John", "Doe", "Middle", "Dept", email, new byte[64], new byte[128], DateOnly.FromDateTime(DateTime.Now), SexType.Male, RoleType.Admin);

            _userRepositoryMock
                .Setup(repo => repo.GetUserByEmailAsync(email))
                .ReturnsAsync((User)null);

            _userRepositoryMock
                .Setup(repo => repo.CreateUserAsync(It.IsAny<UserCreate>()))
                .ReturnsAsync(createdUser);

            // Act
            var result = await _authService.RegisterAsync(registerModel);

            // Assert
            Assert.Equal(createdUser, result);
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(email), Times.Once);
            _userRepositoryMock.Verify(repo => repo.CreateUserAsync(It.IsAny<UserCreate>()), Times.Once);
        }

        [Fact]
        public async Task RegisterAsync_UserAlreadyExists_ThrowsUserAlreadyExistException()
        {
            // Arrange
            var email = "existinguser@example.com";
            var registerModel = new RegisterModel(
                email: email,
                password: "Password123",
                name: "John",
                surname: "Doe",
                patronymic: "Middle",
                sex: SexType.Male,
                department: "Dept",
                birthDay: DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)));

            var existingUser = new User(Guid.NewGuid(), "John", "Doe", "Middle", "Dept", email, new byte[64], new byte[128], DateOnly.FromDateTime(DateTime.Now), SexType.Male, RoleType.Admin);

            _userRepositoryMock
                .Setup(repo => repo.GetUserByEmailAsync(email))
                .ReturnsAsync(existingUser);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UserAlreadyExistException>(() => _authService.RegisterAsync(registerModel));
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(email), Times.Once);
            _userRepositoryMock.Verify(repo => repo.CreateUserAsync(It.IsAny<UserCreate>()), Times.Never);
        }
    }
}
