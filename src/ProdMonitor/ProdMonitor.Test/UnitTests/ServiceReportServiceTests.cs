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
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;

namespace ProdMonitor.Test.UnitTests
{
    public class ServiceReportServiceTests
    {
        private readonly Mock<IServiceReportRepository> _serviceReportRepositoryMock;
        private readonly Mock<IServiceRequestRepository> _serviceRequestRepositoryMock;
        private readonly ServiceReportService _serviceReportService;
        private readonly Mock<IAssemblyLineRepository> _assemblyLineRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public ServiceReportServiceTests()
        {
            _serviceReportRepositoryMock = new Mock<IServiceReportRepository>();
            _serviceRequestRepositoryMock = new Mock<IServiceRequestRepository>();
            _assemblyLineRepositoryMock = new Mock<IAssemblyLineRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _serviceReportService = new ServiceReportService(_serviceReportRepositoryMock.Object, _serviceRequestRepositoryMock.Object, _assemblyLineRepositoryMock.Object, _userRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateServiceReportAsync_ValidReport_ReturnsCreatedServiceReport()
        {
            var userId = Guid.NewGuid();
            var lineId = Guid.NewGuid();
            var requestId = Guid.NewGuid();

            // Arrange
            var serviceReportCreate = new ServiceReportCreate(userId, requestId);

            var serviceRequest = new ServiceRequest(id: requestId,
                lineId: lineId,
                userId: userId,
                requestDate: DateTime.Now,
                status: RequestStatusType.Opened,
                type: RequestType.Inspection,
                description: "test");

            var assemblyLine = new AssemblyLine(
                Guid.NewGuid(),
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

            var user = new User(Guid.NewGuid(), 
                "John", 
                "Doe", 
                "M.", 
                "IT", 
                "john.doe@example.com", 
                new byte[0], 
                new byte[0], 
                new DateOnly(1990, 1, 1), 
                SexType.Male, 
                RoleType.Admin);

            var createdServiceReport = new ServiceReport(Guid.NewGuid(),
                serviceRequest.LineId,
                serviceReportCreate.UserId,
                serviceReportCreate.RequestId,
                DateTime.Now,
                DateTime.Now.AddHours(2),
                100f,
                "Test description");

            _assemblyLineRepositoryMock
                .Setup(repo => repo.GetAssemblyLineByIdAsync(lineId))
                .ReturnsAsync(assemblyLine);

            _userRepositoryMock
                .Setup(repo => repo.GetUserByIdAsync(userId))
                .ReturnsAsync(user);

            _serviceRequestRepositoryMock
                .Setup(repo => repo.GetServiceRequestByIdAsync(serviceReportCreate.RequestId))
                .ReturnsAsync(serviceRequest);

            _serviceReportRepositoryMock
                .Setup(repo => repo.CreateServiceReportAsync(serviceRequest.LineId,
                    serviceReportCreate.UserId,
                    serviceReportCreate.RequestId,
                    It.IsAny<DateTime>()))
                .ReturnsAsync(createdServiceReport);

            // Act
            var result = await _serviceReportService.CreateServiceReportAsync(serviceReportCreate);

            // Assert
            Assert.Equal(createdServiceReport, result);
            _serviceRequestRepositoryMock.Verify(repo => repo.GetServiceRequestByIdAsync(serviceReportCreate.RequestId), Times.Once);
            _assemblyLineRepositoryMock.Verify(repo => repo.GetAssemblyLineByIdAsync(lineId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetUserByIdAsync(userId), Times.Once);
            _serviceReportRepositoryMock.Verify(repo => repo.CreateServiceReportAsync(serviceRequest.LineId, serviceReportCreate.UserId, serviceReportCreate.RequestId, It.IsAny<DateTime>()), Times.Once);
        }



        [Fact]
        public async Task CreateServiceReportAsync_RequestNotFound_ThrowsArgumentException()
        {
            // Arrange
            var serviceReportCreate = new ServiceReportCreate(Guid.NewGuid(), Guid.NewGuid());

            _serviceRequestRepositoryMock
                .Setup(repo => repo.GetServiceRequestByIdAsync(serviceReportCreate.RequestId))
                .ReturnsAsync((ServiceRequest)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ReportServiceException>(() => _serviceReportService.CreateServiceReportAsync(serviceReportCreate));
            _serviceRequestRepositoryMock.Verify(repo => repo.GetServiceRequestByIdAsync(serviceReportCreate.RequestId), Times.Once);

            Assert.IsType<ArgumentException>(exception.InnerException);

        }

        [Fact]
        public async Task CreateServiceReportAsync_RequestNotOpened_ThrowsArgumentException()
        {
            var userId = Guid.NewGuid();
            var lineId = Guid.NewGuid();
            var requestId = Guid.NewGuid();
            // Arrange
            var serviceReportCreate = new ServiceReportCreate(userId, requestId);
            var serviceRequest = new ServiceRequest(id: requestId,
                lineId: lineId,
                userId: userId,
                requestDate: DateTime.Now,
                status: RequestStatusType.InProgress,
                type: RequestType.Inspection,
                description: "test");

            _serviceRequestRepositoryMock
                .Setup(repo => repo.GetServiceRequestByIdAsync(serviceReportCreate.RequestId))
                .ReturnsAsync(serviceRequest);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ReportServiceException>(() => _serviceReportService.CreateServiceReportAsync(serviceReportCreate));
            _serviceRequestRepositoryMock.Verify(repo => repo.GetServiceRequestByIdAsync(serviceReportCreate.RequestId), Times.Once);

            Assert.IsType<ArgumentException>(exception.InnerException);
        }

        [Fact]
        public async Task CreateServiceReportAsync_ThrowsException_ThrowsReportServiceException()
        {
            var userId = Guid.NewGuid();
            var lineId = Guid.NewGuid();
            var requestId = Guid.NewGuid();

            // Arrange
            var serviceReportCreate = new ServiceReportCreate(userId, requestId);

            var serviceRequest = new ServiceRequest(id: requestId,
                lineId: lineId,
                userId: userId,
                requestDate: DateTime.Now,
                status: RequestStatusType.Opened,
                type: RequestType.Inspection,
                description: "test");

            _serviceRequestRepositoryMock
                .Setup(repo => repo.GetServiceRequestByIdAsync(serviceReportCreate.RequestId))
                .ReturnsAsync(serviceRequest);

            _serviceReportRepositoryMock
                .Setup(repo => repo.CreateServiceReportAsync(serviceRequest.LineId, serviceReportCreate.UserId, serviceReportCreate.RequestId, It.IsAny<DateTime>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ReportServiceException>(() => _serviceReportService.CreateServiceReportAsync(serviceReportCreate));

            Assert.Equal("Failed to create report", exception.Message);

            _serviceRequestRepositoryMock.Verify(repo => repo.GetServiceRequestByIdAsync(serviceReportCreate.RequestId), Times.Once);

        }

        [Fact]
        public async Task GetAllServiceReportsAsync_ValidFilter_ReturnsListOfReports()
        {
            // Arrange
            var filter = new ServiceReportFilter(null, null, null, null);
            var reports = new List<ServiceReport>
            {
                new ServiceReport(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, DateTime.Now.AddHours(2), 100f, "Test description"),
                new ServiceReport(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, DateTime.Now.AddHours(2), 150f, "Another description")
            };

            _serviceReportRepositoryMock
                .Setup(repo => repo.GetAllServiceReportsAsync(filter))
                .ReturnsAsync(reports);

            // Act
            var result = await _serviceReportService.GetAllServiceReportsAsync(filter);

            // Assert
            Assert.Equal(reports, result);
            _serviceReportRepositoryMock.Verify(repo => repo.GetAllServiceReportsAsync(filter), Times.Once);
        }

        [Fact]
        public async Task GetAllServiceReportsAsync_ThrowsException_ThrowsReportServiceException()
        {
            // Arrange
            var filter = new ServiceReportFilter(null, null, null, null);

            _serviceReportRepositoryMock
                .Setup(repo => repo.GetAllServiceReportsAsync(filter))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ReportServiceException>(() => _serviceReportService.GetAllServiceReportsAsync(filter));
            Assert.Equal("Failed to get reports", exception.Message);
            _serviceReportRepositoryMock.Verify(repo => repo.GetAllServiceReportsAsync(filter), Times.Once);
        }

        [Fact]
        public async Task GetServiceReportByIdAsync_ValidId_ReturnsReport()
        {
            // Arrange
            var reportId = Guid.NewGuid();
            var report = new ServiceReport(reportId, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, DateTime.Now.AddHours(2), 100f, "Test description");

            _serviceReportRepositoryMock
                .Setup(repo => repo.GetServiceReportByIdAsync(reportId))
                .ReturnsAsync(report);

            // Act
            var result = await _serviceReportService.GetServiceReportByIdAsync(reportId);

            // Assert
            Assert.Equal(report, result);
            _serviceReportRepositoryMock.Verify(repo => repo.GetServiceReportByIdAsync(reportId), Times.Once);
        }

        [Fact]
        public async Task GetServiceReportByIdAsync_ReportNotFound_ThrowsReportNotFoundException()
        {
            // Arrange
            var reportId = Guid.NewGuid();

            _serviceReportRepositoryMock
                .Setup(repo => repo.GetServiceReportByIdAsync(reportId))
                .ReturnsAsync((ServiceReport)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ReportServiceException>(() => _serviceReportService.GetServiceReportByIdAsync(reportId));
            _serviceReportRepositoryMock.Verify(repo => repo.GetServiceReportByIdAsync(reportId), Times.Once);

            Assert.IsType<ReportNotFoundException>(exception.InnerException);
        }

        [Fact]
        public async Task GetServiceReportByIdAsync_ThrowsException_ThrowsReportServiceException()
        {
            // Arrange
            var reportId = Guid.NewGuid();

            _serviceReportRepositoryMock
                .Setup(repo => repo.GetServiceReportByIdAsync(reportId))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ReportServiceException>(() => _serviceReportService.GetServiceReportByIdAsync(reportId));
            Assert.Equal("Failed to get report", exception.Message);
            _serviceReportRepositoryMock.Verify(repo => repo.GetServiceReportByIdAsync(reportId), Times.Once);
        }

        [Fact]
        public async Task CloseServiceReportAsync_ValidRequest_ReturnsClosedReport()
        {
            // Arrange
            var reportId = Guid.NewGuid();
            var lineId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var requestId = Guid.NewGuid();
            var serviceReportClose = new ServiceReportClose(100f, "Test description");

            var serviceRequest = new ServiceRequest(requestId, lineId, userId, DateTime.Now, RequestStatusType.Opened, RequestType.Inspection, "Test request");

            _serviceRequestRepositoryMock
                .Setup(repo => repo.UpdateServiceRequestAsync(requestId, It.IsAny<ServiceRequestUpdate>()))
                .ReturnsAsync(serviceRequest);

            var report = new ServiceReport(reportId, lineId, userId, requestId, DateTime.Now.AddHours(-2), DateTime.Now, 0f, null);

            _serviceReportRepositoryMock
                .Setup(repo => repo.GetServiceReportByIdAsync(reportId))
                .ReturnsAsync(report);

            var closedReport = new ServiceReport(reportId, lineId, userId, requestId, report.OpenDate, DateTime.Now, serviceReportClose.Price, serviceReportClose.Description);

            _serviceReportRepositoryMock
                .Setup(repo => repo.CloseServiceReportAsync(reportId, It.IsAny<DateTime>(), serviceReportClose.Price, serviceReportClose.Description))
                .ReturnsAsync(closedReport);

            var assemblyLine = new AssemblyLine(Guid.NewGuid(),
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
                .Setup(repo => repo.GetAssemblyLineByIdAsync(lineId))
                .ReturnsAsync(assemblyLine); 

            // Act
            var result = await _serviceReportService.CloseServiceReportAsync(reportId, serviceReportClose);

            // Assert
            Assert.Equal(closedReport, result);
            _serviceRequestRepositoryMock.Verify(repo => repo.UpdateServiceRequestAsync(requestId, It.IsAny<ServiceRequestUpdate>()), Times.Once);
            _serviceReportRepositoryMock.Verify(repo => repo.CloseServiceReportAsync(reportId, It.IsAny<DateTime>(), serviceReportClose.Price, serviceReportClose.Description), Times.Once);
            _assemblyLineRepositoryMock.Verify(repo => repo.GetAssemblyLineByIdAsync(lineId), Times.Once);
        }

        [Fact]
        public async Task CloseServiceReportAsync_ThrowsException_ThrowsReportServiceException()
        {
            // Arrange
            var reportId = Guid.NewGuid();
            var serviceReportClose = new ServiceReportClose(100f, "Test description");

            _serviceReportRepositoryMock
                .Setup(repo => repo.CloseServiceReportAsync(reportId, It.IsAny<DateTime>(), serviceReportClose.Price, serviceReportClose.Description))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ReportServiceException>(() => _serviceReportService.CloseServiceReportAsync(reportId, serviceReportClose));
            Assert.Equal("Failed to get report", exception.Message);
            _serviceReportRepositoryMock.Verify(repo => repo.CloseServiceReportAsync(reportId, It.IsAny<DateTime>(), serviceReportClose.Price, serviceReportClose.Description), Times.Once);
        }
    }
}
