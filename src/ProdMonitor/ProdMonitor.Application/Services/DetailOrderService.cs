using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Interfaces.Services;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;
using Serilog;

namespace ProdMonitor.Application.Services
{
    public class DetailOrderService : IDetailOrderService
    {
        private readonly IDetailOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IDetailRepository _detailRepository;
        private readonly ILogger _logger;

        public DetailOrderService(IDetailOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IDetailRepository detailRepository,
            ILogger logger)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _detailRepository = detailRepository;
            _logger = logger;
        }

        public async Task<DetailOrder> CreateDetailOrderAsync(DetailOrderCreate order)
        {
            _logger.Information("Attempting to create a new detail order for user {UserId}", order.UserId);
            try
            {
                if (order.OrderDetails == null || !order.OrderDetails.Any())
                {
                    _logger.Warning("Detail order creation failed: No order details provided for user {UserId}", order.UserId);
                    throw new ArgumentException("Order must contain at least one detail.");
                }

                var totalPrice = 0f;
                foreach (OrderDetailData orderDetail in order.OrderDetails)
                {
                    var detail = await _detailRepository.GetDetailByIdAsync(orderDetail.DetailId);
                    if (detail == null)
                    {
                        _logger.Warning("Detail order creation failed: Detail with ID {DetailId} not found", orderDetail.DetailId);
                        throw new DetailNotFoundException($"Detail with id {orderDetail.DetailId} not found");
                    }
                    totalPrice += detail.Price * orderDetail.DetailsAmount;
                }

                var newOrder = await _orderRepository.CreateDetailOrderAsync(userId: order.UserId,
                    status: DetailOrderStatusType.InWork,
                    totalPrice: totalPrice,
                    orderDate: DateTime.Now.ToUniversalTime(),
                    orderDetails: order.OrderDetails);

                _logger.Information("Successfully created a new detail order with ID {OrderId} for user {UserId}", newOrder.Id, order.UserId);
                return newOrder;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Detail order creation failed for user {UserId}", order.UserId);
                throw new DetailOrderException("Failed to create order", ex);
            }
        }

        public async Task<List<DetailOrder>> GetAllDetailOrdersAsync(DetailOrderFilter filter)
        {
            _logger.Information("Attempting to retrieve all detail orders with the specified filter");
            try
            {
                var orders = await _orderRepository.GetAllDetailOrdersAsync(filter);
                _logger.Information("Successfully retrieved {OrderCount} detail orders", orders.Count);
                return orders;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to retrieve detail orders");
                throw new DetailOrderException("Failed to get detail orders", ex);
            }
        }

        public async Task<DetailOrder> GetDetailOrderById(Guid id)
        {
            _logger.Information("Attempting to retrieve detail order with ID {OrderId}", id);
            try
            {
                var order = await _orderRepository.GetDetailOrderByIdAsync(id);
                if (order == null)
                {
                    _logger.Warning("Detail order with ID {OrderId} not found", id);
                    throw new DetailOrderNotFoundException($"Detail order with id {id} not found");
                }

                _logger.Information("Successfully retrieved detail order with ID {OrderId}", id);
                return order;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to retrieve detail order with ID {OrderId}", id);
                throw new DetailOrderException("Failed to get detail order", ex);
            }
        }
    }
}
