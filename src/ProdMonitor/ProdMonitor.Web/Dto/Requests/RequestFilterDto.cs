using ProdMonitor.Web.Dto.Enums;

namespace ProdMonitor.Web.Dto.Requests;

public class RequestFilterDto
{
    public RequestFilterDto() { }
    
    public RequestFilterDto(Guid? lineId = null,
        Guid? userId = null,
        RequestTypeDto? type = null,
        RequestStatusTypeDto? status = null,
        bool? sortByDate = null,
        int skip = 0,
        int limit = int.MaxValue)
    {
        LineId = lineId;
        UserId = userId;
        Type = type;
        Status = status;
        SortByDate = sortByDate;
        Skip = skip;
        Limit = limit;
    }
    
    public Guid? LineId { get; set; }
    
    public Guid? UserId { get; set; }
    
    public RequestTypeDto? Type { get; set; }
    
    public RequestStatusTypeDto? Status { get; set; }
    
    public bool? SortByDate { get; set; }
    
    public int Skip { get; set; } = 0;
    
    public int Limit { get; set; } = int.MaxValue;
}