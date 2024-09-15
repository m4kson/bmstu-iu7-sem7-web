using ProdMonitor.DataAccess.Context;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.DataAccess.Models;
using ProdMonitor.DataAccess.Models.Converters;
using Microsoft.EntityFrameworkCore;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.DataAccess.Models.Converters.Enums;

namespace ProdMonitor.DataAccess.Repositories
{
    public class DetailOrderRepository : IDetailOrderRepository
    {
        private readonly ProdMonitorContext _context;

        public DetailOrderRepository(ProdMonitorContext context)
        {
            _context = context;
        }

        public async Task<DetailOrder> CreateDetailOrderAsync(Guid userId,
            DetailOrderStatusType status,
            float totalPrice,
            DateTime orderDate,
            ICollection<OrderDetailData>? orderDetails)
        {
            try
            {
                var detailOrder = new DetailOrder(
                    id: Guid.NewGuid(),
                    userId: userId,
                    status: status,
                    totalPrice: totalPrice,
                    orderDate: orderDate,
                    orderDetails: new List<OrderDetail>()
                );

                if (orderDetails != null && orderDetails.Any())
                {
                    foreach (var orderDetailData in orderDetails)
                    {
                        var orderDetail = new OrderDetail(
                            id: Guid.NewGuid(),
                            detailId: orderDetailData.DetailId,
                            detailOrderId: detailOrder.Id,
                            detailsAmount: orderDetailData.DetailsAmount
                        );
                        var createdOrderDetail = await _context.OrderDetails.AddAsync(OrderDetailConverter.ToDb(orderDetail)!);
                        detailOrder.OrderDetails.Add(orderDetail);
                    }
                }

                var detailOrderDb = DetailOrderConverter.ToDb(detailOrder);

                var result = _context.DetailOrders.Add(detailOrderDb!);
                await _context.SaveChangesAsync();

                var createdOrderDb = await _context.DetailOrders
                    .Include(o => o.OrderDetails)
                    .FirstOrDefaultAsync(o => o.Id == result.Entity.Id);

                if (createdOrderDb == null)
                {
                    throw new InvalidOperationException("Detail order was not found after creation.");
                }

                return DetailOrderConverter.ToDomain(createdOrderDb)!;
            }
            catch (Exception ex)
            {
                throw new DetailOrderRepositoryException("Failed to create detail order", ex);
            }
        }


        public async Task<List<DetailOrder>> GetAllDetailOrdersAsync(DetailOrderFilter filter)
        {
            try
            {
                IQueryable<DetailOrderDb> query = _context.DetailOrders;

                if (filter.UserId is not null)
                {
                    query = query.Where(o => o.UserId == filter.UserId.Value);
                }

                if (filter.Status is not null)
                {
                    query = query.Where(o => o.Status == DetailOrderStatusTypeConverter.ToDb(filter.Status.Value));
                }

                var detailOrders = await query
                    .Skip(filter.Skip)
                    .Take(filter.Limit)
                    .AsNoTracking()
                    .ToListAsync();


                var result = detailOrders.ConvertAll(o => DetailOrderConverter.ToDomain(o)!);
                return result;
            }
            catch (Exception ex)
            {
                throw new DetailOrderRepositoryException("Failed to retrieve detail orders", ex);
            }
        }

        public async Task<DetailOrder?> GetDetailOrderByIdAsync(Guid id)
        {
            try
            {
                var detailOrderDb = await _context.DetailOrders
                    .Include(o => o.OrderDetails)
                    .FirstOrDefaultAsync(o => o.Id == id);

                return DetailOrderConverter.ToDomain(detailOrderDb);
            }
            catch (Exception ex)
            {
                throw new DetailOrderRepositoryException("Failed to retrieve detail order by id", ex);
            }
        }
    }
}
