using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProdMonitor.DataAccess.Context;
using ProdMonitor.Web.Dto.Orders;

namespace ProdMonitor.IntegrationTests;

public class DetailOrderIntegrationTests: IClassFixture<ProdMonitorApiApplicationFactory>
{
    private readonly ProdMonitorApiApplicationFactory _factory;
    
    public DetailOrderIntegrationTests(ProdMonitorApiApplicationFactory factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task GetDetailOrders_WithExistingDetailOrders_ReturnsListOfDetailOrders()
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
        using var response = await httpClient.GetAsync("api/v1/DetailOrders");
        var result = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();
        
        var detailOrders = JsonConvert.DeserializeObject<List<OrderDto>>(result);
        
        detailOrders.Should().NotBeNull();
        detailOrders.Should().NotBeEmpty();
        Assert.Equal(2, detailOrders.Count);
    }
    
    [Fact]
    public async Task GetDetailOrder_WithExistingDetailOrder_ReturnsDetailOrder()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ProdMonitorContext>();
            _factory.ReinitializeDbForTests(db);
        }
        // Arrange
        using var httpClient = _factory.CreateClient();
        Guid id = Guid.Parse("5215c9e2-c214-4335-b8d3-7bc9c22d2541");
        
        // Act
        using var response = await httpClient.GetAsync($"api/v1/DetailOrders/{id}");
        var result = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();
        
        var detailOrder = JsonConvert.DeserializeObject<OrderDto>(result);
        
        detailOrder.Should().NotBeNull();
    }
    
    [Fact]
    public async Task GetDetailOrder_WithNonExistingDetailOrder_ReturnsNotFound()
    {
        // Arrange
        using var httpClient = _factory.CreateClient();
        Guid id = Guid.Parse("5215c9e2-c214-4335-b8d3-7bc9c22d2542");
        
        // Act
        using var response = await httpClient.GetAsync($"api/v1/DetailOrders/{id}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}