using ProdMonitor.Domain.Models;

namespace ProdMonitor.Domain.Interfaces.Repositories
{
    public interface IAssemblyLineRepository
    {
        Task<AssemblyLine> CreateAssemblyLineAsync(AssemblyLineCreate line);

        Task<List<AssemblyLine>> GetAllAssemblyLinesAsync(AssemblyLineFilter filter);

        Task<AssemblyLine?> GetAssemblyLineByIdAsync(Guid id);

        Task<AssemblyLine> UpdateAssemblyLineAsync(Guid id, AssemblyLineUpdate assemblyLineUpdate);
    }
}
