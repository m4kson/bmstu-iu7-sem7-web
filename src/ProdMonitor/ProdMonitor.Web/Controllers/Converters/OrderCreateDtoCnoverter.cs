using System.Diagnostics.CodeAnalysis;
using ProdMonitor.Domain.Models;
using ProdMonitor.Web.Controllers.Converters.Enums;
using ProdMonitor.Web.Dto.Orders;

namespace ProdMonitor.Web.Controllers.Converters;

public static class OrderCreateDtoCnoverter
{
    public static DetailOrderCreate ToDomain(this OrderCreateDto orderCreateDto)
    {
        return new DetailOrderCreate
        (
            userId: orderCreateDto.UserId,
            orderDetails: orderCreateDto.OrderDetails.Select(x => x.ToDomain()).ToList()
        );
    }
    
    public static OrderDetailData ToDomain(this OrderDetailsDto orderDetailsDto)
    {
        return new OrderDetailData
        (
            detailId: orderDetailsDto.DetailId,
            detailsAmount: orderDetailsDto.Amount
        );
    }

    [return: NotNullIfNotNull(nameof(detailOrder))]
    public static OrderDto? ToDto(this DetailOrder? detailOrder)
    {
        if (detailOrder is null)
            return null;

        return new OrderDto
        (
            id: detailOrder.Id,
            userId: detailOrder.UserId,
            status: detailOrder.Status.ToDto(),
            totalPrice: detailOrder.TotalPrice,
            orderDate: detailOrder.OrderDate
        );
    }
    
    public static DetailOrderFilter ToDomain(this OrderFilterDto filterDto)
    {
        return new DetailOrderFilter
        (
            userId: filterDto.UserId,
            status: filterDto.Status?.ToDomain(),
            skip: filterDto.Skip,
            limit: filterDto.Limit
        );
    }
}