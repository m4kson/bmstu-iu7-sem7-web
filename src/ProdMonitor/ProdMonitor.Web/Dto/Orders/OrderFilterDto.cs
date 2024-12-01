using ProdMonitor.Web.Dto.Enums;

namespace ProdMonitor.Web.Dto.Orders;

public class OrderFilterDto
{
    
    public OrderFilterDto() { }

    public OrderFilterDto(Guid? userId = null,
        DetailOrderStatusDto? status = null,
        int skip = 0,
        int limit = int.MaxValue)
    {
        UserId = userId;
        Status = status;
        Skip = skip;
        Limit = limit;
    }
    
    public Guid? UserId { get; set; }
    
    public DetailOrderStatusDto? Status { get; set; }
    
    public int Skip { get; set; } = 0;
    
    public int Limit { get; set; } = int.MaxValue;
}