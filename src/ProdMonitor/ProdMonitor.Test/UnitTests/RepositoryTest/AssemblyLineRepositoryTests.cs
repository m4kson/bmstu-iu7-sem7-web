using Moq;
using ProdMonitor.DataAccess.Repositories;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.Test.UnitTests.RepositoryTest.Helpers;
using Xunit;
using Serilog;


namespace ProdMonitor.Test.UnitTests.RepositoryTest;

public class AssemblyLineRepositoryTests : IClassFixture<RepositoryTestsSetup>
{
    private readonly RepositoryTestsSetup _setup;
    
    public AssemblyLineRepositoryTests(RepositoryTestsSetup setup)
    {
        _setup = setup;
    }
    
    [Fact]
    public async Task AddAssemblyLineAsync_ShouldAddAssemblyLine()
    {
        _setup.ResetContext();
        
        // Arrange
        var assemblyLine = new AssemblyLineBuilder()
            .WithName("TestLine1")
            .WithStatus(LineStatusType.Working)
            .Build();

        // Act
        var newLine = await _setup.AssemblyLineRepository.CreateAssemblyLineAsync(assemblyLine);

        // Assert
        var result = await _setup.Context.AssemblyLines.FindAsync(newLine.Id);
        Assert.NotNull(result);
        Assert.Equal(newLine.Id, result.Id);
        Assert.Equal(assemblyLine.Name, result.Name);
    }
    
    [Fact]
    public async Task CreateAssemblyLineAsync_ShouldThrowException_WhenNameIsNull()
    {
        _setup.ResetContext();
        
        // Arrange
        var assemblyLine = new AssemblyLineBuilder()
            .WithName(null)
            .Build();

        // Act & Assert
        var ex = await Assert.ThrowsAsync<AssemblyLineRepositoryException>(() => _setup.AssemblyLineRepository.CreateAssemblyLineAsync(assemblyLine));
        Assert.Equal("Failed to create line", ex.Message);
    }
    
    [Fact]
    public async Task GetAllAssemblyLinesAsync_ShouldReturnAllAssemblyLines()
    {
        _setup.ResetContext();
        
        // Arrange
        var assemblyLine1 = new AssemblyLineBuilder()
            .WithName("Line1")
            .Build();
        var assemblyLine2 = new AssemblyLineBuilder()
            .WithName("Line2")
            .Build();
        var assemblyLine3 = new AssemblyLineBuilder()
            .WithName("Line3")
            .Build();
        
        await _setup.AssemblyLineRepository.CreateAssemblyLineAsync(assemblyLine1);
        await _setup.AssemblyLineRepository.CreateAssemblyLineAsync(assemblyLine2);
        await _setup.AssemblyLineRepository.CreateAssemblyLineAsync(assemblyLine3);

        // Act
        var result = await _setup.AssemblyLineRepository.GetAllAssemblyLinesAsync(new AssemblyLineFilter());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }
    
    [Fact]
    public async Task GetAllAssemblyLinesAsync_ShouldReturnEmptyList_WhenNoAssemblyLines()
    {
        _setup.ResetContext();
        
        // Arrange

        // Act
        var result = await _setup.AssemblyLineRepository.GetAllAssemblyLinesAsync(new AssemblyLineFilter());

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task GetAssemblyLineByIdAsync_ShouldReturnAssemblyLine()
    {
        _setup.ResetContext();
        
        // Arrange
        var assemblyLine = new AssemblyLineBuilder()
            .Build();
        var newLine = await _setup.AssemblyLineRepository.CreateAssemblyLineAsync(assemblyLine);

        // Act
        var result = await _setup.AssemblyLineRepository.GetAssemblyLineByIdAsync(newLine.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newLine.Id, result!.Id);
        Assert.Equal(assemblyLine.Name, result.Name);
    }
    
    [Fact]
    public async Task GetAssemblyLineByIdAsync_ShouldReturnNull_WhenLineNotFound()
    {
        _setup.ResetContext();
        
        // Arrange

        // Act
        var result = await _setup.AssemblyLineRepository.GetAssemblyLineByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAssemblyLineAsync_ShouldUpdateAssemblyLine()
    {
        _setup.ResetContext();
        
        // Arrange
        var assemblyLine = new AssemblyLineBuilder()
            .Build();
        var newLine = await _setup.AssemblyLineRepository.CreateAssemblyLineAsync(assemblyLine);
        var assemblyLineUpdate = new AssemblyLineUpdate(
            name: "Line2",
            status: LineStatusType.OnService,
            downTime: 20,
            inspectionsPerYear: 4);

        // Act
        var result = await _setup.AssemblyLineRepository.UpdateAssemblyLineAsync(newLine.Id, assemblyLineUpdate);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newLine.Id, result.Id);
        Assert.Equal(assemblyLineUpdate.Name, result.Name);
        Assert.Equal(assemblyLineUpdate.Status, result.Status);
        Assert.Equal(assemblyLineUpdate.DownTime, result.DownTime);
        Assert.Equal(assemblyLineUpdate.InspectionsPerYear, result.InspectionsPerYear);
    }
    
    [Fact]
    public async Task UpdateAssemblyLineAsync_ShouldThrowException_WhenLineNotFound()
    {
        _setup.ResetContext();
        
        // Arrange
        var assemblyLineUpdate = new AssemblyLineUpdate(
            name: "Line2",
            status: LineStatusType.OnService,
            downTime: 20,
            inspectionsPerYear: 4);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<AssemblyLineRepositoryException>(() => _setup.AssemblyLineRepository.UpdateAssemblyLineAsync(Guid.NewGuid(), assemblyLineUpdate));
        Assert.Equal("Failed to update AssemblyLine", ex.Message);
        Assert.IsType<KeyNotFoundException>(ex.InnerException);
        Assert.Equal("Line not found.", ex.InnerException?.Message);
    }
}