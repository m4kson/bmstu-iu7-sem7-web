using ProdMonitor.Domain.Models;

namespace ProdMonitor.Domain.Interfaces.Services
{
    public interface IAssemblyLineService
    {
        Task<AssemblyLine> CreateAssemblyLineAsync(AssemblyLineCreate line);

        Task<List<AssemblyLine>> GetAllAssemblyLinesAsync(AssemblyLineFilter filter);

        Task<AssemblyLine> GetAssemblyLineByIdAsync(Guid id);
        
        Task DeleteAssemblyLineAsync(Guid id);
    }
}
