using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Domain.Interfaces.Services
{
    public interface IServiceRequestService
    {
        Task<ServiceRequest> CreateServiceRequestAsync(ServiceRequestCreate serviceRequest);

        Task<List<ServiceRequest>> GetAllServiceRequestsAsync(ServiceRequestFilter filter);

        Task<ServiceRequest> GetServiceRequestByIdAsync(Guid id);
    }
}
