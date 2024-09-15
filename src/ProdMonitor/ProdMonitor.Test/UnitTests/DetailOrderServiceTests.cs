using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Models;
using ProdMonitor.Application.Services;
using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Test.UnitTests
{
    public class DetailOrderServiceTests
    {
        private readonly Mock<IDetailOrderRepository> _mockOrderRepository;
        private readonly Mock<IOrderDetailRepository> _mockOrderDetailRepository;
        private readonly Mock<IDetailRepository> _mockDetailRepository;
        private readonly DetailOrderService _detailOrderService;

        public DetailOrderServiceTests()
        {
            _mockOrderRepository = new Mock<IDetailOrderRepository>();
            _mockOrderDetailRepository = new Mock<IOrderDetailRepository>();
            _mockDetailRepository = new Mock<IDetailRepository>();
            _detailOrderService = new DetailOrderService(
                _mockOrderRepository.Object,
                _mockOrderDetailRepository.Object,
                _mockDetailRepository.Object);
        }

        [Fact]
        public async Task CreateDetailOrderAsync_Should_Create_Order_With_Valid_Details()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var detailId = Guid.NewGuid();
            var orderDetails = new List<OrderDetailData>
            {
                new OrderDetailData(detailId, 2)
            };

            var detail = new Detail(detailId, "Detail 1", "Country 1", 100, 10.0f, 10, 10, 10);

            _mockDetailRepository
                .Setup(repo => repo.GetDetailByIdAsync(detailId))
                .ReturnsAsync(detail);

            _mockOrderRepository
                .Setup(repo => repo.CreateDetailOrderAsync(It.IsAny<Guid>(), It.IsAny<DetailOrderStatusType>(), It.IsAny<float>(), It.IsAny<DateTime>(), It.IsAny<ICollection<OrderDetailData>>()))
                .ReturnsAsync(new DetailOrder(Guid.NewGuid(), userId, DetailOrderStatusType.InWork, 20.0f, DateTime.Now));

            var orderCreate = new DetailOrderCreate(userId, orderDetails);

            // Act
            var result = await _detailOrderService.CreateDetailOrderAsync(orderCreate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(20.0f, result.TotalPrice);
            _mockOrderRepository.Verify(repo => repo.CreateDetailOrderAsync(userId, DetailOrderStatusType.InWork, 20.0f, It.IsAny<DateTime>(), orderDetails), Times.Once);
        }

        [Fact]
        public async Task CreateDetailOrderAsync_Should_Throw_Exception_If_Detail_Not_Found()
        {
            // Arrange
            var detailId = Guid.NewGuid();
            var orderDetails = new List<OrderDetailData>
            {
                new OrderDetailData(detailId, 2)
            };

            _mockDetailRepository
                .Setup(repo => repo.GetDetailByIdAsync(detailId))
                .ReturnsAsync((Detail)null);

            var orderCreate = new DetailOrderCreate(Guid.NewGuid(), orderDetails);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<DetailOrderException>(() => _detailOrderService.CreateDetailOrderAsync(orderCreate));
            Assert.IsType<DetailNotFoundException>(exception.InnerException);

        }

        [Fact]
        public async Task CreateDetailOrderAsync_Should_Throw_Exception_If_Order_Has_No_Details()
        {
            // Arrange
            var orderCreate = new DetailOrderCreate(Guid.NewGuid(), new List<OrderDetailData>());

            // Act & Assert
            var exception = await Assert.ThrowsAsync<DetailOrderException>(() => _detailOrderService.CreateDetailOrderAsync(orderCreate));
            Assert.IsType<ArgumentException>(exception.InnerException);

        }

        [Fact]
        public async Task CreateDetailOrderAsync_Should_Throw_DetailOrderException_On_Repository_Error()
        {
            // Arrange
            var detailId = Guid.NewGuid();
            var orderDetails = new List<OrderDetailData>
            {
                new OrderDetailData(detailId, 2)
            };

            var detail = new Detail(detailId, "Detail 1", "Country 1", 100, 10.0f, 10, 10, 10);

            _mockDetailRepository
                .Setup(repo => repo.GetDetailByIdAsync(detailId))
                .ReturnsAsync(detail);

            _mockOrderRepository
                .Setup(repo => repo.CreateDetailOrderAsync(It.IsAny<Guid>(), It.IsAny<DetailOrderStatusType>(), It.IsAny<float>(), It.IsAny<DateTime>(), It.IsAny<ICollection<OrderDetailData>>()))
                .ThrowsAsync(new Exception("Database error"));

            var orderCreate = new DetailOrderCreate(Guid.NewGuid(), orderDetails);

            // Act & Assert
            await Assert.ThrowsAsync<DetailOrderException>(() => _detailOrderService.CreateDetailOrderAsync(orderCreate));
        }

        // Test for GetAllDetailOrdersAsync
        [Fact]
        public async Task GetAllDetailOrdersAsync_Should_Return_Orders_With_Valid_Filter()
        {
            // Arrange
            var filter = new DetailOrderFilter(null, null, 0, 10);
            var orders = new List<DetailOrder>
            {
                new DetailOrder(Guid.NewGuid(), Guid.NewGuid(), DetailOrderStatusType.InWork, 100.0f, DateTime.Now),
                new DetailOrder(Guid.NewGuid(), Guid.NewGuid(), DetailOrderStatusType.Done, 200.0f, DateTime.Now)
            };

            _mockOrderRepository
                .Setup(repo => repo.GetAllDetailOrdersAsync(filter))
                .ReturnsAsync(orders);

            // Act
            var result = await _detailOrderService.GetAllDetailOrdersAsync(filter);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            _mockOrderRepository.Verify(repo => repo.GetAllDetailOrdersAsync(filter), Times.Once);
        }

        [Fact]
        public async Task GetAllDetailOrdersAsync_Should_Throw_DetailOrderException_On_Repository_Error()
        {
            // Arrange
            var filter = new DetailOrderFilter(null, null, 0, 10);

            _mockOrderRepository
                .Setup(repo => repo.GetAllDetailOrdersAsync(filter))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<DetailOrderException>(() => _detailOrderService.GetAllDetailOrdersAsync(filter));
        }

        // Test for GetDetailOrderById
        [Fact]
        public async Task GetDetailOrderById_Should_Return_Order_If_Found()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new DetailOrder(orderId, Guid.NewGuid(), DetailOrderStatusType.InWork, 100.0f, DateTime.Now);

            _mockOrderRepository
                .Setup(repo => repo.GetDetailOrderByIdAsync(orderId))
                .ReturnsAsync(order);

            // Act
            var result = await _detailOrderService.GetDetailOrderById(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderId, result.Id);
            _mockOrderRepository.Verify(repo => repo.GetDetailOrderByIdAsync(orderId), Times.Once);
        }

        [Fact]
        public async Task GetDetailOrderById_Should_Throw_DetailOrderNotFoundException_If_Order_Not_Found()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            _mockOrderRepository
                .Setup(repo => repo.GetDetailOrderByIdAsync(orderId))
                .ReturnsAsync((DetailOrder)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<DetailOrderException>(() => _detailOrderService.GetDetailOrderById(orderId));
            Assert.IsType<DetailOrderNotFoundException>(exception.InnerException);
        }

        [Fact]
        public async Task GetDetailOrderById_Should_Throw_DetailOrderException_On_Repository_Error()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            _mockOrderRepository
                .Setup(repo => repo.GetDetailOrderByIdAsync(orderId))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<DetailOrderException>(() => _detailOrderService.GetDetailOrderById(orderId));
        }
    }
}
