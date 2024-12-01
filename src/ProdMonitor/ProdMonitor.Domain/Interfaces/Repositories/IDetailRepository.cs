using ProdMonitor.Domain.Models;

namespace ProdMonitor.Domain.Interfaces.Repositories
{
    public interface IDetailRepository
    {
        Task<Detail> CreateDetailAsync(DetailCreate detail);

        Task<List<Detail>> GetAllDetailsAsync(DetailFilter filter);

        Task<Detail?> GetDetailByIdAsync(Guid id);

        Task UpdateDetailAsync(Guid id, int amaunt);
        
        Task DeleteDetailAsync(Guid id);
    }
}
