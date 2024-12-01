using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Test.UnitTests.RepositoryTest;

public class DetailOrderRepositoryTests : IClassFixture<RepositoryTestsSetup>
{
    private readonly RepositoryTestsSetup _setup;
    
    public DetailOrderRepositoryTests(RepositoryTestsSetup setup)
    {
        _setup = setup;
    }
    
    [Fact]
    public async Task CreateDetailOrderAsync_PositiveTest()
    {
        _setup.ResetContext();
        
        //Arrange
        var userId = Guid.NewGuid();
        var status = DetailOrderStatusType.InWork;
        var totalPrice = 100.0f;
        var orderDate = DateTime.Now;
        var orderDetails = new List<OrderDetailData>
        {
            new OrderDetailData(Guid.NewGuid(), 1),
            new OrderDetailData(Guid.NewGuid(), 2)
        };
        
        //Act
        var result = await _setup.DetailOrderRepository.CreateDetailOrderAsync(userId, status, totalPrice, orderDate, orderDetails);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.UserId);
        Assert.Equal(status, result.Status);
        Assert.Equal(totalPrice, result.TotalPrice);
        Assert.Equal(orderDate, result.OrderDate);
        Assert.Equal(orderDetails.Count, result.OrderDetails.Count);
    }
    
    [Fact]
    public async Task CreateDetailOrderAsync_NegativeTest()
    {
        _setup.ResetContext();
        //Arrange
        var userId = Guid.NewGuid();
        var status = DetailOrderStatusType.InWork;
        var totalPrice = 100.0f;
        var orderDate = DateTime.Now;
        var orderDetails = new List<OrderDetailData>();
        
        //Act
        var result = await _setup.DetailOrderRepository.CreateDetailOrderAsync(userId, status, totalPrice, orderDate, orderDetails);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.UserId);
        Assert.Equal(status, result.Status);
        Assert.Equal(totalPrice, result.TotalPrice);
        Assert.Equal(orderDate, result.OrderDate);
        Assert.Equal(orderDetails.Count, result.OrderDetails.Count);
    }
    
    [Fact]
    public async Task GetAllDetailOrdersAsync_PositiveTest()
    {
        _setup.ResetContext();
        
        //Arrange
        var userId = Guid.NewGuid();
        var status = DetailOrderStatusType.InWork;
        var totalPrice = 100.0f;
        var orderDate = DateTime.Now;
        var orderDetails = new List<OrderDetailData>
        {
            new OrderDetailData(Guid.NewGuid(), 1),
            new OrderDetailData(Guid.NewGuid(), 2)
        };
        
        var detailOrder = await _setup.DetailOrderRepository.CreateDetailOrderAsync(userId, status, totalPrice, orderDate, orderDetails);
        
        var filter = new DetailOrderFilter();
        
        //Act
        var result = await _setup.DetailOrderRepository.GetAllDetailOrdersAsync(filter);
        
        //Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Single(result);
        Assert.Equal(detailOrder.Id, result[0].Id);
    }
    
    [Fact]
    public async Task GetAllDetailOrdersAsync_NegativeTest()
    {
        _setup.ResetContext();
        
        //Arrange
        var userId = Guid.NewGuid();
        var status = DetailOrderStatusType.InWork;
        var totalPrice = 100.0f;
        var orderDate = DateTime.Now;
        var orderDetails = new List<OrderDetailData>
        {
            new OrderDetailData(Guid.NewGuid(), 1),
            new OrderDetailData(Guid.NewGuid(), 2)
        };
        
        await _setup.DetailOrderRepository.CreateDetailOrderAsync(userId, status, totalPrice, orderDate, orderDetails);
        
        var filter = new DetailOrderFilter(userId: Guid.NewGuid());
        
        //Act
        var result = await _setup.DetailOrderRepository.GetAllDetailOrdersAsync(filter);
        
        //Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task GetDetailOrderByIdAsync_PositiveTest()
    {
        _setup.ResetContext();
        
        //Arrange
        var userId = Guid.NewGuid();
        var status = DetailOrderStatusType.InWork;
        var totalPrice = 100.0f;
        var orderDate = DateTime.Now;
        var orderDetails = new List<OrderDetailData>
        {
            new OrderDetailData(Guid.NewGuid(), 1),
            new OrderDetailData(Guid.NewGuid(), 2)
        };
        
        var detailOrder = await _setup.DetailOrderRepository.CreateDetailOrderAsync(userId, status, totalPrice, orderDate, orderDetails);
        
        //Act
        var result = await _setup.DetailOrderRepository.GetDetailOrderByIdAsync(detailOrder.Id);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(detailOrder.Id, result!.Id);
    }
    
    [Fact]
    public async Task GetDetailOrderByIdAsync_NegativeTest()
    {
        _setup.ResetContext();
        
        //Arrange
        var userId = Guid.NewGuid();
        var status = DetailOrderStatusType.InWork;
        var totalPrice = 100.0f;
        var orderDate = DateTime.Now;
        var orderDetails = new List<OrderDetailData>
        {
            new OrderDetailData(Guid.NewGuid(), 1),
            new OrderDetailData(Guid.NewGuid(), 2)
        };
        
        await _setup.DetailOrderRepository.CreateDetailOrderAsync(userId, status, totalPrice, orderDate, orderDetails);
        
        //Act
        var result = await _setup.DetailOrderRepository.GetDetailOrderByIdAsync(Guid.NewGuid());
        
        //Assert
        Assert.Null(result);
    }
}