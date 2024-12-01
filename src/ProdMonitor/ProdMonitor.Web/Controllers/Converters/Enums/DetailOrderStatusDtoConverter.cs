using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.Web.Dto.Enums;

namespace ProdMonitor.Web.Controllers.Converters.Enums;

public static class DetailOrderStatusDtoConverter
{
    public static DetailOrderStatusDto ToDto(this DetailOrderStatusType status)
    {
        return status switch
        {
            DetailOrderStatusType.Processing => DetailOrderStatusDto.Processing,
            DetailOrderStatusType.InWork => DetailOrderStatusDto.InWork,
            DetailOrderStatusType.InDelivery => DetailOrderStatusDto.InDelivery,
            DetailOrderStatusType.Done => DetailOrderStatusDto.Done,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
    
    public static DetailOrderStatusType ToDomain(this DetailOrderStatusDto status)
    {
        return status switch
        {
            DetailOrderStatusDto.Processing => DetailOrderStatusType.Processing,
            DetailOrderStatusDto.InWork => DetailOrderStatusType.InWork,
            DetailOrderStatusDto.InDelivery => DetailOrderStatusType.InDelivery,
            DetailOrderStatusDto.Done => DetailOrderStatusType.Done,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}