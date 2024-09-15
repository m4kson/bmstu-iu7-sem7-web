namespace ProdMonitor.Domain.Models
{
    public class ServiceReport
    {
        public Guid Id { get; set; }
        public Guid LineId { get; set; }
        public Guid UserId { get; set; }
        public Guid RequestId { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public float? Price { get; set; }
        public string? Description { get; set; }

        public ServiceReport(Guid id,
            Guid lineId,
            Guid userId,
            Guid requestId,
            DateTime openDate,
            DateTime? closeDate,
            float? price,
            string? description)
        {
            Id = id;
            LineId = lineId;
            UserId = userId;
            RequestId = requestId;
            OpenDate = openDate;
            CloseDate = closeDate;
            Price = price;
            Description = description;
        }
    }
}
