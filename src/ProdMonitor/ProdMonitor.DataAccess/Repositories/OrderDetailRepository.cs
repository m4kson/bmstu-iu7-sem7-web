using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Models;
using Microsoft.EntityFrameworkCore;
using ProdMonitor.DataAccess.Context;
using ProdMonitor.DataAccess.Models.Enums;
using ProdMonitor.DataAccess.Models;
using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.DataAccess.Models.Converters;
using ProdMonitor.DataAccess.Models.Converters.Enums;
using ProdMonitor.Domain.Exceptions;

namespace ProdMonitor.DataAccess.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ProdMonitorContext _context;

        public OrderDetailRepository(ProdMonitorContext context)
        {
            _context = context;
        }

        public async Task<OrderDetail> CreateOrderDetail(OrderDetailCreate orderDetail)
        {
            try
            {
                var orderDetailDb = new OrderDetailDb(id: Guid.NewGuid(),
                    detailId: orderDetail.DetailId,
                    detailOrderId: orderDetail.DetailOrderId,
                    detailsAmount: orderDetail.DetailsAmount);

                var result = await _context.OrderDetails.AddAsync(orderDetailDb);
                await _context.SaveChangesAsync();

                var createdOrderDetailDb = await _context.OrderDetails
                    .FirstOrDefaultAsync(o => o.Id == result.Entity.Id);

                return OrderDetailConverter.ToDomain(createdOrderDetailDb)!;
            }
            catch (Exception ex)
            {
                throw new OrderDetailRepositoryException("Failed to create OrderDetail", ex);
            }
        }
    }
}
