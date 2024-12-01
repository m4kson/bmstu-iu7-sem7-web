using ProdMonitor.Domain.Models;

namespace ProdMonitor.Domain.Interfaces.Services
{
    public interface IDetailService
    {
        Task<Detail> CreateDetailAsync(DetailCreate detail);

        Task<List<Detail>> GetAllDetailsAsync(DetailFilter filter);

        Task<Detail> GetDetailByIdAsync(Guid id);
        
        Task DeleteDetailAsync(Guid id);
    }
}
