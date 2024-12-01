using System.Diagnostics.CodeAnalysis;
using ProdMonitor.Domain.Models;
using ProdMonitor.Web.Dto.Tractors;

namespace ProdMonitor.Web.Controllers.Converters;

public static class TractorDtoConverter
{
    public static TractorCreate ToDomain(this TractorCreateDto dto)
    {
        return new TractorCreate(
            model: dto.Model,
            releaseYear: dto.ReleaseYear,
            engineType: dto.EngineType,
            enginePower: dto.EnginePower,
            frontTireSize: dto.FrontTireSize,
            backTireSize: dto.BackTireSize,
            wheelsAmount: dto.WheelsAmount,
            tankCapacity: dto.TankCapacity,
            ecologicalStandart: dto.EcologicalStandart,
            length: dto.Length,
            width: dto.Width,
            cabinHeight: dto.CabinHeight);
    }
    
    [return: NotNullIfNotNull(nameof(tractor))]
    public static TractorDto? ToDto(this Tractor? tractor)
    {
        if (tractor is null)
            return null;
        
        return new TractorDto(
            id: tractor.Id,
            model: tractor.Model,
            releaseYear: tractor.ReleaseYear,
            engineType: tractor.EngineType,
            enginePower: tractor.EnginePower,
            frontTireSize: tractor.FrontTireSize,
            backTireSize: tractor.BackTireSize,
            wheelsAmount: tractor.WheelsAmount,
            tankCapacity: tractor.TankCapacity,
            ecologicalStandart: tractor.EcologicalStandart,
            length: tractor.Length,
            width: tractor.Width,
            cabinHeight: tractor.CabinHeight);
    }
    
    public static TractorFilter ToDomain(this TractorFilterDto dto)
    {
        return new TractorFilter(
            releaseYear: dto.ReleaseYear,
            engineType: dto.EngineType,
            ecologicalStandart: dto.EcologicalStandart,
            skip: dto.Skip,
            limit: dto.Limit);
    }
}