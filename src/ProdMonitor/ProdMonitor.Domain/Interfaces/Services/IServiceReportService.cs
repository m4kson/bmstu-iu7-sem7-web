using ProdMonitor.Domain.Models;

namespace ProdMonitor.Domain.Interfaces.Services
{
    public interface IServiceReportService
    {
        Task<ServiceReport> CreateServiceReportAsync(ServiceReportCreate report);

        Task<List<ServiceReport>> GetAllServiceReportsAsync(ServiceReportFilter filter);

        Task<ServiceReport> GetServiceReportByIdAsync(Guid id);

        Task<ServiceReport> CloseServiceReportAsync(Guid id, ServiceReportClose report);
    }
}
