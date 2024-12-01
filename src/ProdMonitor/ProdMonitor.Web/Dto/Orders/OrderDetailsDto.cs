using System.Text.Json.Serialization;

namespace ProdMonitor.Web.Dto.Orders;

public class OrderDetailsDto(Guid detailId,
    int amount)
{
    [JsonPropertyName("detailId")]
    [JsonRequired]
    public Guid DetailId { get; set; } = detailId;
    
    [JsonPropertyName("amount")]
    [JsonRequired]
    public int Amount { get; set; } = amount;
}