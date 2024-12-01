using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Domain.Models
{
    public class ServiceRequest
    {
        public Guid Id { get; set; }
        public Guid LineId { get; set; }
        public Guid UserId { get; set; }
        public DateTime RequestDate { get; set; }
        public RequestStatusType Status { get; set; }
        public RequestType Type { get; set; }
        public string Description { get; set; }

        public ServiceRequest(Guid id,
            Guid lineId,
            Guid userId,
            DateTime requestDate,
            RequestStatusType status,
            RequestType type,
            string description)
        { 
            Id = id;
            LineId = lineId;
            UserId = userId;
            RequestDate = requestDate;
            Status = status;
            Type = type;
            Description = description;
        }
    }
}
