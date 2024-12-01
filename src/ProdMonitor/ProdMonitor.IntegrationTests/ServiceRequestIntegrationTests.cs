using System.Text;
using System.Transactions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProdMonitor.DataAccess.Context;
using ProdMonitor.Web.Dto.Requests;

namespace ProdMonitor.IntegrationTests;

public class ServiceRequestIntegrationTests : IClassFixture<ProdMonitorApiApplicationFactory>
{
    private readonly ProdMonitorApiApplicationFactory _factory;

    public ServiceRequestIntegrationTests(ProdMonitorApiApplicationFactory factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task GetServiceRequests_WithExistingRequests_ReturnsListOfRequests()
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
        using var response = await httpClient.GetAsync("api/v1/ServiceRequests");
        var result = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();
        
        var serviceRequests = JsonConvert.DeserializeObject<List<RequestDto>>(result);
        
        serviceRequests.Should().NotBeNull();
        serviceRequests.Should().NotBeEmpty();
        Assert.Equal(3, serviceRequests.Count);  
    }
    
    [Fact]
    public async Task GetServiceRequest_WithExistingRequest_ReturnsRequest()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ProdMonitorContext>();
            _factory.ReinitializeDbForTests(db);
        }
        // Arrange
        using var httpClient = _factory.CreateClient();
        Guid id = Guid.Parse("ea4f7424-c870-4c2a-b1db-cf65af3d5564");
        
        // Act
        using var response = await httpClient.GetAsync($"api/v1/ServiceRequests/{id}");
        var result = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();
        
        var serviceRequest = JsonConvert.DeserializeObject<RequestDto>(result);
        
        serviceRequest.Should().NotBeNull();
    }
    
    [Fact]
    public async Task CreateServiceRequest_WithValidRequest_ReturnsCreatedRequest()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ProdMonitorContext>();
            _factory.ReinitializeDbForTests(db);
        }
        // Arrange
        using var httpClient = _factory.CreateClient();
        var request = new 
        {
            LineId = "ec539f4e-0811-40bd-b077-8b9e604f0345",
            UserId = "93ba8784-b320-49d8-b810-8ad1e1bd0cf8",
            Type = "inspection",
            Description = "Service request description"
        };
        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        
        // Act
        using var response = await httpClient.PostAsync("api/v1/ServiceRequests", content);
        var result = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();
        
        var serviceRequest = JsonConvert.DeserializeObject<RequestDto>(result);
        
        serviceRequest.Should().NotBeNull();
        serviceRequest.Description.Should().Be(request.Description);
    }
}