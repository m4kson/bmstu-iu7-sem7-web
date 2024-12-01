using System.Text.Json.Serialization;

namespace ProdMonitor.Web.Dto.Reports;

public class ReportCloseDto(float totalPrice,
    string description)
{
    [JsonPropertyName("totalPrice")]
    [JsonRequired]
    public float TotalPrice { get; set; } = totalPrice;
    
    [JsonPropertyName("description")]
    [JsonRequired]
    public string Description { get; set; } = description;
}