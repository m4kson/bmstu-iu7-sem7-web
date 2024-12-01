using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ProdMonitor.Application.Services;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Models;
using Serilog;

namespace ProdMonitor.Test.UnitTests
{
    public class DetailServiceTests
    {
        private readonly Mock<IDetailRepository> _detailRepositoryMock;
        private readonly Mock<ILogger> _mockLogger;
        private readonly DetailService _detailService;

        public DetailServiceTests()
        {
            _detailRepositoryMock = new Mock<IDetailRepository>();
            _mockLogger = new Mock<ILogger>();
            _detailService = new DetailService(_detailRepositoryMock.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateDetailAsync_ValidDetail_ReturnsCreatedDetail()
        {
            // Arrange
            var detailCreate = new DetailCreate(
                name: "Detail1",
                country: "USA",
                amount: 100,
                price: 50.5f,
                length: 100,
                height: 50,
                width: 30,
                assemblyLines: null,
                orderDetails: null);

            var createdDetail = new Detail(
                id: Guid.NewGuid(),
                name: "Detail1",
                country: "USA",
                amount: 100,
                price: 50.5f,
                length: 100,
                height: 50,
                width: 30);

            _detailRepositoryMock
                .Setup(repo => repo.CreateDetailAsync(detailCreate))
                .ReturnsAsync(createdDetail);

            // Act
            var result = await _detailService.CreateDetailAsync(detailCreate);

            // Assert
            Assert.Equal(createdDetail, result);
            _detailRepositoryMock.Verify(repo => repo.CreateDetailAsync(detailCreate), Times.Once);
        }

        [Fact]
        public async Task CreateDetailAsync_ThrowsException_ThrowsDetailServiceException()
        {
            // Arrange
            var detailCreate = new DetailCreate(
                name: "Detail1",
                country: "USA",
                amount: 100,
                price: 50.5f,
                length: 100,
                height: 50,
                width: 30,
                assemblyLines: null,
                orderDetails: null);

            _detailRepositoryMock
                .Setup(repo => repo.CreateDetailAsync(detailCreate))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<DetailServiceException>(() => _detailService.CreateDetailAsync(detailCreate));
            Assert.Equal("Failed to create detail", exception.Message);
            _detailRepositoryMock.Verify(repo => repo.CreateDetailAsync(detailCreate), Times.Once);
        }

        [Fact]
        public async Task GetAllDetailsAsync_ValidFilter_ReturnsListOfDetails()
        {
            // Arrange
            var filter = new DetailFilter(country: "USA");

            var details = new List<Detail>
            {
                new Detail(Guid.NewGuid(), "Detail1", "USA", 100, 50.5f, 100, 50, 30),
                new Detail(Guid.NewGuid(), "Detail2", "USA", 200, 75.3f, 120, 60, 35)
            };

            _detailRepositoryMock
                .Setup(repo => repo.GetAllDetailsAsync(filter))
                .ReturnsAsync(details);

            // Act
            var result = await _detailService.GetAllDetailsAsync(filter);

            // Assert
            Assert.Equal(details, result);
            _detailRepositoryMock.Verify(repo => repo.GetAllDetailsAsync(filter), Times.Once);
        }

        [Fact]
        public async Task GetAllDetailsAsync_ThrowsException_ThrowsDetailServiceException()
        {
            // Arrange
            var filter = new DetailFilter(country: "USA");

            _detailRepositoryMock
                .Setup(repo => repo.GetAllDetailsAsync(filter))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<DetailServiceException>(() => _detailService.GetAllDetailsAsync(filter));
            Assert.Equal("Failed to get details", exception.Message);
            _detailRepositoryMock.Verify(repo => repo.GetAllDetailsAsync(filter), Times.Once);
        }

        [Fact]
        public async Task GetDetailByIdAsync_ValidId_ReturnsDetail()
        {
            // Arrange
            var detailId = Guid.NewGuid();

            var detail = new Detail(detailId, "Detail1", "USA", 100, 50.5f, 100, 50, 30);

            _detailRepositoryMock
                .Setup(repo => repo.GetDetailByIdAsync(detailId))
                .ReturnsAsync(detail);

            // Act
            var result = await _detailService.GetDetailByIdAsync(detailId);

            // Assert
            Assert.Equal(detail, result);
            _detailRepositoryMock.Verify(repo => repo.GetDetailByIdAsync(detailId), Times.Once);
        }

        [Fact]
        public async Task GetDetailByIdAsync_DetailNotFound_ThrowsDetailNotFoundException()
        {
            // Arrange
            var detailId = Guid.NewGuid();

            _detailRepositoryMock
                .Setup(repo => repo.GetDetailByIdAsync(detailId))
                .ReturnsAsync((Detail)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<DetailNotFoundException>(() => _detailService.GetDetailByIdAsync(detailId));
            _detailRepositoryMock.Verify(repo => repo.GetDetailByIdAsync(detailId), Times.Once);
        }

        [Fact]
        public async Task GetDetailByIdAsync_ThrowsException_ThrowsDetailServiceException()
        {
            // Arrange
            var detailId = Guid.NewGuid();

            _detailRepositoryMock
                .Setup(repo => repo.GetDetailByIdAsync(detailId))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<DetailServiceException>(() => _detailService.GetDetailByIdAsync(detailId));
            Assert.Equal("Failed to get detail", exception.Message);
            _detailRepositoryMock.Verify(repo => repo.GetDetailByIdAsync(detailId), Times.Once);
        }
    }
}

