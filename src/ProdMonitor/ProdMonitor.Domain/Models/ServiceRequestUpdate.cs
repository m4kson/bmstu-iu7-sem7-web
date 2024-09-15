using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Domain.Models
{
    public class ServiceRequestUpdate(Guid? lineId = null,
        RequestType? type = null,
        RequestStatusType? status = null,
        string? description = null)
    {
        public Guid? LineId { get; set; } = lineId;
        public RequestType? Type { get; set; } = type;
        public RequestStatusType? Status { get; set; } = status;
        public string? Description { get; set; } = description;
    }
}
