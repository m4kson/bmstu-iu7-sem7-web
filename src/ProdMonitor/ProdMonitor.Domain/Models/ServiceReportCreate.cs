namespace ProdMonitor.Domain.Models
{
    public class ServiceReportCreate(Guid userId, Guid requestId)
    {
        public Guid UserId { get; set; } = userId;
        public Guid RequestId { get; set; } = requestId;
    }
}
