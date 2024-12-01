using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Models;

namespace ProdMonitor.Test.UnitTests.RepositoryTest;

public class OrderDetailRepositoryTests : IClassFixture<RepositoryTestsSetup>
{
    private readonly RepositoryTestsSetup _setup;
    
    public OrderDetailRepositoryTests(RepositoryTestsSetup setup)
    {
        _setup = setup;
    }
    
    [Fact]
    public async Task CreateOrderDetail_ShouldCreateOrderDetail()
    {
        _setup.ResetContext();
        
        var detail = new DetailCreate(
            "Detail1",
            "Russia",
            50,
            30,
            10,
            2,
            1);
        var newDetail = await _setup.DetailRepository.CreateDetailAsync(detail);
        
        // Arrange
        var orderDetail = new OrderDetailCreate(
            detailId: newDetail.Id,
            detailOrderId: Guid.NewGuid(),
            detailsAmount: 10);
        
        // Act
        var newOrderDetail = await _setup.OrderDetailRepository.CreateOrderDetail(orderDetail);
        
        // Assert
        var result = await _setup.Context.OrderDetails.FindAsync(newOrderDetail.Id);
        Assert.NotNull(result);
        Assert.Equal(newOrderDetail.Id, result.Id);
        Assert.Equal(orderDetail.DetailId, result.DetailId);
        Assert.Equal(orderDetail.DetailOrderId, result.DetailOrderId);
        Assert.Equal(orderDetail.DetailsAmount, result.DetailsAmount);
    }
    
    [Fact]
    public async Task CreateOrderDetail_ShouldThrowException_WhenDetailIdIsEmpty()
    {
        _setup.ResetContext();
        
        // Arrange
        var orderDetail = new OrderDetailCreate(
            detailId: Guid.Empty, 
            detailOrderId: Guid.NewGuid(),
            detailsAmount: 10);
        
        // Act
        var ex = await Assert.ThrowsAsync<OrderDetailRepositoryException>(() => _setup.OrderDetailRepository.CreateOrderDetail(orderDetail));
        
        // Assert
        Assert.Equal("Failed to create OrderDetail", ex.Message);
        Assert.IsType<OrderDetailRepositoryException>(ex.InnerException);
        Assert.Equal("Detail with Id not found", ex.InnerException?.Message);
    }
}
