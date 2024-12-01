using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Test.UnitTests.RepositoryTest;

public class ServiceRequestRepositoryTests : IClassFixture<RepositoryTestsSetup>
{
    private readonly RepositoryTestsSetup _setup;
    
    public ServiceRequestRepositoryTests(RepositoryTestsSetup setup)
    {
        _setup = setup;
    }
    
    [Fact]
    public async Task CreateServiceRequestAsync_ShouldCreateServiceRequest()
    {
        _setup.ResetContext();
        
        // Arrange
        var serviceRequest = new ServiceRequestCreate(
            lineId: Guid.NewGuid(),
            userId: Guid.NewGuid(),
            type: RequestType.Inspection,
            description: "Test description");
        
        // Act
        var newRequest = await _setup.ServiceRequestRepository.CreateServiceRequestAsync(
            serviceRequest.LineId,
            serviceRequest.UserId,
            DateTime.Now, 
            RequestStatusType.Opened,
            serviceRequest.Type,
            serviceRequest.Description);
        
        // Assert
        var result = await _setup.Context.ServiceRequests.FindAsync(newRequest.Id);
        Assert.NotNull(result);
        Assert.Equal(newRequest.Id, result.Id);
    }

    [Fact]
    public async Task CreateServiceRequestAsync_ShouldThrowException_WhenLineIdIsInvalid()
    {
        _setup.ResetContext();
        
        // Arrange
        var serviceRequest = new ServiceRequestCreate(
            lineId: Guid.Empty,
            userId: Guid.NewGuid(),
            type: RequestType.Inspection,
            description: "Test description");
        
        // Act
        async Task Act() => await _setup.ServiceRequestRepository.CreateServiceRequestAsync(
            serviceRequest.LineId,
            serviceRequest.UserId,
            DateTime.Now, 
            RequestStatusType.Opened,
            serviceRequest.Type,
            serviceRequest.Description);
        
        // Assert
        await Assert.ThrowsAsync<ServiceRequestRepositoryException>(Act);
    }
    
    [Fact]
    public async Task GetAllServiceRequestsAsync_ShouldReturnAllServiceRequests()
    {
        _setup.ResetContext();
        
        // Arrange
        var serviceRequest1 = new ServiceRequestCreate(
            lineId: Guid.NewGuid(),
            userId: Guid.NewGuid(),
            type: RequestType.Inspection,
            description: "Test description 1");
        var serviceRequest2 = new ServiceRequestCreate(
            lineId: Guid.NewGuid(),
            userId: Guid.NewGuid(),
            type: RequestType.Repair,
            description: "Test description 2");
        var serviceRequest3 = new ServiceRequestCreate(
            lineId: Guid.NewGuid(),
            userId: Guid.NewGuid(),
            type: RequestType.Inspection,
            description: "Test description 3");
        var newRequest1 = await _setup.ServiceRequestRepository.CreateServiceRequestAsync(
            serviceRequest1.LineId,
            serviceRequest1.UserId,
            DateTime.Now, 
            RequestStatusType.Opened,
            serviceRequest1.Type,
            serviceRequest1.Description);
        var newRequest2 = await _setup.ServiceRequestRepository.CreateServiceRequestAsync(
            serviceRequest2.LineId,
            serviceRequest2.UserId,
            DateTime.Now, 
            RequestStatusType.Opened,
            serviceRequest2.Type,
            serviceRequest2.Description);
        var newRequest3 = await _setup.ServiceRequestRepository.CreateServiceRequestAsync(
            serviceRequest3.LineId,
            serviceRequest3.UserId,
            DateTime.Now, 
            RequestStatusType.Opened,
            serviceRequest3.Type,
            serviceRequest3.Description);
        
        // Act
        var result = await _setup.ServiceRequestRepository.GetAllServiceRequestsAsync(new ServiceRequestFilter());
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }
    
