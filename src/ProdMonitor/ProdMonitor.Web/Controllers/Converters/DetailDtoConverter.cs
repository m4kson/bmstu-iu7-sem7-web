using System.Diagnostics.CodeAnalysis;
using ProdMonitor.Domain.Models;
using ProdMonitor.Web.Dto.Details;

namespace ProdMonitor.Web.Controllers.Converters;

public static class DetailDtoConverter
{
    public static DetailCreate ToDomain(this DetailCreateDto detailCreateDto)
    {
        return new DetailCreate(
            name: detailCreateDto.Name,
            country: detailCreateDto.Country,
            amount: detailCreateDto.Amount,
            price: detailCreateDto.Price,
            length: detailCreateDto.Length,
            height: detailCreateDto.Height,
            width: detailCreateDto.Width);
    }
    
    [return: NotNullIfNotNull(nameof(detail))]
    public static DetailDto? ToDto(this Detail? detail)
    {
        if (detail is null)
            return null;
        
        return new DetailDto(
            id: detail.Id,
            name: detail.Name,
            country: detail.Country,
            amount: detail.Amount,
            price: detail.Price,
            length: detail.Length,
            height: detail.Height,
            width: detail.Width);
    }
    
    public static DetailFilter ToDomain(this DetailFilterDto filterDto)
    {
        return new DetailFilter(
            country: filterDto.Country,
            skip: filterDto.Skip,
            limit: filterDto.Limit);
    }
}