using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Models;

namespace ProdMonitor.Test.UnitTests.RepositoryTest;

public class TractorRepositoryTests : IClassFixture<RepositoryTestsSetup>
{
    private readonly RepositoryTestsSetup _setup;
    
    public TractorRepositoryTests(RepositoryTestsSetup setup)
    {
        _setup = setup;
    }
    
    [Fact]
    public async Task CreateTractorAsync_ShouldAddTractor()
    {
        _setup.ResetContext();
        
        // Arrange
        var tractor = new TractorCreate(
            "Tractor1", 
            100, 
            "Diesel",
            "30",
            10,
            2,
            4, 
            4, 
            "EURO 5",
            100,
            15,
            500);

        // Act
        var newTractor = await _setup.TractorRepository.CreateTractorAsync(tractor);

        // Assert
        var result = await _setup.Context.Tractors.FindAsync(newTractor.Id);
        Assert.NotNull(result);
        Assert.Equal(newTractor.Id, result.Id);
    }
    
    [Fact]
    public async Task CreateTractorAsync_ShouldThrowException_WhenModelIsNull()
    {
        _setup.ResetContext();
        
        // Arrange
        var tractor = new TractorCreate(
            model: null, 
            releaseYear: 100, 
            engineType: "Diesel",
            enginePower: "30",
            frontTireSize: 10,
            backTireSize: 2,
            wheelsAmount: 4, 
            tankCapacity: 4, 
            ecologicalStandart: "EURO 5",
            length: 100,
            width: 15,
            cabinHeight: 500);

        // Act
        async Task CreateTractor() => await _setup.TractorRepository.CreateTractorAsync(tractor);

        // Assert
        await Assert.ThrowsAsync<TractorRepositoryException>(CreateTractor);
    }
    
    [Fact]
    public async Task GetAllTractorsAsync_ShouldReturnAllTractors()
    {
        _setup.ResetContext();
        
        // Arrange
        var tractor1 = new TractorCreate(
            "Tractor1", 
            100, 
            "Diesel",
            "30",
            10,
            2,
            4, 
            4, 
            "EURO 5",
            100,
            15,
            500);
        
        var tractor2 = new TractorCreate(
            "Tractor2", 
            100, 
            "Diesel",
            "30",
            10,
            2,
            4, 
            4, 
            "EURO 5",
            100,
            15,
            500);

        await _setup.TractorRepository.CreateTractorAsync(tractor1);
        await _setup.TractorRepository.CreateTractorAsync(tractor2);

        // Act
        var result = await _setup.TractorRepository.GetAllTractorsAsync(new TractorFilter());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }
    
    [Fact]
    public async Task GetAllTractorsAsync_ShouldReturnEmptyList_WhenNoTractors()
    {
        _setup.ResetContext();
        
        // Arrange

        // Act
        var result = await _setup.TractorRepository.GetAllTractorsAsync(new TractorFilter());

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task GetTractorByIdAsync_ShouldReturnTractor()
    {
        _setup.ResetContext();
        
        // Arrange
        var tractor = new TractorCreate(
            "Tractor1", 
            100, 
            "Diesel",
            "30",
            10,
            2,
            4, 
            4, 
            "EURO 5",
            100,
            15,
            500);
        var newTractor = await _setup.TractorRepository.CreateTractorAsync(tractor);

        // Act
        var result = await _setup.TractorRepository.GetTractorByIdAsync(newTractor.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newTractor.Id, result!.Id);
        Assert.Equal(tractor.Model, result.Model);
    }
    
    [Fact]
    public async Task GetTractorByIdAsync_ShouldReturnNull_WhenTractorNotFound()
    {
        _setup.ResetContext();
        
        // Arrange

        // Act
        var result = await _setup.TractorRepository.GetTractorByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }
}