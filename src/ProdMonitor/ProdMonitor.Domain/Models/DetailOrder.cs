using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Domain.Models
{
    public class DetailOrder
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DetailOrderStatusType Status { get; set; }
        public float TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } 

        public DetailOrder(Guid id,
                           Guid userId,
                           DetailOrderStatusType status,
                           float totalPrice,
                           DateTime orderDate,
                           ICollection<OrderDetail>? orderDetails = null)
        {
            Id = id;
            UserId = userId;
            Status = status;
            TotalPrice = totalPrice;
            OrderDate = orderDate;
            OrderDetails = orderDetails ?? new List<OrderDetail>();
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            OrderDetails.Add(orderDetail);
        }

        public void RemoveOrderDetail(OrderDetail orderDetail)
        {
            OrderDetails.Remove(orderDetail);
        }
    }
}
