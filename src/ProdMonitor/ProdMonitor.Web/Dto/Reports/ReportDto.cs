using System.Text.Json.Serialization;

namespace ProdMonitor.Web.Dto.Reports;

public class ReportDto(Guid id,
    Guid lineId,
    Guid userId,
    Guid requestId,
    DateTime openDate,
    DateTime? closeDate,
    float? totalPrice,
    string? description)
{
    [JsonPropertyName("id")]
    [JsonRequired]
    public Guid Id { get; set; } = id;
    
    [JsonPropertyName("lineId")]
    [JsonRequired]
    public Guid LineId { get; set; } = lineId;
    
    [JsonPropertyName("userId")]
    [JsonRequired]
    public Guid UserId { get; set; } = userId;
    
    [JsonPropertyName("requestId")]
    [JsonRequired]
    public Guid RequestId { get; set; } = requestId;
    
    [JsonPropertyName("openDate")]
    [JsonRequired]
    public DateTime OpenDate { get; set; } = openDate;
    
    [JsonPropertyName("closeDate")]
    [JsonRequired]
    public DateTime? CloseDate { get; set; } = closeDate;
    
    [JsonPropertyName("totalPrice")]
    [JsonRequired]
    public float? TotalPrice { get; set; } = totalPrice;
    
    [JsonPropertyName("description")]
    [JsonRequired]
    public string? Description { get; set; } = description;
}