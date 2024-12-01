using System.Text.Json.Serialization;
using ProdMonitor.Web.Dto.Enums;

namespace ProdMonitor.Web.Dto.Requests;

public class RequestCreateDto(Guid lineId,
    Guid userId,
    RequestTypeDto type,
    string description)
{
    [JsonPropertyName("lineId")]
    [JsonRequired]
    public Guid LineId { get; set; } = lineId;
    
    [JsonPropertyName("userId")]
    [JsonRequired]
    public Guid UserId { get; set; } = userId;
    
    [JsonPropertyName("type")]
    [JsonRequired]
    public RequestTypeDto Type { get; set; } = type;
    
    [JsonPropertyName("description")]
    [JsonRequired]
    public string Description { get; set; } = description;
}