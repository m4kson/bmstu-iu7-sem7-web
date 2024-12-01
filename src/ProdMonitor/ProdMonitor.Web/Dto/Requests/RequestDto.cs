using System.Text.Json.Serialization;
using ProdMonitor.Web.Dto.Enums;

namespace ProdMonitor.Web.Dto.Requests;

public class RequestDto(Guid id, 
    Guid lineId,
    Guid userId,
    DateTime requestDate,
    RequestStatusTypeDto status,
    RequestTypeDto type,
    string description)
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
    
    [JsonPropertyName("requestDate")]
    [JsonRequired]
    public DateTime RequestDate { get; set; } = requestDate;
    
    [JsonPropertyName("status")]
    [JsonRequired]
    public RequestStatusTypeDto Status { get; set; } = status;
    
    [JsonPropertyName("type")]
    [JsonRequired]
    public RequestTypeDto Type { get; set; } = type;
    
    [JsonPropertyName("description")]
    [JsonRequired]
    public string Description { get; set; } = description;
}