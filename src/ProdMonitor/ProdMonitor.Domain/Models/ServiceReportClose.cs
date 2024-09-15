namespace ProdMonitor.Domain.Models
{
    public class ServiceReportClose(float price,
        string description)
    {
        public float Price { get; set; } = price;
        public string Description { get; set; } = description;
    }
}
