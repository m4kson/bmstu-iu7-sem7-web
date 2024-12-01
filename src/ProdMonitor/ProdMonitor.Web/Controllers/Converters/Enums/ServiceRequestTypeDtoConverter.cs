using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.Web.Dto.Enums;

namespace ProdMonitor.Web.Controllers.Converters.Enums;

public static class ServiceRequestTypeDtoConverter
{
    public static RequestType ToDomain(this RequestTypeDto requestTypeDto)
    {
        return requestTypeDto switch
        {
            RequestTypeDto.Inspection => RequestType.Inspection,
            RequestTypeDto.Repair => RequestType.Repair,
            _ => throw new ArgumentOutOfRangeException(nameof(requestTypeDto), requestTypeDto, null)
        };
    }
    
    public static RequestTypeDto ToDto(this RequestType requestType)
    {
        return requestType switch
        {
            RequestType.Inspection => RequestTypeDto.Inspection,
            RequestType.Repair => RequestTypeDto.Repair,
            _ => throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null)
        };
    }
}