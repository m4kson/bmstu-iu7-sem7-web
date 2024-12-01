using System.Text;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProdMonitor.DataAccess.Context;
using ProdMonitor.Web.Dto.AssemblyLines;

namespace ProdMonitor.IntegrationTests;

public class AssemblyLineIntegrationTests : IClassFixture<ProdMonitorApiApplicationFactory>
{
    private readonly ProdMonitorApiApplicationFactory _factory;

    public AssemblyLineIntegrationTests(ProdMonitorApiApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetAssemblyLines_WithExistingAssemblyLines_ReturnsListOfAssemblyLines()
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
        using var response = await httpClient.GetAsync("api/v1/AssemblyLines");
        var result = await response.Content.ReadAsStringAsync();
       
        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();
        var assemblyLines = JsonConvert.DeserializeObject<List<AssemblyLineDto>>(result);
        assemblyLines.Should().NotBeNull();
        assemblyLines.Should().NotBeEmpty();
        Assert.Equal(3, assemblyLines.Count);
    }

    [Fact]
    public async Task GetAssemblyLine_WithExistingAssemblyLine_ReturnsAssemblyLine()
    {
        // Arrange
        using var httpClient = _factory.CreateClient();
        Guid id = Guid.Parse("ec539f4e-0811-40bd-b077-8b9e604f0345");

        // Act
        using var response = await httpClient.GetAsync($"api/v1/AssemblyLines/{id}");
        var result = await response.Content.ReadAsStringAsync();

        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();

        var assemblyLine = JsonConvert.DeserializeObject<AssemblyLineDto>(result);

        assemblyLine.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateAssemblyLine_WithValidData_ReturnsCreatedAssemblyLine()
    {
        // Arrange
        using var httpClient = _factory.CreateClient();
        var assemblyLine = new
        {
            Name = "Assembly Line 4",
            Length = "120",
            Width = "100",
            Height = "80",
            Status = "Working",
            Downtime = "0",
            InspectionsPerYear = "3",
            LastInspection = "2022-01-01",
            NextInspection = "2022-12-31",
            DefectRate = "1"
        };
        var content = new StringContent(JsonConvert.SerializeObject(assemblyLine), Encoding.UTF8, "application/json");

        // Act
        using var response = await httpClient.PostAsync("api/v1/AssemblyLines", content);
        var result = await response.Content.ReadAsStringAsync();

        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().NotBeNullOrEmpty();

        var createdAssemblyLine = JsonConvert.DeserializeObject<AssemblyLineDto>(result);

        createdAssemblyLine.Should().NotBeNull();
        createdAssemblyLine.Name.Should().Be(assemblyLine.Name);
    
    }

    [Fact]
    public async Task DeleteAssemblyLine_WithExistingAssemblyLine_DeletedLine()
    {
        // Arrange
        using var httpClient = _factory.CreateClient();
        Guid id = Guid.Parse("a04716ca-81bb-4c46-9b6f-b4d601b1d8e0");

        // Act
        using var response = await httpClient.DeleteAsync($"api/v1/AssemblyLines/{id}");
        var result = await response.Content.ReadAsStringAsync();

        // Assert
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        result.Should().BeNullOrEmpty();
    }
}