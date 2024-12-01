using System.Net;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProdMonitor.DataAccess.Context;
using ProdMonitor.Web.Dto.Auth;
using ProdMonitor.Web.Dto.Enums;
using ProdMonitor.Web.Dto.Users;

namespace ProdMonitor.IntegrationTests;

public class UsersIntegrationTests : IClassFixture<ProdMonitorApiApplicationFactory>
{
    private readonly ProdMonitorApiApplicationFactory _factory;
    
    public UsersIntegrationTests(ProdMonitorApiApplicationFactory factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task GetUsers_WithExistingUsers_ReturnsListOfUsers()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ProdMonitorContext>();
            _factory.ReinitializeDbForTests(db);
        }
        // Arrange
        using var httpClient = _factory.CreateClient();
        
        // Act
        using var response = await httpClient.GetAsync("api/v1/Users");
        var result = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();
        
        var users = JsonConvert.DeserializeObject<List<UserDto>>(result);
        
        users.Should().NotBeNull();
        users.Should().NotBeEmpty();
        Assert.Equal(3, users.Count);
    }
    
    [Fact]
    public async Task GetUser_WithExistingUser_ReturnsUser()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ProdMonitorContext>();
            _factory.ReinitializeDbForTests(db);
        }
        // Arrange
        using var httpClient = _factory.CreateClient();
        Guid id = Guid.Parse("203b583b-fe1d-4e1c-b3c6-9408b907bd38");
        
        // Act
        using var response = await httpClient.GetAsync($"api/v1/Users/{id}");
        var result = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();
        
        var user = JsonConvert.DeserializeObject<UserDto>(result);
        
        user.Should().NotBeNull();
        Assert.Equal(id, user.Id);
        Assert.Equal("admin", user.Name);
    }
    
    [Fact]
    public async Task GetUser_WithNonExistingUser_ReturnsNotFound()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ProdMonitorContext>();
            _factory.ReinitializeDbForTests(db);
        }
        // Arrange
        using var httpClient = _factory.CreateClient();
        Guid id = Guid.NewGuid();
        
        // Act
        using var response = await httpClient.GetAsync($"api/v1/Users/{id}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task UpdateUser_WithExistingUser_ReturnsUpdatedUser()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ProdMonitorContext>();
            _factory.ReinitializeDbForTests(db);
        }
        // Arrange
        using var httpClient = _factory.CreateClient();
        Guid id = Guid.Parse("203b583b-fe1d-4e1c-b3c6-9408b907bd38");
        var userUpdateDto = new 
        {
            Name = "new admin",
            Surname = "string",
            Fathername = "string",
            Department = "new department",
            Email = "string",
            Password = "string",
            BirthDate = "1990-01-01",   
            Sex = "male"
        };
        
        var content = new StringContent(JsonConvert.SerializeObject(userUpdateDto), Encoding.UTF8, "application/json");

        // Act
        using var response = await httpClient.PutAsync($"api/v1/Users/{id}", content);
        var result = await response.Content.ReadAsStringAsync();

        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();

        var updatedUser = JsonConvert.DeserializeObject<UserDto>(result);

        updatedUser.Should().NotBeNull();
        Assert.Equal(id, updatedUser.Id);
        Assert.Equal("new admin", updatedUser.Name);
        Assert.Equal("new department", updatedUser.Department);
    }

    [Fact]
    public async Task UpdateUserRole_WithExistingUser_ReturnsUpdatedUser()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ProdMonitorContext>();
            _factory.ReinitializeDbForTests(db);
        }
        //arrange
        using var httpClient = _factory.CreateClient();
        Guid id = Guid.Parse("203b583b-fe1d-4e1c-b3c6-9408b907bd38");
        var userRoleUpdateDto = new { Role = "Specialist" };
        
        var content = new StringContent(JsonConvert.SerializeObject(userRoleUpdateDto), Encoding.UTF8, "application/json");
        
        //act
        using var response = await httpClient.PatchAsync($"api/v1/Users/UpdateRole/{id}", content);
        var result = await response.Content.ReadAsStringAsync();
        
        //assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();
        
        var updatedUser = JsonConvert.DeserializeObject<UserDto>(result);
        
        updatedUser.Should().NotBeNull();
        Assert.Equal(id, updatedUser.Id);
        Assert.Equal(RoleTypeDto.Specialist, updatedUser.Role);
    }
    
    [Fact]
    public async Task UpdateUserRole_WithNonExistingUser_ReturnsNotFound()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ProdMonitorContext>();
            _factory.ReinitializeDbForTests(db);
        }
        // Arrange
        using var httpClient = _factory.CreateClient();
        Guid id = Guid.NewGuid();
        var userRoleUpdateDto = new { Role = "Specialist" };
        
        var content = new StringContent(JsonConvert.SerializeObject(userRoleUpdateDto), Encoding.UTF8, "application/json");
        
        // Act
        using var response = await httpClient.PatchAsync($"api/v1/Users/UpdateRole/{id}", content);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}