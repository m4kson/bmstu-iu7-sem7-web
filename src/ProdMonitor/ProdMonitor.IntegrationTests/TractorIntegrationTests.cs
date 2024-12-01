using System.Net;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProdMonitor.DataAccess.Context;
using ProdMonitor.Web.Dto.Tractors;

namespace ProdMonitor.IntegrationTests;

public class TractorIntegrationTests: IClassFixture<ProdMonitorApiApplicationFactory>
{
    private readonly ProdMonitorApiApplicationFactory _factory;
    
    public TractorIntegrationTests(ProdMonitorApiApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetTractors_WithExistingTractors_ReturnsListOfTractors()
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
        using var response = await httpClient.GetAsync("api/v1/Tractors");
        var result = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();
        
        var tractors = JsonConvert.DeserializeObject<List<TractorDto>>(result);
        
        tractors.Should().NotBeNull();
        tractors.Should().NotBeEmpty();
        Assert.Equal(2, tractors.Count);
    }
    
    [Fact]
    public async Task GetTractor_WithExistingTractor_ReturnsTractor()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ProdMonitorContext>();
            _factory.ReinitializeDbForTests(db);
        }
        // Arrange
        using var httpClient = _factory.CreateClient();
        Guid id = Guid.Parse("39675975-2a1e-4554-9aff-de8cb30b6c80");
        
        // Act
        using var response = await httpClient.GetAsync($"api/v1/Tractors/{id}");
        var result = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();
        
        var tractor = JsonConvert.DeserializeObject<TractorDto>(result);
        
        tractor.Should().NotBeNull();
    }
    
    [Fact]
    public async Task CreateTractor_WithValidData_ReturnsTractor()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ProdMonitorContext>();
            _factory.ReinitializeDbForTests(db);
        }
        // Arrange
        using var httpClient = _factory.CreateClient();
        var tractor = new
        {
            Model = "model2",
            ReleseYear = "2022",
            EngineType = "engineType2",
            EnginePower = "enginePower2",
            FrontTireSize = "2",
            BackTireSize = "2",
            WheelsAmount = "2",
            TankCapacity = "2",
            EcologicalStandart = "ecologicalStandart2",
            Length = "2",
            Width = "2",
            CabinHeight = "2"
        };
        var json = JsonConvert.SerializeObject(tractor);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        
        // Act
        using var response = await httpClient.PostAsync("api/v1/Tractors", data);
        var result = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();
        
        var createdTractor = JsonConvert.DeserializeObject<TractorDto>(result);
        
        createdTractor.Should().NotBeNull();
        createdTractor.Model.Should().Be("model2");
    }

    [Fact]
    public async Task DeleteTractor_WithExistingTractor_ReturnsNoContent()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ProdMonitorContext>();
            _factory.ReinitializeDbForTests(db);
        }
        // Arrange
        using var httpClient = _factory.CreateClient();
        Guid id = Guid.Parse("39675975-2a1e-4554-9aff-de8cb30b6c80");

        // Act
        using var response = await httpClient.DeleteAsync($"api/v1/Tractors/{id}");
        var result = await response.Content.ReadAsStringAsync();

        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().BeNullOrEmpty();
    }
}