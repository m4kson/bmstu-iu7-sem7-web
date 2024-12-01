using ProdMonitor.Web.Dto.Enums;

namespace ProdMonitor.Web.Dto.AssemblyLines;

public class AssemblyLineFilterDto
{
    public AssemblyLineFilterDto() { }
    
    public AssemblyLineFilterDto(LineStatusTypeDto? status = null,
        int skip = 0,
        int limit = int.MaxValue)
    {
        Status = status;
        Skip = skip;
        Limit = limit;
    }
    
    public LineStatusTypeDto? Status { get; set; }

    public int Skip { get; set; } = 0;

    public int Limit { get; set; } = int.MaxValue;
}


