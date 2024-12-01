using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Domain.Interfaces.Repositories
{
    public interface IServiceRequestRepository
    {
        Task<ServiceRequest> CreateServiceRequestAsync(Guid lineId,
            Guid userId,
            DateTime requestDate,
            RequestStatusType status,
            RequestType type,
            string description);

        Task<List<ServiceRequest>> GetAllServiceRequestsAsync(ServiceRequestFilter filter);

        Task<ServiceRequest?> GetServiceRequestByIdAsync(Guid id);

        Task<ServiceRequest> UpdateServiceRequestAsync(Guid id, ServiceRequestUpdate updateData);

        Task DeleteServiceRequest(Guid id);
    }
}
