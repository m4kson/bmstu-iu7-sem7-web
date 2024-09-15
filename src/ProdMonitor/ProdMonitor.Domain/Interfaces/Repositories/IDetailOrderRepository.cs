using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Domain.Interfaces.Repositories
{
    public interface IDetailOrderRepository
    {
        Task<DetailOrder> CreateDetailOrderAsync(Guid userId,
            DetailOrderStatusType status,
            float totalPrice,
            DateTime orderDate,
            ICollection<OrderDetailData>? orderDetails);

        Task<List<DetailOrder>> GetAllDetailOrdersAsync(DetailOrderFilter filter);

        Task<DetailOrder?> GetDetailOrderByIdAsync(Guid id);
    }
}
