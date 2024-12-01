using System.Text.Json.Serialization;

namespace ProdMonitor.Web.Dto.Details;

public class DetailDto(Guid id,
    string name,
    string country,
    int amount,
    float price,
    float length,
    float width,
    float height)
{
    [JsonPropertyName("id")]
    [JsonRequired]
    public Guid Id { get; set; } = id;
    
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
    public float Length { get; set; } = length;
    
    [JsonPropertyName("width")]
    [JsonRequired]
    public float Width { get; set; } = width;
    
    [JsonPropertyName("height")]
    [JsonRequired]
    public float Height { get; set; } = height;
}