using System;
using System.Collections.Generic;
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

namespace ProdMonitor.Test.UnitTests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ILogger> _mockLogger;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mockLogger = new Mock<ILogger>();
            _userService = new UserService(_userRepositoryMock.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllUsersAsync_ValidFilter_ReturnsListOfUsers()
        {
            // Arrange
            var filter = new UserFilter(null, null, null, null);
            var users = new List<User>
            {
                new User(Guid.NewGuid(), "John", "Doe", "M.", "IT", "john.doe@example.com", new byte[0], new byte[0], new DateOnly(1990, 1, 1), SexType.Male, RoleType.Specialist),
                new User(Guid.NewGuid(), "Jane", "Doe", "M.", "HR", "jane.doe@example.com", new byte[0], new byte[0], new DateOnly(1985, 5, 10), SexType.Female, RoleType.Admin)
            };

            _userRepositoryMock
                .Setup(repo => repo.GetAllUsersAsync(filter))
                .ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllUsersAsync(filter);

            // Assert
            Assert.Equal(users, result);
            _userRepositoryMock.Verify(repo => repo.GetAllUsersAsync(filter), Times.Once);
        }

        [Fact]
        public async Task GetAllUsersAsync_ThrowsException_ThrowsUserServiceException()
        {
            // Arrange
            var filter = new UserFilter(null, null, null, null);

            _userRepositoryMock
                .Setup(repo => repo.GetAllUsersAsync(filter))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UserServiceException>(() => _userService.GetAllUsersAsync(filter));
            Assert.Equal("Failed to get users", exception.Message);
            _userRepositoryMock.Verify(repo => repo.GetAllUsersAsync(filter), Times.Once);
        }

        [Fact]
        public async Task GetUserById_ValidId_ReturnsUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User(userId, "John", "Doe", "M.", "IT", "john.doe@example.com", new byte[0], new byte[0], new DateOnly(1990, 1, 1), SexType.Male, RoleType.Operator);

            _userRepositoryMock
                .Setup(repo => repo.GetUserByIdAsync(userId))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserById(userId);

            // Assert
            Assert.Equal(user, result);
            _userRepositoryMock.Verify(repo => repo.GetUserByIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetUserById_UserNotFound_ThrowsUserNotFoundException()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _userRepositoryMock
                .Setup(repo => repo.GetUserByIdAsync(userId))
                .ReturnsAsync((User)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UserNotFoundException>(() => _userService.GetUserById(userId));
            _userRepositoryMock.Verify(repo => repo.GetUserByIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetUserById_ThrowsException_ThrowsUserServiceException()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _userRepositoryMock
                .Setup(repo => repo.GetUserByIdAsync(userId))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UserServiceException>(() => _userService.GetUserById(userId));
            Assert.Equal("Failed to get user", exception.Message);
            _userRepositoryMock.Verify(repo => repo.GetUserByIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task UpdateUserRole_ValidRole_UpdatesUserRole()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var updatedUser = new User(userId, "John", "Doe", "M.", "IT", "john.doe@example.com", new byte[0], new byte[0], new DateOnly(1990, 1, 1), SexType.Male, RoleType.Admin);
            var updateRole = new UserUpdateRole(RoleType.Admin);

            _userRepositoryMock
                .Setup(repo => repo.UpdateUserRoleAsync(userId, updateRole))
                .ReturnsAsync(updatedUser);

            // Act
            var result = await _userService.UpdateUserRole(userId, updateRole);

            // Assert
            Assert.Equal(updatedUser, result);
            _userRepositoryMock.Verify(repo => repo.UpdateUserRoleAsync(userId, updateRole), Times.Once);
        }

        [Fact]
        public async Task UpdateUserRole_ThrowsException_ThrowsUserServiceException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var updateRole = new UserUpdateRole(RoleType.Admin);

            _userRepositoryMock
                .Setup(repo => repo.UpdateUserRoleAsync(userId, updateRole))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UserServiceException>(() => _userService.UpdateUserRole(userId, updateRole));
            Assert.Equal("Failed to update users status", exception.Message);
            _userRepositoryMock.Verify(repo => repo.UpdateUserRoleAsync(userId, updateRole), Times.Once);
        }
    }
}
