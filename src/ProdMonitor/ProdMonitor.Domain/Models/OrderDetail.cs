namespace ProdMonitor.Domain.Models
{
    public class OrderDetail
    {
        public Guid Id { get; set; }
        public Guid DetailId { get; set; }
        public Guid DetailOrderId { get; set; }
        public int DetailsAmount { get; set; }

        public OrderDetail(Guid id, 
            Guid detailId, 
            Guid detailOrderId, 
            int detailsAmount)
        {
            Id = id;
            DetailId = detailId;
            DetailOrderId = detailOrderId;
            DetailsAmount = detailsAmount;
        }
    }
}
