using Xunit;
using FluentAssertions;
using ProdMonitor.Application.Services;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProdMonitor.DataAccess.Models;
using ProdMonitor.DataAccess.Models.Enums;
using System.Data;
using System.Xml.Linq;
using System.Reflection.Metadata;

namespace ProdMonitor.IntegrationTests
{
    public class ServiceReportTests : IClassFixture<ServiceTestSetup>
    {
        private readonly ServiceTestSetup _setup;

        public ServiceReportTests(ServiceTestSetup setup)
        {
            _setup = setup;
        }

        [Fact]
        public async Task CreateServiceReportAsync_Should_Create_Report_When_Valid_Request()
        {
            // Arrange
            var lineGuid = Guid.NewGuid();
            var userGuid = Guid.NewGuid();
            var requestGuid = Guid.NewGuid();

            var assemblyLineCreate = new AssemblyLineDb(
                id: lineGuid,
                name: "Line1",
                length: 100,
                height: 50,
                width: 30,
                status: LineStatusTypeDb.Working,
                production: 1000,
                downTime: 10,
                inspectionsPerYear: 2,
                lastInspection: new DateOnly(2023, 8, 1),
                nextInspection: new DateOnly(2024, 8, 1),
                defectRate: 2);

            var userCreate = new UserDb(
                userGuid,
                "Jane",
                "Doe",
                "M.",
                "HR",
                "jane.doe@example.com",
                new byte[0],
                new byte[0],
                new DateOnly(1985, 5, 10),
                SexTypeDb.Male,
                RoleTypeDb.Admin);

            var request = new ServiceRequestDb
            {
                Id = requestGuid,
                LineId = lineGuid,
                UserId = userGuid,
                RequestDate = DateTime.Now,
                Status = RequestStatusTypeDb.Opened,
                Type = RequestTypeDb.Inspection,
                Description = "test"
            };

            _setup.DbContext.AssemblyLines.Add(assemblyLineCreate);
            _setup.DbContext.Users.Add(userCreate);
            _setup.DbContext.ServiceRequests.Add(request);
            await _setup.DbContext.SaveChangesAsync();

            var savedLine = await _setup.DbContext.AssemblyLines.FindAsync(lineGuid);
            var savedUser = await _setup.DbContext.Users.FindAsync(userGuid);
            var savedRequest = await _setup.DbContext.ServiceRequests.FindAsync(requestGuid);

            Assert.NotNull(savedLine);
            Assert.NotNull(savedUser);
            Assert.NotNull(savedRequest);

            var reportCreate = new ServiceReportCreate
            (
                userId: userGuid,
                requestId: request.Id
            );

            // Act
            var createdReport = await _setup.ServiceReportService.CreateServiceReportAsync(reportCreate);

            // Assert
            createdReport.Should().NotBeNull();
            createdReport.RequestId.Should().Be(request.Id);
            var updatedRequest = await _setup.DbContext.ServiceRequests.FindAsync(request.Id);
            updatedRequest.Status.Should().Be(RequestStatusTypeDb.InProgress);
        }

