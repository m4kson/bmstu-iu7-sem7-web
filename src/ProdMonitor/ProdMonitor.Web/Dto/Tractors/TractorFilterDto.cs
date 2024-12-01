namespace ProdMonitor.Web.Dto.Tractors;

public class TractorFilterDto
{
    public TractorFilterDto() { }
    
    public TractorFilterDto(int? releaseYear = null,
        string? engineType = null,
        string? ecologicalStandart = null,
        int skip = 0,
        int limit = int.MaxValue)
    {
        ReleaseYear = releaseYear;
        EngineType = engineType;
        EcologicalStandart = ecologicalStandart;
        Skip = skip;
        Limit = limit;
    }
    
    public int? ReleaseYear { get; set; }
    
    public string? EngineType { get; set; }
    
    public string? EcologicalStandart { get; set; }
    
    public int Skip { get; set; } = 0;
    
    public int Limit { get; set; } = int.MaxValue;
}