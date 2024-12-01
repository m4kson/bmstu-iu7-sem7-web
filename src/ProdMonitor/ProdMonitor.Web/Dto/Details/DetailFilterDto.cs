namespace ProdMonitor.Web.Dto.Details;

public class DetailFilterDto
{
    
    public DetailFilterDto() { }
    
    public DetailFilterDto(string? country = null,
        int skip = 0,
        int limit = int.MaxValue)
    {
        Country = country;
        Skip = skip;
        Limit = limit;
    }
    
    public string? Country { get; set; }
    
    public int Skip { get; set; } = 0;
    
    public int Limit { get; set; } = int.MaxValue;
}