        [Fact]
        public async Task GetAllServiceReportsAsync_ShouldReturnAllReports()
        {
            // Arrange
            var lineGuid = Guid.NewGuid();
            var userGuid = Guid.NewGuid();
            var requestGuid = Guid.NewGuid();
            var request2Guid = Guid.NewGuid();

            var assemblyLineCreate = new AssemblyLineDb(
                id: lineGuid,
                name: "Line1",
                length: 100,
                height: 50,
                width: 30,
                status: LineStatusTypeDb.Working,
                production: 1000,
                downTime: 10,
                inspectionsPerYear: 2,
                lastInspection: new DateOnly(2023, 8, 1),
                nextInspection: new DateOnly(2024, 8, 1),
                defectRate: 2);

            var userCreate = new UserDb(
                userGuid,
                "Jane",
                "Doe",
                "M.",
                "HR",
                "jane.doe@example.com",
                new byte[0],
                new byte[0],
                new DateOnly(1985, 5, 10),
                SexTypeDb.Male,
                RoleTypeDb.Admin);

            var request = new ServiceRequestDb
            {
                Id = requestGuid,
                LineId = lineGuid,
                UserId = userGuid,
                RequestDate = DateTime.Now,
                Status = RequestStatusTypeDb.Opened,
                Type = RequestTypeDb.Inspection,
                Description = "test"
            };

            var request2 = new ServiceRequestDb
            {
                Id = request2Guid,
                LineId = lineGuid,
                UserId = userGuid,
                RequestDate = DateTime.Now,
                Status = RequestStatusTypeDb.Opened,
                Type = RequestTypeDb.Repair,
                Description = "test2"
            };

            _setup.DbContext.AssemblyLines.Add(assemblyLineCreate);
            _setup.DbContext.Users.Add(userCreate);
            _setup.DbContext.ServiceRequests.Add(request);
            _setup.DbContext.ServiceRequests.Add(request2);
            await _setup.DbContext.SaveChangesAsync();

            var report1 = new ServiceReportDb
            {
                Id = Guid.NewGuid(),
                RequestId = requestGuid,
                UserId = userGuid,
                LineId = lineGuid,
                OpenDate = DateTime.Now
            };

            var report2 = new ServiceReportDb
            {
                Id = Guid.NewGuid(),
                RequestId = request2Guid,
                UserId = userGuid,
                LineId = lineGuid,
                OpenDate = DateTime.Now
            };

            _setup.DbContext.ServiceReports.AddRange(report1, report2);
            await _setup.DbContext.SaveChangesAsync();

            var filter = new ServiceReportFilter();

            // Act
            var result = await _setup.ServiceReportService.GetAllServiceReportsAsync(filter);

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetServiceReportByIdAsync_ShouldReturnReport_WhenReportExists()
        {
            // Arrange
            var lineGuid = Guid.NewGuid();
            var userGuid = Guid.NewGuid();
            var requestGuid = Guid.NewGuid();

            var assemblyLineCreate = new AssemblyLineDb(
                id: lineGuid,
                name: "Line1",
                length: 100,
                height: 50,
                width: 30,
                status: LineStatusTypeDb.Working,
                production: 1000,
                downTime: 10,
                inspectionsPerYear: 2,
                lastInspection: new DateOnly(2023, 8, 1),
                nextInspection: new DateOnly(2024, 8, 1),
                defectRate: 2);

            var userCreate = new UserDb(
                userGuid,
                "Jane",
                "Doe",
                "M.",
                "HR",
                "jane.doe@example.com",
                new byte[0],
                new byte[0],
                new DateOnly(1985, 5, 10),
                SexTypeDb.Male,
                RoleTypeDb.Admin);

            var request = new ServiceRequestDb
            {
                Id = requestGuid,
                LineId = lineGuid,
                UserId = userGuid,
                RequestDate = DateTime.Now,
                Status = RequestStatusTypeDb.Opened,
                Type = RequestTypeDb.Inspection,
                Description = "test"
            };

            _setup.DbContext.AssemblyLines.Add(assemblyLineCreate);
            _setup.DbContext.Users.Add(userCreate);
            _setup.DbContext.ServiceRequests.Add(request);
            await _setup.DbContext.SaveChangesAsync();

            var report = new ServiceReportDb
            {
                Id = Guid.NewGuid(),
                RequestId = requestGuid,
                UserId = userGuid,
                LineId = lineGuid,
                OpenDate = DateTime.Now
            };

            _setup.DbContext.ServiceReports.Add(report);
            await _setup.DbContext.SaveChangesAsync();

            // Act
            var result = await _setup.ServiceReportService.GetServiceReportByIdAsync(report.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(report.Id);
        }

        [Fact]
        public async Task CloseServiceReportAsync_Should_Update_Status_And_Downtime_When_Valid_Report()
        {
            // Arrange
            var lineGuid = Guid.NewGuid();
            var userGuid = Guid.NewGuid();
            var requestGuid = Guid.NewGuid();

            var assemblyLineCreate = new AssemblyLineDb(
                id: lineGuid,
                name: "Line1",
                length: 100,
                height: 50,
                width: 30,
                status: LineStatusTypeDb.Working,
                production: 1000,
                downTime: 0,
                inspectionsPerYear: 2,
                lastInspection: new DateOnly(2023, 8, 1),
                nextInspection: new DateOnly(2024, 8, 1),
                defectRate: 2);

            var userCreate = new UserDb(
                userGuid,
                "Jane",
                "Doe",
                "M.",
                "HR",
                "jane.doe@example.com",
                new byte[0],
                new byte[0],
                new DateOnly(1985, 5, 10),
                SexTypeDb.Male,
                RoleTypeDb.Admin);

            var request = new ServiceRequestDb
            {
                Id = requestGuid,
                LineId = lineGuid,
                UserId = userGuid,
                RequestDate = DateTime.Now.AddHours(-5),
                Status = RequestStatusTypeDb.Opened,
                Type = RequestTypeDb.Inspection,
                Description = "test"
            };

            _setup.DbContext.AssemblyLines.Add(assemblyLineCreate);
            _setup.DbContext.Users.Add(userCreate);
            _setup.DbContext.ServiceRequests.Add(request);
            await _setup.DbContext.SaveChangesAsync();


            var report = new ServiceReportDb
            {
                Id = Guid.NewGuid(),
                RequestId = requestGuid,
                UserId = userGuid,
                LineId = lineGuid,
                OpenDate = DateTime.Now.AddHours(-4) // Отчет создан через час после заявки
            };


            _setup.DbContext.ServiceReports.Add(report);
            await _setup.DbContext.SaveChangesAsync();

            var reportClose = new ServiceReportClose
            (
                price: 5000,
                description: "Repair completed"
            );

            // Act
            var closedReport = await _setup.ServiceReportService.CloseServiceReportAsync(report.Id, reportClose);

            // Assert
            closedReport.Should().NotBeNull();
            closedReport.Price.Should().Be(5000);
            closedReport.Description.Should().Be("Repair completed");

            var updatedLine = await _setup.DbContext.AssemblyLines.FindAsync(lineGuid);
            updatedLine.DownTime.Should().Be(5); // 5 часов простоя
            updatedLine.Status.Should().Be(LineStatusTypeDb.Working);
            updatedLine.LastInspection.Should().Be(DateOnly.FromDateTime(closedReport.CloseDate.Value));
            var newNextInspectionDate = updatedLine.LastInspection.AddMonths(12 / updatedLine.InspectionsPerYear);
            updatedLine.NextInspection.Should().Be(newNextInspectionDate);
            var updatedRequest = await _setup.DbContext.ServiceRequests.FindAsync(requestGuid);
            updatedRequest.Status.Should().Be(RequestStatusTypeDb.Closed);

        }

        [Fact]
        public async Task CloseServiceReportAsync_With_Repair_Should_Update_Status_And_Downtime_When_Valid_Report()
        {
            // Arrange
            var lineGuid = Guid.NewGuid();
            var userGuid = Guid.NewGuid();
            var requestGuid = Guid.NewGuid();

            var assemblyLineCreate = new AssemblyLineDb(
                id: lineGuid,
                name: "Line1",
                length: 100,
                height: 50,
                width: 30,
                status: LineStatusTypeDb.Working,
                production: 1000,
                downTime: 0,
                inspectionsPerYear: 2,
                lastInspection: new DateOnly(2023, 8, 1),
                nextInspection: new DateOnly(2024, 8, 1),
                defectRate: 2);

            var userCreate = new UserDb(
                userGuid,
                "Jane",
                "Doe",
                "M.",
                "HR",
                "jane.doe@example.com",
                new byte[0],
                new byte[0],
                new DateOnly(1985, 5, 10),
                SexTypeDb.Male,
                RoleTypeDb.Admin);

            var request = new ServiceRequestDb
            {
                Id = requestGuid,
                LineId = lineGuid,
                UserId = userGuid,
                RequestDate = DateTime.Now.AddHours(-5),
                Status = RequestStatusTypeDb.Opened,
                Type = RequestTypeDb.Repair,
                Description = "test"
            };

            _setup.DbContext.AssemblyLines.Add(assemblyLineCreate);
            _setup.DbContext.Users.Add(userCreate);
            _setup.DbContext.ServiceRequests.Add(request);
            await _setup.DbContext.SaveChangesAsync();


            var report = new ServiceReportDb
            {
                Id = Guid.NewGuid(),
                RequestId = requestGuid,
                UserId = userGuid,
                LineId = lineGuid,
                OpenDate = DateTime.Now.AddHours(-4) // Отчет создан через час после заявки
            };


            _setup.DbContext.ServiceReports.Add(report);
            await _setup.DbContext.SaveChangesAsync();

            var reportClose = new ServiceReportClose
            (
                price: 5000,
                description: "Repair completed"
            );

            // Act
            var closedReport = await _setup.ServiceReportService.CloseServiceReportAsync(report.Id, reportClose);

            // Assert
            closedReport.Should().NotBeNull();
            closedReport.Price.Should().Be(5000);
            closedReport.Description.Should().Be("Repair completed");

            var updatedLine = await _setup.DbContext.AssemblyLines.FindAsync(lineGuid);
            updatedLine.DownTime.Should().Be(5); // 5 часов простоя
            updatedLine.Status.Should().Be(LineStatusTypeDb.Working);
            updatedLine.LastInspection.Should().Be(assemblyLineCreate.LastInspection);
            updatedLine.NextInspection.Should().Be(assemblyLineCreate.NextInspection);
            var updatedRequest = await _setup.DbContext.ServiceRequests.FindAsync(requestGuid);
            updatedRequest.Status.Should().Be(RequestStatusTypeDb.Closed);

        }

        [Fact]
        public async Task CreateServiceReportAsync_ShouldThrow_When_RequestNotFound()
        {
            // Arrange
            var reportCreate = new ServiceReportCreate
            (
                requestId: Guid.NewGuid(),
                userId: Guid.NewGuid()
            );

            // Act
            Func<Task> act = async () => await _setup.ServiceReportService.CreateServiceReportAsync(reportCreate);

            // Assert
            var exceptionAssertion = await act.Should().ThrowAsync<ReportServiceException>();
            exceptionAssertion.WithInnerException<ArgumentException>()
                              .WithMessage("*not found*");
        }

        [Fact]
        public async Task CreateServiceReportAsync_ShouldThrow_When_Request_Not_Opened()
        {
            // Arrange
            var lineGuid = Guid.NewGuid();
            var userGuid = Guid.NewGuid();
            var requestGuid = Guid.NewGuid();

            var assemblyLineCreate = new AssemblyLineDb(
                id: lineGuid,
                name: "Line1",
                length: 100,
                height: 50,
                width: 30,
                status: LineStatusTypeDb.Working,
                production: 1000,
                downTime: 10,
                inspectionsPerYear: 2,
                lastInspection: new DateOnly(2023, 8, 1),
                nextInspection: new DateOnly(2024, 8, 1),
                defectRate: 2);

            var userCreate = new UserDb(
                userGuid,
                "Jane",
                "Doe",
                "M.",
                "HR",
                "jane.doe@example.com",
                new byte[0],
                new byte[0],
                new DateOnly(1985, 5, 10),
                SexTypeDb.Male,
                RoleTypeDb.Admin);

            var request = new ServiceRequestDb
            {
                Id = requestGuid,
                LineId = lineGuid,
                UserId = userGuid,
                RequestDate = DateTime.Now,
                Status = RequestStatusTypeDb.Closed,
                Type = RequestTypeDb.Inspection,
                Description = "test"
            };

            _setup.DbContext.AssemblyLines.Add(assemblyLineCreate);
            _setup.DbContext.Users.Add(userCreate);
            _setup.DbContext.ServiceRequests.Add(request);
            await _setup.DbContext.SaveChangesAsync();

            var savedLine = await _setup.DbContext.AssemblyLines.FindAsync(lineGuid);
            var savedUser = await _setup.DbContext.Users.FindAsync(userGuid);
            var savedRequest = await _setup.DbContext.ServiceRequests.FindAsync(requestGuid);

            Assert.NotNull(savedLine);
            Assert.NotNull(savedUser);
            Assert.NotNull(savedRequest);

            var reportCreate = new ServiceReportCreate
            (
                userId: userGuid,
                requestId: request.Id
            );

            // Act
            Func<Task> act = async () => await _setup.ServiceReportService.CreateServiceReportAsync(reportCreate);

            // Assert
            var exceptionAssertion = await act.Should().ThrowAsync<ReportServiceException>();
            exceptionAssertion.WithInnerException<ArgumentException>()
                              .WithMessage("*not opened*");
        }
    }
}
