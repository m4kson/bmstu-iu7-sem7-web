using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.Web.Dto.Enums;

namespace ProdMonitor.Web.Controllers.Converters.Enums;

public static class AssemblyLineStatusDtoConverter
{
    public static LineStatusType ToDomain(this LineStatusTypeDto dto)
    {
        return dto switch
        {
            LineStatusTypeDto.Working => LineStatusType.Working,
            LineStatusTypeDto.OnService => LineStatusType.OnService,
            _ => throw new ArgumentOutOfRangeException(nameof(dto), dto, "Incorrect enum value")
        };
    }
    
    public static LineStatusTypeDto ToDto(this LineStatusType status)
    {
        return status switch
        {
            LineStatusType.Working => LineStatusTypeDto.Working,
            LineStatusType.OnService => LineStatusTypeDto.OnService,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Incorrect enum value")
        };
    }
}