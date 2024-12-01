using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.Web.Dto.Enums;

namespace ProdMonitor.Web.Controllers.Converters.Enums;

public static class ServiceRequestStatusDtoConverter
{
    public static RequestStatusType ToDomain(this RequestStatusTypeDto requestStatusTypeDto)
    {
        return requestStatusTypeDto switch
        {
            RequestStatusTypeDto.Opened => RequestStatusType.Opened,
            RequestStatusTypeDto.InProgress => RequestStatusType.InProgress,
            RequestStatusTypeDto.Closed => RequestStatusType.Closed,
            _ => throw new ArgumentOutOfRangeException(nameof(requestStatusTypeDto), requestStatusTypeDto, null)
        };
    }
    
    public static RequestStatusTypeDto ToDto(this RequestStatusType requestStatusType)
    {
        return requestStatusType switch
        {
            RequestStatusType.Opened => RequestStatusTypeDto.Opened,
            RequestStatusType.InProgress => RequestStatusTypeDto.InProgress,
            RequestStatusType.Closed => RequestStatusTypeDto.Closed,
            _ => throw new ArgumentOutOfRangeException(nameof(requestStatusType), requestStatusType, null)
        };
    }
}