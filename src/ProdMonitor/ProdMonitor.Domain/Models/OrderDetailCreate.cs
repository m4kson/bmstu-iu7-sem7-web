namespace ProdMonitor.Domain.Models
{
    public class OrderDetailCreate(Guid detailId,
        Guid detailOrderId,
        int detailsAmount)
    {
        public Guid DetailId { get; set; } = detailId;
        public Guid DetailOrderId { get; set; } = detailOrderId;
        public int DetailsAmount { get; set; } = detailsAmount;
    }
}
