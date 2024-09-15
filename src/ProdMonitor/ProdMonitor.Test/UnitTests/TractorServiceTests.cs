using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ProdMonitor.Application.Services;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Models;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using ProdMonitor.Domain.Interfaces.Services;

namespace ProdMonitor.Test.UnitTests
{
    public class TractorServiceTests
    {
        private readonly Mock<ITractorRepository> _tractorRepositoryMock;
        private readonly TractorService _tractorService;

        public TractorServiceTests()
        {
            _tractorRepositoryMock = new Mock<ITractorRepository>();
            _tractorService = new TractorService(_tractorRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateTractorAsync_ValidTractor_ReturnsCreatedTractor()
        {
            // Arrange
            var tractorCreate = new TractorCreate("ModelX", 2023, "Diesel", "200HP", 16, 20, 4, 100, "Euro5", 5.0f, 2.5f, 3.0f, null);
            var createdTractor = new Tractor(Guid.NewGuid(), "ModelX", 2023, "Diesel", "200HP", 16, 20, 4, 100, "Euro5", 5.0f, 2.5f, 3.0f);

            _tractorRepositoryMock
                .Setup(repo => repo.CreateTractorAsync(tractorCreate))
                .ReturnsAsync(createdTractor);

            // Act
            var result = await _tractorService.CreateTractorAsync(tractorCreate);

            // Assert
            Assert.Equal(createdTractor, result);
            _tractorRepositoryMock.Verify(repo => repo.CreateTractorAsync(tractorCreate), Times.Once);
        }

        [Fact]
        public async Task CreateTractorAsync_ThrowsException_ThrowsTractorServiceException()
        {
            // Arrange
            var tractorCreate = new TractorCreate("ModelX", 2023, "Diesel", "200HP", 16, 20, 4, 100, "Euro5", 5.0f, 2.5f, 3.0f, null);

            _tractorRepositoryMock
                .Setup(repo => repo.CreateTractorAsync(tractorCreate))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<TractorServiceException>(() => _tractorService.CreateTractorAsync(tractorCreate));
            Assert.Equal("Failed to create tractor", exception.Message);
            _tractorRepositoryMock.Verify(repo => repo.CreateTractorAsync(tractorCreate), Times.Once);
        }

        [Fact]
        public async Task GetAllTractorsAsync_ValidFilter_ReturnsListOfTractors()
        {
            // Arrange
            var filter = new TractorFilter(null, null, null);
            var tractors = new List<Tractor>
            {
                new Tractor(Guid.NewGuid(), "ModelX", 2023, "Diesel", "200HP", 16, 20, 4, 100, "Euro5", 5.0f, 2.5f, 3.0f),
                new Tractor(Guid.NewGuid(), "ModelY", 2022, "Gasoline", "180HP", 15, 19, 4, 90, "Euro4", 4.8f, 2.4f, 2.9f)
            };

            _tractorRepositoryMock
                .Setup(repo => repo.GetAllTractorsAsync(filter))
                .ReturnsAsync(tractors);

            // Act
            var result = await _tractorService.GetAllTractorsAsync(filter);

            // Assert
            Assert.Equal(tractors, result);
            _tractorRepositoryMock.Verify(repo => repo.GetAllTractorsAsync(filter), Times.Once);
        }

        [Fact]
        public async Task GetAllTractorsAsync_ThrowsException_ThrowsTractorServiceException()
        {
            // Arrange
            var filter = new TractorFilter(null, null, null);

            _tractorRepositoryMock
                .Setup(repo => repo.GetAllTractorsAsync(filter))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<TractorServiceException>(() => _tractorService.GetAllTractorsAsync(filter));
            Assert.Equal("Failed to get tractors", exception.Message);
            _tractorRepositoryMock.Verify(repo => repo.GetAllTractorsAsync(filter), Times.Once);
        }

        [Fact]
        public async Task GetTractorByIdAsync_ValidId_ReturnsTractor()
        {
            // Arrange
            var tractorId = Guid.NewGuid();
            var tractor = new Tractor(tractorId, "ModelX", 2023, "Diesel", "200HP", 16, 20, 4, 100, "Euro5", 5.0f, 2.5f, 3.0f);

            _tractorRepositoryMock
                .Setup(repo => repo.GetTractorByIdAsync(tractorId))
                .ReturnsAsync(tractor);

            // Act
            var result = await _tractorService.GetTractorByIdAsync(tractorId);

            // Assert
            Assert.Equal(tractor, result);
            _tractorRepositoryMock.Verify(repo => repo.GetTractorByIdAsync(tractorId), Times.Once);
        }

        [Fact]
        public async Task GetTractorByIdAsync_TractorNotFound_ThrowsTractorNotFoundException()
        {
            // Arrange
            var tractorId = Guid.NewGuid();

            _tractorRepositoryMock
                .Setup(repo => repo.GetTractorByIdAsync(tractorId))
                .ReturnsAsync((Tractor)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<TractorServiceException>(() => _tractorService.GetTractorByIdAsync(tractorId));
            _tractorRepositoryMock.Verify(repo => repo.GetTractorByIdAsync(tractorId), Times.Once);

            Assert.IsType<TractorNotFoundException>(exception.InnerException);
        }

        [Fact]
        public async Task GetTractorByIdAsync_ThrowsException_ThrowsTractorServiceException()
        {
            // Arrange
            var tractorId = Guid.NewGuid();

            _tractorRepositoryMock
                .Setup(repo => repo.GetTractorByIdAsync(tractorId))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<TractorServiceException>(() => _tractorService.GetTractorByIdAsync(tractorId));
            Assert.Equal("Failed to get tractor", exception.Message);
            _tractorRepositoryMock.Verify(repo => repo.GetTractorByIdAsync(tractorId), Times.Once);
        }
    }
}