    [Fact]
    public async Task GetAllServiceRequestsAsync_ShouldReturnEmptyList_WhenNoServiceRequests()
    {
        _setup.ResetContext();
        
        // Arrange

        // Act
        var result = await _setup.ServiceRequestRepository.GetAllServiceRequestsAsync(new ServiceRequestFilter());
        
        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task GetServiceRequestByIdAsync_ShouldReturnServiceRequest()
    {
        _setup.ResetContext();
        
        // Arrange
        var serviceRequest = new ServiceRequestCreate(
            lineId: Guid.NewGuid(),
            userId: Guid.NewGuid(),
            type: RequestType.Inspection,
            description: "Test description");
        var newRequest = await _setup.ServiceRequestRepository.CreateServiceRequestAsync(
            serviceRequest.LineId,
            serviceRequest.UserId,
            DateTime.Now, 
            RequestStatusType.Opened,
            serviceRequest.Type,
            serviceRequest.Description);
        
        // Act
        var result = await _setup.ServiceRequestRepository.GetServiceRequestByIdAsync(newRequest.Id);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(newRequest.Id, result!.Id);
        Assert.Equal(serviceRequest.Description, result.Description);
    }
    
    [Fact]
    public async Task GetServiceRequestByIdAsync_ShouldReturnNull_WhenServiceRequestNotFound()
    {
        _setup.ResetContext();
        
        // Arrange

        // Act
        var result = await _setup.ServiceRequestRepository.GetServiceRequestByIdAsync(Guid.NewGuid());
        
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task UpdateServiceRequestAsync_ShouldUpdateServiceRequest()
    {
        _setup.ResetContext();
        
        // Arrange
        var serviceRequest = new ServiceRequestCreate(
            lineId: Guid.NewGuid(),
            userId: Guid.NewGuid(),
            type: RequestType.Inspection,
            description: "Test description");
        var newRequest = await _setup.ServiceRequestRepository.CreateServiceRequestAsync(
            serviceRequest.LineId,
            serviceRequest.UserId,
            DateTime.Now, 
            RequestStatusType.Opened,
            serviceRequest.Type,
            serviceRequest.Description);
        var updateData = new ServiceRequestUpdate(
            status: RequestStatusType.Closed,
            description: "Updated description");
        
        // Act
        var result = await _setup.ServiceRequestRepository.UpdateServiceRequestAsync(newRequest.Id, updateData);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(newRequest.Id, result.Id);
        Assert.Equal(updateData.Status, result.Status);
        Assert.Equal(updateData.Description, result.Description);
    }
    
    [Fact]
    public async Task UpdateServiceRequestAsync_ShouldThrowException_WhenServiceRequestNotFound()
    {
        _setup.ResetContext();
        
        // Arrange
        var updateData = new ServiceRequestUpdate(
            status: RequestStatusType.Closed,
            description: "Updated description");
        
        // Act
        async Task Act() => await _setup.ServiceRequestRepository.UpdateServiceRequestAsync(Guid.NewGuid(), updateData);
        
        // Assert
        await Assert.ThrowsAsync<ServiceRequestRepositoryException>(Act);
    }   
    
    [Fact]
    public async Task DeleteServiceRequest_ShouldDeleteServiceRequest()
    {
        _setup.ResetContext();
        
        // Arrange
        var serviceRequest = new ServiceRequestCreate(
            lineId: Guid.NewGuid(),
            userId: Guid.NewGuid(),
            type: RequestType.Inspection,
            description: "Test description");
        var newRequest = await _setup.ServiceRequestRepository.CreateServiceRequestAsync(
            serviceRequest.LineId,
            serviceRequest.UserId,
            DateTime.Now, 
            RequestStatusType.Opened,
            serviceRequest.Type,
            serviceRequest.Description);
        
        // Act
        await _setup.ServiceRequestRepository.DeleteServiceRequest(newRequest.Id);
        
        // Assert
        var result = await _setup.Context.ServiceRequests.FindAsync(newRequest.Id);
        Assert.Null(result);
    }
    
    [Fact]
    public async Task DeleteServiceRequest_ShouldThrowException_WhenServiceRequestNotFound()
    {
        _setup.ResetContext();
        
        // Arrange

        // Act
        async Task Act() => await _setup.ServiceRequestRepository.DeleteServiceRequest(Guid.NewGuid());
        
        // Assert
        await Assert.ThrowsAsync<ServiceRequestRepositoryException>(Act);
    }
}