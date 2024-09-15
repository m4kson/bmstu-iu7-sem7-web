using System;
using System.Collections.Generic;
using ProdMonitor.DataAccess.Models.Enums;

namespace ProdMonitor.DataAccess.Models
{
    public class AssemblyLineDb
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Length { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public LineStatusTypeDb Status { get; set; }
        public int Production { get; set; }
        public int DownTime { get; set; }
        public int InspectionsPerYear { get; set; }
        public DateOnly LastInspection { get; set; }
        public DateOnly NextInspection { get; set; }
        public int DefectRate { get; set; }

        public virtual ICollection<TractorDb> Tractors { get; set; }
        public virtual ICollection<ServiceReportDb> ServiceReports { get; set; }
        public virtual ICollection<ServiceRequestDb> ServiceRequests { get; set; }
        public virtual ICollection<DetailDb> Details { get; set; }

        public AssemblyLineDb(
            Guid id,
            string name,
            float length,
            float height,
            float width,
            LineStatusTypeDb status,
            int production,
            int downTime,
            int inspectionsPerYear,
            DateOnly lastInspection,
            DateOnly nextInspection,
            int defectRate)
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
        }

        public AssemblyLineDb()
        {
        }
    }
}
