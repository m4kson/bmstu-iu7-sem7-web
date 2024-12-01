using System.Text.Json.Serialization;

namespace ProdMonitor.Web.Dto.Orders;

public class OrderCreateDto(Guid userId, List<OrderDetailsDto> orderDetails)
{
    [JsonPropertyName("userId")]
    [JsonRequired]
    public Guid UserId { get; set; } = userId;
    
    [JsonPropertyName("orderDetails")]
    [JsonRequired]
    public List<OrderDetailsDto> OrderDetails { get; set; } = orderDetails;
}
