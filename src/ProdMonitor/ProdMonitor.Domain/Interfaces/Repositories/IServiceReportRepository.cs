using ProdMonitor.Domain.Models;

namespace ProdMonitor.Domain.Interfaces.Repositories
{
    public interface IServiceReportRepository
    {
        Task<ServiceReport> CreateServiceReportAsync(Guid lineId,
            Guid userId,
            Guid requestId,
            DateTime openDate);

        Task<List<ServiceReport>> GetAllServiceReportsAsync(ServiceReportFilter filter);

        Task<ServiceReport?> GetServiceReportByIdAsync(Guid id);

        Task<ServiceReport> CloseServiceReportAsync(Guid id,
            DateTime closeDate,
            float price,
            string description);
    }
}
