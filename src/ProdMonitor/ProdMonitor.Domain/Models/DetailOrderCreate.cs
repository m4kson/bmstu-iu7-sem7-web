namespace ProdMonitor.Domain.Models
{
    public class DetailOrderCreate(Guid userId,
    ICollection<OrderDetailData> orderDetails)
    {
        public Guid UserId { get; set; } = userId;
        public ICollection<OrderDetailData> OrderDetails { get; set; } = orderDetails;
    }
}
