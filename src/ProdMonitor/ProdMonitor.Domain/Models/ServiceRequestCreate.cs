using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Domain.Models
{
    public class ServiceRequestCreate(Guid lineId,
        Guid userId,
        RequestType type,
        string description)
    {
        public Guid LineId { get; set; } = lineId;
        public Guid UserId { get; set; } = userId;
        public RequestType Type { get; set; } = type;
        public string Description { get; set; } = description;
    }
}
