using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProdMonitor.DataAccess.Context;
using ProdMonitor.Web.Dto.Reports;

namespace ProdMonitor.IntegrationTests;

public class ServiceReportIntegrationTests: IClassFixture<ProdMonitorApiApplicationFactory>
{
    private readonly ProdMonitorApiApplicationFactory _factory;
    
    public ServiceReportIntegrationTests(ProdMonitorApiApplicationFactory factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task GetServiceReports_WithExistingServiceReports_ReturnsListOfServiceReports()
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
        using var response = await httpClient.GetAsync("api/v1/ServiceReports");
        var result = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();
        
        var serviceReports = JsonConvert.DeserializeObject<List<ReportDto>>(result);
        
        serviceReports.Should().NotBeNull();
        serviceReports.Should().NotBeEmpty();
        Assert.Equal(2, serviceReports.Count);
    }
    
    [Fact]
    public async Task GetServiceReport_WithExistingServiceReport_ReturnsServiceReport()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ProdMonitorContext>();
            _factory.ReinitializeDbForTests(db);
        }
        // Arrange
        using var httpClient = _factory.CreateClient();
        Guid id = Guid.Parse("938d9d85-4665-4d30-b549-fef4593c8a78");
        
        // Act
        using var response = await httpClient.GetAsync($"api/v1/ServiceReports/{id}");
        var result = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();
        
        var serviceReport = JsonConvert.DeserializeObject<ReportDto>(result);
        
        serviceReport.Should().NotBeNull();
    }
    
    [Fact]
    public async Task CloseReport_WithExistingServiceReport_ClosesServiceReport()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ProdMonitorContext>();
            _factory.ReinitializeDbForTests(db);
        }
        // Arrange
        using var httpClient = _factory.CreateClient();
        Guid id = Guid.Parse("5a700c95-55e6-4b08-9d5f-6b59a9c3e11e");
        var closeData = new
        {
            TotalPrice = "1000",
            Description = "Test"
        };
        var content = new StringContent(JsonConvert.SerializeObject(closeData), Encoding.UTF8, "application/json");
        
        // Act
        using var response = await httpClient.PatchAsync($"api/v1/ServiceReports/close/{id}", content);
        
        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
    }
    
    [Fact]
    public async Task CreateServiceReport_WithValidData_CreatesServiceReport()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ProdMonitorContext>();
            _factory.ReinitializeDbForTests(db);
        }
        // Arrange
        using var httpClient = _factory.CreateClient();
        var serviceReportCreateDto = new
        {
            UserId = "a991df27-a0ff-4a4e-8173-fcf3a40befb0",
            RequestId = "80DE302B-DDFA-4F5E-A82C-0068C7699A3D",
        };
        var content = new StringContent(JsonConvert.SerializeObject(serviceReportCreateDto), Encoding.UTF8, "application/json");
        
        // Act
        using var response = await httpClient.PostAsync("api/v1/ServiceReports", content);
        var result = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
    }
}