using ProdMonitor.Domain.Models;

namespace ProdMonitor.Domain.Interfaces.Services
{
    public interface IDetailOrderService
    {
        Task<DetailOrder> CreateDetailOrderAsync(DetailOrderCreate order);

        Task<List<DetailOrder>> GetAllDetailOrdersAsync(DetailOrderFilter filter);

        Task<DetailOrder> GetDetailOrderById(Guid id);
    }
}
