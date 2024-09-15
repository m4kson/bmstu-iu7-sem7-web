using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ProdMonitor.Application.Services;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.Domain.Interfaces.Services;

namespace ProdMonitor.Test.UnitTests
{
    public class ServiceRequestServiceTests
    {
        private readonly Mock<IServiceRequestRepository> _requestRepositoryMock;
        private readonly Mock<IAssemblyLineRepository> _assemblyLineRepositoryMock;
        private readonly ServiceRequestService _serviceRequestService;

        public ServiceRequestServiceTests()
        {
            _requestRepositoryMock = new Mock<IServiceRequestRepository>();
            _assemblyLineRepositoryMock = new Mock<IAssemblyLineRepository>();
            _serviceRequestService = new ServiceRequestService(_requestRepositoryMock.Object, _assemblyLineRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateServiceRequestAsync_ValidRequest_ReturnsCreatedRequest()
        {
            // Arrange
            var serviceRequestCreate = new ServiceRequestCreate(Guid.NewGuid(), Guid.NewGuid(), RequestType.Inspection, "Test description");
            var assemblyLine = new AssemblyLine(serviceRequestCreate.LineId,
                    name: "Line1",
                    length: 100,
                    height: 50,
                    width: 30,
                    status: LineStatusType.Working,
                    production: 1000,
                    downTime: 10,
                    inspectionsPerYear: 2,
                    lastInspection: new DateOnly(2023, 8, 1),
                    nextInspection: new DateOnly(2024, 8, 1),
                    defectRate: 2);
            var createdRequest = new ServiceRequest(Guid.NewGuid(), serviceRequestCreate.LineId, serviceRequestCreate.UserId, DateTime.Now, RequestStatusType.Opened, serviceRequestCreate.Type, serviceRequestCreate.Description);

            _assemblyLineRepositoryMock
                .Setup(repo => repo.GetAssemblyLineByIdAsync(serviceRequestCreate.LineId))
                .ReturnsAsync(assemblyLine);

            _requestRepositoryMock
                .Setup(repo => repo.CreateServiceRequestAsync(serviceRequestCreate.LineId, serviceRequestCreate.UserId, It.IsAny<DateTime>(), RequestStatusType.Opened, serviceRequestCreate.Type, serviceRequestCreate.Description))
                .ReturnsAsync(createdRequest);

            // Act
            var result = await _serviceRequestService.CreateServiceRequestAsync(serviceRequestCreate);

            // Assert
            Assert.Equal(createdRequest, result);
            _assemblyLineRepositoryMock.Verify(repo => repo.GetAssemblyLineByIdAsync(serviceRequestCreate.LineId), Times.Once);
            _requestRepositoryMock.Verify(repo => repo.CreateServiceRequestAsync(serviceRequestCreate.LineId, serviceRequestCreate.UserId, It.IsAny<DateTime>(), RequestStatusType.Opened, serviceRequestCreate.Type, serviceRequestCreate.Description), Times.Once);
        }

        [Fact]
        public async Task CreateServiceRequestAsync_AssemblyLineNotFound_ThrowsRequestServiceException()
        {
            // Arrange
            var serviceRequestCreate = new ServiceRequestCreate(Guid.NewGuid(), Guid.NewGuid(), RequestType.Repair, "Test description");

            _assemblyLineRepositoryMock
                .Setup(repo => repo.GetAssemblyLineByIdAsync(serviceRequestCreate.LineId))
                .ReturnsAsync((AssemblyLine)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<RequestServiceException>(() => _serviceRequestService.CreateServiceRequestAsync(serviceRequestCreate));
            _assemblyLineRepositoryMock.Verify(repo => repo.GetAssemblyLineByIdAsync(serviceRequestCreate.LineId), Times.Once);
        }

        [Fact]
        public async Task CreateServiceRequestAsync_ThrowsException_ThrowsRequestServiceException()
        {
            // Arrange
            var serviceRequestCreate = new ServiceRequestCreate(Guid.NewGuid(), Guid.NewGuid(), RequestType.Repair, "Test description");
            var assemblyLine = new AssemblyLine(serviceRequestCreate.LineId,
                    name: "Line1",
                    length: 100,
                    height: 50,
                    width: 30,
                    status: LineStatusType.Working,
                    production: 1000,
                    downTime: 10,
                    inspectionsPerYear: 2,
                    lastInspection: new DateOnly(2023, 8, 1),
                    nextInspection: new DateOnly(2024, 8, 1),
                    defectRate: 2);

            _assemblyLineRepositoryMock
                .Setup(repo => repo.GetAssemblyLineByIdAsync(serviceRequestCreate.LineId))
                .ReturnsAsync(assemblyLine);

            _requestRepositoryMock
                .Setup(repo => repo.CreateServiceRequestAsync(serviceRequestCreate.LineId, serviceRequestCreate.UserId, It.IsAny<DateTime>(), RequestStatusType.Opened, serviceRequestCreate.Type, serviceRequestCreate.Description))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<RequestServiceException>(() => _serviceRequestService.CreateServiceRequestAsync(serviceRequestCreate));
            Assert.Equal("Failed to create request", exception.Message);
            _assemblyLineRepositoryMock.Verify(repo => repo.GetAssemblyLineByIdAsync(serviceRequestCreate.LineId), Times.Once);
            _requestRepositoryMock.Verify(repo => repo.CreateServiceRequestAsync(serviceRequestCreate.LineId, serviceRequestCreate.UserId, It.IsAny<DateTime>(), RequestStatusType.Opened, serviceRequestCreate.Type, serviceRequestCreate.Description), Times.Once);
        }

        [Fact]
        public async Task GetAllServiceRequestsAsync_ValidFilter_ReturnsListOfRequests()
        {
            // Arrange
            var filter = new ServiceRequestFilter(null, null, null, null, null);
            var requests = new List<ServiceRequest>
            {
                new ServiceRequest(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, RequestStatusType.Opened, RequestType.Inspection, "Test description"),
                new ServiceRequest(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, RequestStatusType.Closed, RequestType.Repair, "Another description")
            };

            _requestRepositoryMock
                .Setup(repo => repo.GetAllServiceRequestsAsync(filter))
                .ReturnsAsync(requests);

            // Act
            var result = await _serviceRequestService.GetAllServiceRequestsAsync(filter);

            // Assert
            Assert.Equal(requests, result);
            _requestRepositoryMock.Verify(repo => repo.GetAllServiceRequestsAsync(filter), Times.Once);
        }

        [Fact]
        public async Task GetAllServiceRequestsAsync_ThrowsException_ThrowsRequestServiceException()
        {
            // Arrange
            var filter = new ServiceRequestFilter(null, null, null, null, null);

            _requestRepositoryMock
                .Setup(repo => repo.GetAllServiceRequestsAsync(filter))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<RequestServiceException>(() => _serviceRequestService.GetAllServiceRequestsAsync(filter));
            Assert.Equal("Failed to get requests", exception.Message);
            _requestRepositoryMock.Verify(repo => repo.GetAllServiceRequestsAsync(filter), Times.Once);
        }

        [Fact]
        public async Task GetServiceRequestByIdAsync_ValidId_ReturnsRequest()
        {
            // Arrange
            var requestId = Guid.NewGuid();
            var request = new ServiceRequest(requestId, Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, RequestStatusType.Opened, RequestType.Repair, "Test description");

            _requestRepositoryMock
                .Setup(repo => repo.GetServiceRequestByIdAsync(requestId))
                .ReturnsAsync(request);

            // Act
            var result = await _serviceRequestService.GetServiceRequestByIdAsync(requestId);

            // Assert
            Assert.Equal(request, result);
            _requestRepositoryMock.Verify(repo => repo.GetServiceRequestByIdAsync(requestId), Times.Once);
        }

        [Fact]
        public async Task GetServiceRequestByIdAsync_RequestNotFound_ThrowsRequestNotFoundException()
        {
            // Arrange
            var requestId = Guid.NewGuid();

            _requestRepositoryMock
                .Setup(repo => repo.GetServiceRequestByIdAsync(requestId))
                .ReturnsAsync((ServiceRequest)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<RequestServiceException>(() => _serviceRequestService.GetServiceRequestByIdAsync(requestId));
            _requestRepositoryMock.Verify(repo => repo.GetServiceRequestByIdAsync(requestId), Times.Once);

            Assert.IsType<RequestNotFoundException>(exception.InnerException);
        }

        [Fact]
        public async Task GetServiceRequestByIdAsync_ThrowsException_ThrowsRequestServiceException()
        {
            // Arrange
            var requestId = Guid.NewGuid();

            _requestRepositoryMock
                .Setup(repo => repo.GetServiceRequestByIdAsync(requestId))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<RequestServiceException>(() => _serviceRequestService.GetServiceRequestByIdAsync(requestId));
            Assert.Equal("Failed to get request", exception.Message);
            _requestRepositoryMock.Verify(repo => repo.GetServiceRequestByIdAsync(requestId), Times.Once);
        }
    }
}
