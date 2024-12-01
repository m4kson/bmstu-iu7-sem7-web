namespace ProdMonitor.Domain.Models
{
    public class OrderDetailData(Guid detailId,
        int detailsAmount)
    {
        public Guid DetailId { get; set; } = detailId;
        public int DetailsAmount { get; set; } = detailsAmount;
    }
}
