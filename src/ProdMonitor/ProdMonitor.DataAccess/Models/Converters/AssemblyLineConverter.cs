using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.DataAccess.Models.Converters.Enums;

namespace ProdMonitor.DataAccess.Models.Converters
{
    public static class AssemblyLineConverter
    {
        public static AssemblyLine? ToDomain(AssemblyLineDb? assemblyLineDb)
        {
            if (assemblyLineDb == null)
            {
                return null;
            }

            var statusType = LineStatusTypeConverter.ToDomain(assemblyLineDb.Status);

            return new AssemblyLine(
                id: assemblyLineDb.Id,
                name: assemblyLineDb.Name,
                length: assemblyLineDb.Length,
                height: assemblyLineDb.Height,
                width: assemblyLineDb.Width,
                status: statusType,
                production: assemblyLineDb.Production,
                downTime: assemblyLineDb.DownTime,
                inspectionsPerYear: assemblyLineDb.InspectionsPerYear,
                lastInspection: assemblyLineDb.LastInspection,
                nextInspection: assemblyLineDb.NextInspection,
                defectRate: assemblyLineDb.DefectRate,
                tractors: assemblyLineDb.Tractors?.Select(TractorConverter.ToDomain).ToList() ?? new List<Tractor>(),
                details: assemblyLineDb.Details?.Select(DetailConverter.ToDomain).ToList() ?? new List<Detail>()
            );
        }

        public static AssemblyLineDb? ToDb(AssemblyLine? assemblyLineDomain)
        {
            if (assemblyLineDomain == null)
            {
                return null;
            }

            var statusType = LineStatusTypeConverter.ToDb(assemblyLineDomain.Status);

            return new AssemblyLineDb(
                id: assemblyLineDomain.Id,
                name: assemblyLineDomain.Name,
                length: assemblyLineDomain.Length,
                height: assemblyLineDomain.Height,
                width: assemblyLineDomain.Width,
                status: statusType,
                production: assemblyLineDomain.Production,
                downTime: assemblyLineDomain.DownTime,
                inspectionsPerYear: assemblyLineDomain.InspectionsPerYear,
                lastInspection: assemblyLineDomain.LastInspection,
                nextInspection: assemblyLineDomain.NextInspection,
                defectRate: assemblyLineDomain.DefectRate
            );
        }
    }
}
