using ProdMonitor.DataAccess.Models;
using ProdMonitor.DataAccess.Models.Enums;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Test.UnitTests.RepositoryTest;

public class ServiceReportRepositoryTests : IClassFixture<RepositoryTestsSetup>
{
    private readonly RepositoryTestsSetup _setup;
    
    public ServiceReportRepositoryTests(RepositoryTestsSetup setup)
    {
        _setup = setup;
    }
    
    [Fact]
    public async Task CreateServiceReportAsync_ShouldCreateServiceReport()
    {
        _setup.ResetContext();

        // Arrange
        var lineId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var requestId = Guid.NewGuid();

        // Act
        var newServiceReport = await _setup.ServiceReportRepository.CreateServiceReportAsync(
            lineId,
            userId,
            requestId,
            DateTime.Now);

        // Assert
        var result = await _setup.Context.ServiceReports.FindAsync(newServiceReport.Id);
        Assert.NotNull(result);
        Assert.Equal(newServiceReport.Id, result.Id);
    }
    
    [Fact]
    public async Task CreateServiceReportAsync_ShouldThrowException_WhenServiceReportIsNull()
    {
        _setup.ResetContext();
        
        // Arrange
        ServiceReportCreate serviceReport = null;

        // Act
        async Task Act() => await _setup.ServiceReportRepository.CreateServiceReportAsync(
            Guid.NewGuid(),
            serviceReport.UserId,
            serviceReport.RequestId,
            DateTime.Now);

        // Assert
        await Assert.ThrowsAsync<NullReferenceException>(Act);
    }
    
    [Fact]
    public async Task GetAllServiceReportsAsync_ShouldReturnAllServiceReports()
    {
        _setup.ResetContext();
        
        // Arrange
        var serviceReport = new ServiceReportDb(
            );

        // Act
        var newServiceReport = await _setup.ServiceReportRepository.CreateServiceReportAsync(
            Guid.NewGuid(),
            serviceReport.UserId,
            serviceReport.RequestId,
            DateTime.Now);

        var serviceReportFilter = new ServiceReportFilter
        {
            LineId = newServiceReport.LineId,
            UserId = newServiceReport.UserId,
            RequestId = newServiceReport.RequestId
        };

        var result = await _setup.ServiceReportRepository.GetAllServiceReportsAsync(serviceReportFilter);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }
    
    [Fact]
    public async Task GetAllServiceReportsAsync_ShouldReturnEmptyList_WhenServiceReportNotFound()
    {
        _setup.ResetContext();
        
        // Arrange
        var serviceReportFilter = new ServiceReportFilter
        {
            LineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            RequestId = Guid.NewGuid()
        };

        // Act
        var result = await _setup.ServiceReportRepository.GetAllServiceReportsAsync(serviceReportFilter);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task GetServiceReportByIdAsync_ShouldReturnServiceReport()
    {
        _setup.ResetContext();
        
        // Arrange
        var serviceReport = new ServiceReportCreate(
            Guid.NewGuid(),
            Guid.NewGuid());

        // Act
        var newServiceReport = await _setup.ServiceReportRepository.CreateServiceReportAsync(
            Guid.NewGuid(),
            serviceReport.UserId,
            serviceReport.RequestId,
            DateTime.Now);

        var result = await _setup.ServiceReportRepository.GetServiceReportByIdAsync(newServiceReport.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newServiceReport.Id, result!.Id);
    }
    
    [Fact]
    public async Task GetServiceReportByIdAsync_ShouldReturnNull_WhenServiceReportNotFound()
    {
        _setup.ResetContext();
        
        // Arrange

        // Act
        var result = await _setup.ServiceReportRepository.GetServiceReportByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task CloseServiceReportAsync_ShouldCloseServiceReport()
    {
        _setup.ResetContext();

        // Arrange
        var serviceRequest = new ServiceRequestDb
        {
            Id = Guid.NewGuid(),
            LineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            RequestDate = DateTime.Now,
            Status = RequestStatusTypeDb.Closed,
            Type = RequestTypeDb.Inspection,
            Description = "Description"
        };
        _setup.Context.ServiceRequests.Add(serviceRequest);
        await _setup.Context.SaveChangesAsync();

        var serviceReport = new ServiceReportDb
        {
            Id = Guid.NewGuid(),
            LineId = serviceRequest.LineId,
            UserId = serviceRequest.UserId,
            RequestId = serviceRequest.Id,
            OpenDate = DateTime.Now
        };
        _setup.Context.ServiceReports.Add(serviceReport);
        await _setup.Context.SaveChangesAsync();

        // Act
        var result = await _setup.ServiceReportRepository.CloseServiceReportAsync(
            serviceReport.Id,
            DateTime.Now,
            100,
            "Description");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(serviceReport.Id, result.Id);
        Assert.Equal(100, result.Price);
        Assert.Equal("Description", result.Description);
        Assert.NotNull(result.CloseDate);
    }
    
    [Fact]
    public async Task CloseServiceReportAsync_ShouldThrowException_WhenServiceReportNotFound()
    {
        _setup.ResetContext();
        
        // Arrange

        // Act
        async Task Act() => await _setup.ServiceReportRepository.CloseServiceReportAsync(
            Guid.NewGuid(),
            DateTime.Now,
            100,
            "Description");

        // Assert
        await Assert.ThrowsAsync<ReportNotFoundException>(Act);
    }
}