using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using ProdMonitor.Domain.Models;
using ProdMonitor.Web.Controllers.Converters.Enums;
using ProdMonitor.Web.Dto.AssemblyLines;

namespace ProdMonitor.Web.Controllers.Converters;

public static class AssemblyLineDtoConverter
{
    public static AssemblyLineCreate ToDomain(this AssemblyLineCreateDto assemblyLineCreateDto)
    {
        return new AssemblyLineCreate(
            name: assemblyLineCreateDto.Name,
            length: assemblyLineCreateDto.Length,
            height: assemblyLineCreateDto.Height,
            width: assemblyLineCreateDto.Width,
            status: assemblyLineCreateDto.Status.ToDomain(),
            downTime: assemblyLineCreateDto.Downtime,
            inspectionsPerYear: assemblyLineCreateDto.InspectionsPerYear,
            lastInspection: assemblyLineCreateDto.LastInspection,
            nextInspection: assemblyLineCreateDto.NextInspection,
            defectRate: assemblyLineCreateDto.DefectRate);
    }
    
    [return: NotNullIfNotNull(nameof(assemblyLine))]
    public static AssemblyLineDto? ToDto(this AssemblyLine? assemblyLine)
    {
        if (assemblyLine is null)
            return null;

        return new AssemblyLineDto(
            id: assemblyLine.Id,
            name: assemblyLine.Name,
            length: assemblyLine.Length,
            height: assemblyLine.Height,
            width: assemblyLine.Width,
            status: assemblyLine.Status.ToDto(),
            downtime: assemblyLine.DownTime,
            inspectionsPerYear: assemblyLine.InspectionsPerYear,
            lastInspection: assemblyLine.LastInspection,
            nextInspection: assemblyLine.NextInspection,
            defectRate: assemblyLine.DefectRate);
    }
    
    public static AssemblyLineFilter ToDomain(this AssemblyLineFilterDto assemblyLineFilterDto)
    {
        return new AssemblyLineFilter(
            status: assemblyLineFilterDto.Status?.ToDomain(),
            skip: assemblyLineFilterDto.Skip,
            limit: assemblyLineFilterDto.Limit);
    } 
}