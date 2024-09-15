using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Domain.Models
{
    public class AssemblyLineUpdate(string? name = null,
        LineStatusType? status = null,
        int? production = null,
        int? downTime = null,
        int? inspectionsPerYear = null,
        DateOnly? lastInspection = null,
        DateOnly? nextInspection = null,
        int? defectRate = null)
    {
        public string? Name { get; set; } = name;
        public LineStatusType? Status { get; set; } = status;
        public int? Production { get; set; } = production;
        public int? DownTime { get; set; } = downTime;
        public int? InspectionsPerYear { get; set; } = inspectionsPerYear;
        public DateOnly? LastInspection { get; set; } = lastInspection;
        public DateOnly? NextInspection { get; set; } = nextInspection;
        public int? DefectRate { get; set; } = defectRate;
    }
}
