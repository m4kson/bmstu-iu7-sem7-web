using ProdMonitor.Domain.Models;
using ProdMonitor.Application.Services;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Test.UnitTests.RepositoryTest;

public class UserRepositoryTests : IClassFixture<RepositoryTestsSetup>
{
    private readonly RepositoryTestsSetup _setup;
    
    public UserRepositoryTests(RepositoryTestsSetup setup)
    {
        _setup = setup;
    }

    [Fact]
    public async Task CreateUserAsync_ShouldCreateUser()
    {
        _setup.ResetContext();

        // Arrange

        var hmac = new System.Security.Cryptography.HMACSHA512();
        byte[] passwordSalt = hmac.Key;
        byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("password"));

        var user = new UserCreate(
            "John",
            "Doe",
            "Doevich",
            "testing",
            "test@mail.ru",
            passwordHash,
            passwordSalt,
            new DateOnly(1990, 1, 1),
            SexType.Female,
            RoleType.Admin);
        
        // Act
        var newUser = await _setup.UserRepository.CreateUserAsync(user);
        
        // Assert
        var result = await _setup.Context.Users.FindAsync(newUser.Id);
        Assert.NotNull(result);
        Assert.Equal(newUser.Id, result.Id);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldThrowException_WhenNameIsNull()
    {
        _setup.ResetContext();

        // Arrange
        var hmac = new System.Security.Cryptography.HMACSHA512();
        byte[] passwordSalt = hmac.Key;
        byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("password"));

        var user = new UserCreate(
            null,
            "Doe",
            "Doevich",
            "testing",
            "test@mail.ru",
            passwordHash,
            passwordSalt,
            new DateOnly(1990, 1, 1),
            SexType.Female,
            RoleType.Admin);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<UserRepositoryException>(() => _setup.UserRepository.CreateUserAsync(user));
    }

    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnAllUsers()
    {
        _setup.ResetContext();

        // Arrange
        var hmac = new System.Security.Cryptography.HMACSHA512();
        byte[] passwordSalt = hmac.Key;
        byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("password"));

        var user1 = new UserCreate(
            "John",
            "Doe",
            "Doevich",
            "testing",
            "test1@mail.ru",
            passwordHash,
            passwordSalt,
            new DateOnly(1990, 1, 1),
            SexType.Female,
            RoleType.Admin);

        var user2 = new UserCreate(
            "Peter",
            "Doe",
            "Doevich",
            "testing",
            "test2@mail.ru",
            passwordHash,
            passwordSalt,
            new DateOnly(1990, 1, 1),
            SexType.Female,
            RoleType.Admin);

        var user3 = new UserCreate(
            "Frank",
            "Doe",
            "Doevich",
            "testing",
            "test3@mail.ru",
            passwordHash,
            passwordSalt,
            new DateOnly(1990, 1, 1),
            SexType.Female,
            RoleType.Admin);


        await _setup.UserRepository.CreateUserAsync(user1);
        await _setup.UserRepository.CreateUserAsync(user2);
        await _setup.UserRepository.CreateUserAsync(user3);

        // Act
        var result = await _setup.UserRepository.GetAllUsersAsync(new UserFilter());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }
    
    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnEmptyList_WhenNoUsers()
    {
        _setup.ResetContext();

        // Arrange

        // Act
        var result = await _setup.UserRepository.GetAllUsersAsync(new UserFilter());

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnUser()
    {
        _setup.ResetContext();

        // Arrange
        var hmac = new System.Security.Cryptography.HMACSHA512();
        byte[] passwordSalt = hmac.Key;
        byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("password"));

        var user1 = new UserCreate(
            "John",
            "Doe",
            "Doevich",
            "testing",
            "test1@mail.ru",
            passwordHash,
            passwordSalt,
            new DateOnly(1990, 1, 1),
            SexType.Female,
            RoleType.Admin);

        var newUser = await _setup.UserRepository.CreateUserAsync(user1);

        // Act

        var result = await _setup.UserRepository.GetUserByIdAsync(newUser.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newUser.Id, result!.Id);
    }
    
    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserNotFound()
    {
        _setup.ResetContext();

        // Arrange

        // Act
        var result = await _setup.UserRepository.GetUserByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetUserByEmailAsync_ShouldReturnUser()
    {
        _setup.ResetContext();

        // Arrange
        var hmac = new System.Security.Cryptography.HMACSHA512();
        byte[] passwordSalt = hmac.Key;
        byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("password"));

        var user1 = new UserCreate(
            "John",
            "Doe",
            "Doevich",
            "testing",
            "test1@mail.ru",
            passwordHash,
            passwordSalt,
            new DateOnly(1990, 1, 1),
            SexType.Female,
            RoleType.Admin);

        var newUser = await _setup.UserRepository.CreateUserAsync(user1);

        // Act
        var result = await _setup.UserRepository.GetUserByEmailAsync(newUser.Email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newUser.Email, result!.Email);

    }

    [Fact]
    public async Task GetUserByEmailAsync_ShouldReturnNull_WhenUserNotFound()
    {
        _setup.ResetContext();

        // Arrange
        var email = "nonexist@mail.ru";
        // Act
        var result = await _setup.UserRepository.GetUserByEmailAsync(email);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateUserRoleAsync_ShouldUpdateUserRole()
    {
        _setup.ResetContext();

        // Arrange
        var hmac = new System.Security.Cryptography.HMACSHA512();
        byte[] passwordSalt = hmac.Key;
        byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("password"));

        var user1 = new UserCreate(
            "John",
            "Doe",
            "Doevich",
            "testing",
            "test1@mail.ru",
            passwordHash,
            passwordSalt,
            new DateOnly(1990, 1, 1),
            SexType.Female,
            RoleType.Verification);

        var newUser = await _setup.UserRepository.CreateUserAsync(user1);
        var userUpdateRole = new UserUpdateRole(RoleType.Operator);

        // Act
        var result = await _setup.UserRepository.UpdateUserRoleAsync(newUser.Id, userUpdateRole);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(RoleType.Operator, result.Role);

    }
    
    [Fact]
    public async Task UpdateUserRoleAsync_ShouldThrowException_WhenUserNotFound()
    {
        _setup.ResetContext();

        // Arrange
        var userUpdateRole = new UserUpdateRole(RoleType.Operator);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<UserNotFoundException>(() => _setup.UserRepository.UpdateUserRoleAsync(Guid.NewGuid(), userUpdateRole));
    }

    [Fact]
    public async Task DeleteUserAsync_ShouldDeleteUser()
    {
        _setup.ResetContext();

        // Arrange
        var hmac = new System.Security.Cryptography.HMACSHA512();
        byte[] passwordSalt = hmac.Key;
        byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("password"));

        var user1 = new UserCreate(
            "John",
            "Doe",
            "Doevich",
            "testing",
            "test1@mail.ru",
            passwordHash,
            passwordSalt,
            new DateOnly(1990, 1, 1),
            SexType.Female,
            RoleType.Admin);

        var newUser = await _setup.UserRepository.CreateUserAsync(user1);

        // Act
        await _setup.UserRepository.DeleteUserAsync(newUser.Id);

        // Assert
        var result = await _setup.Context.Users.FindAsync(newUser.Id);
        Assert.Null(result);
    }
    
    [Fact]
    public async Task DeleteUserAsync_ShouldThrowException_WhenUserNotFound()
    {
        _setup.ResetContext();

        // Arrange

        // Act & Assert
        var ex = await Assert.ThrowsAsync<UserRepositoryException>(() => _setup.UserRepository.DeleteUserAsync(Guid.NewGuid()));
    }

}