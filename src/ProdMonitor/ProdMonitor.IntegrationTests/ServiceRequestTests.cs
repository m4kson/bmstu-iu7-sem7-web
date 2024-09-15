using Xunit;
using FluentAssertions;
using ProdMonitor.Application.Services;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.DataAccess.Models;
using ProdMonitor.DataAccess.Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProdMonitor.IntegrationTests
{
    public class ServiceRequestTests : IClassFixture<ServiceTestSetup>
    {
        private readonly ServiceTestSetup _setup;

        public ServiceRequestTests(ServiceTestSetup setup)
        {
            _setup = setup;
        }

        [Fact]
        public async Task CreateServiceRequestAsync_Should_Create_Request_When_Valid_Request()
        {
            // Arrange
            var lineGuid = Guid.NewGuid();
            var userGuid = Guid.NewGuid();

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

            _setup.DbContext.AssemblyLines.Add(assemblyLineCreate);
            _setup.DbContext.Users.Add(userCreate);
            await _setup.DbContext.SaveChangesAsync();

            var serviceRequestCreate = new ServiceRequestCreate(
                lineId: lineGuid,
                userId: userGuid,
                type: RequestType.Inspection,
                description: "Initial inspection");

            // Act
            var createdRequest = await _setup.ServiceRequestService.CreateServiceRequestAsync(serviceRequestCreate);

            // Assert
            createdRequest.Should().NotBeNull();
            createdRequest.LineId.Should().Be(lineGuid);
            createdRequest.UserId.Should().Be(userGuid);
            createdRequest.Status.Should().Be(RequestStatusType.Opened);

            var updatedLine = await _setup.DbContext.AssemblyLines.FindAsync(lineGuid);
            updatedLine.Status.Should().Be(LineStatusTypeDb.OnService);
        }

        [Fact]
        public async Task CreateServiceRequestAsync_ShouldThrow_When_Line_Not_Found()
        {
            // Arrange
            var serviceRequestCreate = new ServiceRequestCreate(
                lineId: Guid.NewGuid(),
                userId: Guid.NewGuid(),
                type: RequestType.Inspection,
                description: "Invalid line");

            // Act
            Func<Task> act = async () => await _setup.ServiceRequestService.CreateServiceRequestAsync(serviceRequestCreate);

            // Assert
            var exceptionAssertion = await act.Should().ThrowAsync<RequestServiceException>();
            exceptionAssertion.WithMessage("Failed to create request*");

        }

        [Fact]
        public async Task GetAllServiceRequestsAsync_ShouldReturn_All_Requests()
        {
            // Arrange
            var lineGuid = Guid.NewGuid();
            var userGuid = Guid.NewGuid();

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

            _setup.DbContext.AssemblyLines.Add(assemblyLineCreate);
            _setup.DbContext.Users.Add(userCreate);
            await _setup.DbContext.SaveChangesAsync();

            var request1 = new ServiceRequestDb
            {
                Id = Guid.NewGuid(),
                LineId = lineGuid,
                UserId = userGuid,
                RequestDate = DateTime.Now,
                Status = RequestStatusTypeDb.Opened,
                Type = RequestTypeDb.Inspection,
                Description = "Inspection"
            };

            var request2 = new ServiceRequestDb
            {
                Id = Guid.NewGuid(),
                LineId = lineGuid,
                UserId = userGuid,
                RequestDate = DateTime.Now,
                Status = RequestStatusTypeDb.Opened,
                Type = RequestTypeDb.Repair,
                Description = "Repair"
            };

            _setup.DbContext.ServiceRequests.AddRange(request1, request2);
            await _setup.DbContext.SaveChangesAsync();

            var filter = new ServiceRequestFilter();

            // Act
            var result = await _setup.ServiceRequestService.GetAllServiceRequestsAsync(filter);

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetServiceRequestByIdAsync_ShouldReturn_Request_When_Request_Exists()
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
                Description = "Inspection"
            };

            _setup.DbContext.AssemblyLines.Add(assemblyLineCreate);
            _setup.DbContext.Users.Add(userCreate);
            _setup.DbContext.ServiceRequests.Add(request);
            await _setup.DbContext.SaveChangesAsync();

            // Act
            var result = await _setup.ServiceRequestService.GetServiceRequestByIdAsync(requestGuid);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(requestGuid);
        }

        [Fact]
        public async Task GetServiceRequestByIdAsync_ShouldThrow_When_Request_Not_Found()
        {
            // Act
            Func<Task> act = async () => await _setup.ServiceRequestService.GetServiceRequestByIdAsync(Guid.NewGuid());

            // Assert

            var exceptionAssertion = await act.Should().ThrowAsync<RequestServiceException>();
            exceptionAssertion.WithInnerException<RequestNotFoundException>()
                              .WithMessage("*not found*");
        }
    }
}
