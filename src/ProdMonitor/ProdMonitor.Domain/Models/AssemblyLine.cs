using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.Domain.Models
{
    public class AssemblyLine
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Length { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public LineStatusType Status { get; set; }
        public int Production { get; set; }
        public int DownTime { get; set; }
        public int InspectionsPerYear { get; set; }
        public DateOnly LastInspection { get; set; }
        public DateOnly NextInspection { get; set; }
        public int DefectRate { get; set; }

        public ICollection<Tractor> Tractors { get; set; }
        public ICollection<Detail> Details { get; set; }

        public AssemblyLine(Guid id,
            string name,
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
            Id = id;
            Name = name;
            Length = length;
            Height = height;
            Width = width;
            Status = status;
            Production = production;
            DownTime = downTime;
            InspectionsPerYear = inspectionsPerYear;
            LastInspection = lastInspection;
            NextInspection = nextInspection;
            DefectRate = defectRate;
            Tractors = tractors ?? new List<Tractor>();
            Details = details ?? new List<Detail>();
        }

        public void AddTractor(Tractor tractor)
        {
            if (!Tractors.Contains(tractor))
            {
                Tractors.Add(tractor);
            }
        }

        public void AddDetail(Detail detail)
        {
            if (!Details.Contains(detail))
            {
                Details.Add(detail);
            }
        }

        public void RemoveTractor(Tractor tractor)
        {
            Tractors.Remove(tractor);
        }

        public void RemoveDetail(Detail detail)
        {
            Details.Remove(detail);
        }
    }
}
