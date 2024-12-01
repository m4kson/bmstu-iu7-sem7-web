using System.Text.Json.Serialization;

namespace ProdMonitor.Web.Dto.Reports;

public class ReportCreateDto(Guid userId, Guid requestId)
{
    [JsonPropertyName("userId")]
    [JsonRequired]
    public Guid UserId { get; set; } = userId;
    
    [JsonPropertyName("requestId")]
    [JsonRequired]
    public Guid RequestId { get; set; } = requestId;
}