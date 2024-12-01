using System.Net;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProdMonitor.DataAccess.Context;
using ProdMonitor.Web.Dto.Details;

namespace ProdMonitor.IntegrationTests;

public class DetailIntegrationTests : IClassFixture<ProdMonitorApiApplicationFactory>
{
    private readonly ProdMonitorApiApplicationFactory _factory;
    
    public DetailIntegrationTests(ProdMonitorApiApplicationFactory factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task GetDetails_WithExistingDetails_ReturnsListOfDetails()
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
        using var response = await httpClient.GetAsync("api/v1/Details");
        var result = await response.Content.ReadAsStringAsync();
    
        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();
    
        var details = JsonConvert.DeserializeObject<List<DetailDto>>(result);
    
        details.Should().NotBeNull();
        details.Should().NotBeEmpty();
        Assert.Equal(2, details.Count);
    }
    
    [Fact]
    public async Task GetDetail_WithExistingDetail_ReturnsDetail()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ProdMonitorContext>();
            _factory.ReinitializeDbForTests(db);
        }
        
        // Arrange
        using var httpClient = _factory.CreateClient();
        Guid id = Guid.Parse("0b145e17-dccc-42a8-80e5-3e2906ef08a3");
        
        // Act
        using var response = await httpClient.GetAsync($"api/v1/Details/{id}");
        var result = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();
        
        var detail = JsonConvert.DeserializeObject<DetailDto>(result);
        
        detail.Should().NotBeNull();
    }
    
    [Fact]
    public async Task CreateDetail_WithValidDetail_ReturnsDetail()
    {
        // Arrange
        using var httpClient = _factory.CreateClient();
        var detail = new 
        {
            Name = "Detail 3",
            Country = "Russia",
            Amount = "10",
            Price = "100",
            Length = "10",
            Width = "10",
            Height = "10"
        };
        var content = new StringContent(JsonConvert.SerializeObject(detail), Encoding.UTF8, "application/json");
    
        // Act
        using var response = await httpClient.PostAsync("api/v1/Details", content);
        var result = await response.Content.ReadAsStringAsync();
    
        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();
    
        var createdDetail = JsonConvert.DeserializeObject<DetailDto>(result);
    
        createdDetail.Should().NotBeNull();
        createdDetail.Name.Should().Be(detail.Name);
    }
    
    [Fact]
    public async Task DeleteDetail_WithExistingDetail_ReturnsNoContent()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ProdMonitorContext>();
            _factory.ReinitializeDbForTests(db);
        }
        // Arrange
        using var httpClient = _factory.CreateClient();
        Guid id = Guid.Parse("0b145e17-dccc-42a8-80e5-3e2906ef08a3");
    
        // Act
        using var response = await httpClient.DeleteAsync($"api/v1/Details/{id}");
        var result = await response.Content.ReadAsStringAsync();
    
        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().BeNullOrEmpty();
    }
}