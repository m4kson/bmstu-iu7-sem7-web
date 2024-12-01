using ProdMonitor.Domain.Models;

namespace ProdMonitor.Domain.Interfaces.Repositories
{
    public interface IOrderDetailRepository
    {
        Task<OrderDetail> CreateOrderDetail(OrderDetailCreate orderDetail);
    }
}
