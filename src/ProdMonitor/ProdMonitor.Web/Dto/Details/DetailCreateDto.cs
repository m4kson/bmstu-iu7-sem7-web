using System.Text.Json.Serialization;

namespace ProdMonitor.Web.Dto.Details;

public class DetailCreateDto(string name,
    string country,
    int amount,
    float price,
    int length,
    int width,
    int height)
{
    [JsonPropertyName("name")]
    [JsonRequired]
    public string Name { get; set; } = name;
    
    [JsonPropertyName("country")]
    [JsonRequired]
    public string Country { get; set; } = country;
    
    [JsonPropertyName("amount")]
    [JsonRequired]
    public int Amount { get; set; } = amount;
    
    [JsonPropertyName("price")]
    [JsonRequired]
    public float Price { get; set; } = price;
    
    [JsonPropertyName("length")]
    [JsonRequired]
    public int Length { get; set; } = length;
    
    [JsonPropertyName("width")]
    [JsonRequired]
    public int Width { get; set; } = width;
    
    [JsonPropertyName("height")]
    [JsonRequired]
    public int Height { get; set; } = height;
}