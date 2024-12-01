using System.Text.Json.Serialization;
using ProdMonitor.Web.Dto.Enums;

namespace ProdMonitor.Web.Dto.Orders;

public class OrderDto(Guid id,
    Guid userId,
    DetailOrderStatusDto status,
    float totalPrice,
    DateTime orderDate)
{
    [JsonPropertyName("id")]
    [JsonRequired]
    public Guid Id { get; set; } = id;
    
    [JsonPropertyName("userId")]
    [JsonRequired]
    public Guid UserId { get; set; } = userId;
    
    [JsonPropertyName("status")]
    [JsonRequired]
    public DetailOrderStatusDto Status { get; set; } = status;
    
    [JsonPropertyName("totalPrice")]
    [JsonRequired]
    public float TotalPrice { get; set; } = totalPrice;
    
    [JsonPropertyName("orderDate")]
    [JsonRequired]
    public DateTime OrderDate { get; set; } = orderDate;
}