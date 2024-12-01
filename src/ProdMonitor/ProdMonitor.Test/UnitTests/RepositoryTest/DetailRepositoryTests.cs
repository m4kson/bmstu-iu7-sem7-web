using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Models;
using ProdMonitor.Test.UnitTests.RepositoryTest.Helpers;

namespace ProdMonitor.Test.UnitTests.RepositoryTest;

public class DetailRepositoryTests : IClassFixture<RepositoryTestsSetup>
{
    private readonly RepositoryTestsSetup _setup;
    
    public DetailRepositoryTests(RepositoryTestsSetup setup)
    {
        _setup = setup;
    }
    
    [Fact]
    public async Task CreateDetailAsync_ShouldAddDetail()
    {
        _setup.ResetContext();
        
        // Arrange
        var detail = DetailMother.Default();
        
        // Act
        var newDetail = await _setup.DetailRepository.CreateDetailAsync(detail);

        // Assert
        var result = await _setup.Context.Details.FindAsync(newDetail.Id);
        Assert.NotNull(result);
        Assert.Equal(newDetail.Id, result.Id);
        Assert.Equal(detail.Name, result.Name);
    }
    
    [Fact]
    public async Task CreateDetailAsync_ShouldThrowException_WhenNameIsNull()
    {
        _setup.ResetContext();
        
        // Arrange
        var detail = DetailMother.Broken();
        
        // Act
        var ex = await Assert.ThrowsAsync<DetailRepositoryException>(() => _setup.DetailRepository.CreateDetailAsync(detail));
        
        // Assert
        Assert.Equal("Failed to create detail", ex.Message);
    }
    
    [Fact]
    public async Task GetAllDetailsAsync_ShouldReturnAllDetails()
    {
        _setup.ResetContext();
        
        // Arrange
        var detail1 = DetailMother.Default();
        
        var detail2 = DetailMother.Default();
        
        var detail3 = DetailMother.Default();
        
        await _setup.DetailRepository.CreateDetailAsync(detail1);
        await _setup.DetailRepository.CreateDetailAsync(detail2);
        await _setup.DetailRepository.CreateDetailAsync(detail3);
        
        // Act
        var details = await _setup.DetailRepository.GetAllDetailsAsync(new DetailFilter("Russia"));
        
        // Assert
        Assert.Equal(3, details.Count);
    }
    
    [Fact]
    public async Task GetAllDetailsAsync_ShouldReturnEmptyList_WhenCountryIsNull()
    {
        _setup.ResetContext();
        
        // Arrange
        var detail1 =  DetailMother.Default();
        
        var detail2 =  DetailMother.Default();
        
        var detail3 =  DetailMother.Default();
        
        await _setup.DetailRepository.CreateDetailAsync(detail1);
        await _setup.DetailRepository.CreateDetailAsync(detail2);
        await _setup.DetailRepository.CreateDetailAsync(detail3);
        
        // Act
        var details = await _setup.DetailRepository.GetAllDetailsAsync(new DetailFilter("Germany"));
        
        // Assert
        Assert.Empty(details);
    }
    
    [Fact]
    public async Task GetDetailByIdAsync_ShouldReturnDetail()
    {
        _setup.ResetContext();
        
        // Arrange
        var detail =  DetailMother.Default();
        
        var newDetail = await _setup.DetailRepository.CreateDetailAsync(detail);
        
        // Act
        var result = await _setup.DetailRepository.GetDetailByIdAsync(newDetail.Id);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(newDetail.Id, result!.Id);
        Assert.Equal(detail.Name, result.Name);
    }
    
    [Fact]
    public async Task GetDetailByIdAsync_ShouldReturnNull_WhenDetailNotFound()
    {
        _setup.ResetContext();
        
        // Arrange
        
        // Act
        var result = await _setup.DetailRepository.GetDetailByIdAsync(Guid.NewGuid());
        
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task UpdateDetailAsync_ShouldUpdateDetail()
    {
        _setup.ResetContext();
        
        // Arrange
        var detail =  DetailMother.Default();
        
        var newDetail = await _setup.DetailRepository.CreateDetailAsync(detail);
        
        // Act
        await _setup.DetailRepository.UpdateDetailAsync(newDetail.Id, 100);
        
        // Assert
        var result = await _setup.Context.Details.FindAsync(newDetail.Id);
        Assert.Equal(100, result.Amount);
    }
    
    [Fact]
    public async Task UpdateDetailAsync_ShouldThrowException_WhenDetailNotFound()
    {
        _setup.ResetContext();
        
        // Arrange
        
        // Act
        var ex = await Assert.ThrowsAsync<Exception>(() => _setup.DetailRepository.UpdateDetailAsync(Guid.NewGuid(), 100));
        
        // Assert
        Assert.Equal("Failed to update detail", ex.Message);
    }
}