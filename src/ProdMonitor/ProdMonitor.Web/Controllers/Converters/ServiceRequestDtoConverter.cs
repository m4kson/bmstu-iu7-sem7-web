using System.Diagnostics.CodeAnalysis;
using ProdMonitor.Domain.Models;
using ProdMonitor.Web.Controllers.Converters.Enums;
using ProdMonitor.Web.Dto.Requests;

namespace ProdMonitor.Web.Controllers.Converters;

public static class ServiceRequestDtoConverter
{
    public static ServiceRequestCreate ToDomain(this RequestCreateDto requestCreateDto)
    {
        return new ServiceRequestCreate(
            lineId: requestCreateDto.LineId,
            userId: requestCreateDto.UserId,
            type: requestCreateDto.Type.ToDomain(),
            description: requestCreateDto.Description
        );
    }
    
    [return: NotNullIfNotNull(nameof(serviceRequest))]
    public static RequestDto? ToDto(this ServiceRequest? serviceRequest)
    {
        if (serviceRequest is null)
            return null;
        
        return new RequestDto(
            id: serviceRequest.Id,
            lineId: serviceRequest.LineId,
            userId: serviceRequest.UserId,
            requestDate: serviceRequest.RequestDate,
            status: serviceRequest.Status.ToDto(),
            type: serviceRequest.Type.ToDto(),
            description: serviceRequest.Description
        );
    }
    
    public static ServiceRequestFilter ToDomain(this RequestFilterDto requestFilterDto)
    {
        return new ServiceRequestFilter(
            lineId: requestFilterDto.LineId,
            userId: requestFilterDto.UserId,
            type: requestFilterDto.Type?.ToDomain(),
            status: requestFilterDto.Status?.ToDomain(),
            sortByDate: requestFilterDto.SortByDate,
            skip: requestFilterDto.Skip,
            limit: requestFilterDto.Limit
        );
    }
}