using System.Diagnostics.CodeAnalysis;
using ProdMonitor.Domain.Models;
using ProdMonitor.Web.Dto.Reports;

namespace ProdMonitor.Web.Controllers.Converters;

public static class ServiceReportDtoConverter
{
    public static ServiceReportCreate ToDomain(this ReportCreateDto reportCreateDto)
    {
        return new ServiceReportCreate(
            userId: reportCreateDto.UserId,
            requestId: reportCreateDto.RequestId
        );
    } 
    
    [return: NotNullIfNotNull(nameof(serviceReport))]
    public static ReportDto? ToDto(this ServiceReport? serviceReport)
    {
        
        if (serviceReport is null)
            return null;
        
        return new ReportDto(
            id: serviceReport.Id,
            lineId: serviceReport.LineId,
            userId: serviceReport.UserId,
            requestId: serviceReport.RequestId,
            openDate: serviceReport.OpenDate,
            closeDate: serviceReport.CloseDate,
            totalPrice: serviceReport.Price,
            description: serviceReport.Description
        );
    }
    
    public static ServiceReportFilter ToDomain(this ReportFilterDto reportFilterDto)
    {
        return new ServiceReportFilter(
            lineId: reportFilterDto.LineId,
            userId: reportFilterDto.UserId,
            requestId: reportFilterDto.RequestId,
            sortByDate: reportFilterDto.SortByDate,
            skip: reportFilterDto.Skip,
            limit: reportFilterDto.Limit
        );
    }
    
    
    public static ServiceReportClose ToDomain(this ReportCloseDto serviceReportClose)
    {
        return new ServiceReportClose(
            price: serviceReportClose.TotalPrice,
            description: serviceReportClose.Description
        );
    }
}