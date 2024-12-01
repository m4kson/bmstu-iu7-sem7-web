using System.Text.Json.Serialization;

namespace ProdMonitor.Web.Dto.Tractors;

public class TractorCreateDto(string model,
    int releaseYear,
    string engineType,
    string enginePower,
    int frontTireSize,
    int backTireSize,
    int wheelsAmount,
    int tankCapacity,
    string ecologicalStandart,
    float length,
    float width,
    float cabinHeight)
{
    [JsonPropertyName("model")]
    [JsonRequired]
    public string Model { get; set; } = model;
    
    [JsonPropertyName("releseYear")]
    [JsonRequired]
    public int ReleaseYear { get; set; } = releaseYear;
    
    [JsonPropertyName("engineType")]
    [JsonRequired]
    public string EngineType { get; set; } = engineType;
    
    [JsonPropertyName("enginePower")]
    [JsonRequired]
    public string EnginePower { get; set; } = enginePower;
    
    [JsonPropertyName("frontTireSize")]
    [JsonRequired]
    public int FrontTireSize { get; set; } = frontTireSize;
    
    [JsonPropertyName("backTireSize")]
    [JsonRequired]
    public int BackTireSize { get; set; } = backTireSize;
    
    [JsonPropertyName("wheelsAmount")]
    [JsonRequired]
    public int WheelsAmount { get; set; } = wheelsAmount;
    
    [JsonPropertyName("tankCapacity")]
    [JsonRequired]
    public int TankCapacity { get; set; } = tankCapacity;
    
    [JsonPropertyName("ecologicalStandart")]
    [JsonRequired]
    public string EcologicalStandart { get; set; } = ecologicalStandart;
    
    [JsonPropertyName("length")]
    [JsonRequired]
    public float Length { get; set; } = length;
    
    [JsonPropertyName("width")]
    [JsonRequired]
    public float Width { get; set; } = width;
    
    [JsonPropertyName("cabinHeight")]
    [JsonRequired]
    public float CabinHeight { get; set; } = cabinHeight;
}