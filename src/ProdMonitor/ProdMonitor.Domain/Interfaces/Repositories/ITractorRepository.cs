using ProdMonitor.Domain.Models;

namespace ProdMonitor.Domain.Interfaces.Repositories
{
    public interface ITractorRepository
    {
        Task<Tractor> CreateTractorAsync(TractorCreate tractor);

        Task<List<Tractor>> GetAllTractorsAsync(TractorFilter filter);

        Task<Tractor?> GetTractorByIdAsync(Guid id);
        
        Task DeleteTractorAsync(Guid id);
    }
}
