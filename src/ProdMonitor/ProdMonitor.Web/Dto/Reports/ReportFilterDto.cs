namespace ProdMonitor.Web.Dto.Reports;

public class ReportFilterDto
{
    
    public ReportFilterDto() { }
    
    public ReportFilterDto(Guid? lineId = null,
        Guid? userId = null,
        Guid? requestId = null, 
        bool? sortByDate = null,
        int skip = 0,
        int limit = int.MaxValue)
    {
        LineId = lineId;
        UserId = userId;
        RequestId = requestId;
        SortByDate = sortByDate;
        Skip = skip;
        Limit = limit;
    }
    
    public Guid? LineId { get; set; }
    
    public Guid? UserId { get; set; }
    
    public Guid? RequestId { get; set; }
    
    public bool? SortByDate { get; set; }
    
    public int Skip { get; set; } = 0;
    
    public int Limit { get; set; } = int.MaxValue;
}