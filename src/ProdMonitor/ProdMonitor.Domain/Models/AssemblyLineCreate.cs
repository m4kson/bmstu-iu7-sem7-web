using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Domain.Models
{
    public class AssemblyLineCreate(string name,
        float length,
        float height,
        float width,
        LineStatusType status,
        int production,
        int downTime,
        int inspectionsPerYear,
        DateOnly lastInspection,
        DateOnly nextInspection,
        int defectRate,
        ICollection<Tractor>? tractors = null,
        ICollection<Detail>? details = null)
    {
        public string Name { get; set; } = name;
        public float Length { get; set; } = length;
        public float Height { get; set; } = height;
        public float Width { get; set; } = width;
        public LineStatusType Status { get; set; } = status;
        public int Production { get; set; } = production;
        public int DownTime { get; set; } = downTime;
        public int InspectionsPerYear { get; set; } = inspectionsPerYear;
        public DateOnly LastInspection { get; set; } = lastInspection;
        public DateOnly NextInspection { get; set; } = nextInspection;
        public int DefectRate { get; set; } = defectRate;
        public ICollection<Tractor>? Tractors { get; set; } = tractors;
        public ICollection<Detail>? Details { get; set; } = details;
    }
}
