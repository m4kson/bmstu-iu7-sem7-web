using System.Text.Json.Serialization;
using ProdMonitor.Web.Dto.Enums;

namespace ProdMonitor.Web.Dto.AssemblyLines;

public class AssemblyLineCreateDto(string name,
    float length,
    float width,
    float height,
    LineStatusTypeDto status,
    int downtime,
    int inspectionsPerYear,
    DateOnly lastInspection,
    DateOnly nextInspection,
    float defectRate)
{
    [JsonPropertyName("name")]
    [JsonRequired]
    public string Name { get; set; } = name;
    
    [JsonPropertyName("length")]
    [JsonRequired]
    public float Length { get; set; } = length;
    
    [JsonPropertyName("width")]
    [JsonRequired]
    public float Width { get; set; } = width;
    
    [JsonPropertyName("height")]
    [JsonRequired]
    public float Height { get; set; } = height;
    
    [JsonPropertyName("status")]
    [JsonRequired]
    public LineStatusTypeDto Status { get; set; } = status;
    
    [JsonPropertyName("downtime")]
    [JsonRequired]
    public int Downtime { get; set; } = downtime;
    
    [JsonPropertyName("inspectionsPerYear")]
    [JsonRequired]
    public int InspectionsPerYear { get; set; } = inspectionsPerYear;
    
    [JsonPropertyName("lastInspection")]
    [JsonRequired]
    public DateOnly LastInspection { get; set; } = lastInspection;
    
    [JsonPropertyName("nextInspection")]
    [JsonRequired]
    public DateOnly NextInspection { get; set; } = nextInspection;
    
    [JsonPropertyName("defectRate")]
    [JsonRequired]
    public float DefectRate { get; set; } = defectRate;
}