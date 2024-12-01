using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.Web.Dto.Enums;

namespace ProdMonitor.Web.Controllers.Converters.Enums;

public static class SexTypeDtoConverter
{
    public static SexType ToDomain(this SexTypeDto sexTypeDto)
    {
        return sexTypeDto switch
        {
            SexTypeDto.Male => SexType.Male,
            SexTypeDto.Female => SexType.Female,
            _ => throw new ArgumentOutOfRangeException(nameof(sexTypeDto), sexTypeDto, "Incorrect enum value")
        };
    }

    public static SexTypeDto ToDto(this SexType sexType)
    {
        return sexType switch
        {
            SexType.Male => SexTypeDto.Male,
            SexType.Female => SexTypeDto.Female,
            _ => throw new ArgumentOutOfRangeException(nameof(sexType), sexType, "Incorrect enum value")
        };
    }
}