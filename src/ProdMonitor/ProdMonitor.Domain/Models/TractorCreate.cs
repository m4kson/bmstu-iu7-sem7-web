namespace ProdMonitor.Domain.Models
{
    public class TractorCreate(string model,
        int releaseYear,
        string engineType,
        string enginePower,
        int frontTireSize,
        int backTireSize,
        int wheelsAmount,
        int tankCapacity,
        string ecologicalStandart,
        float length,
        float width,
        float cabinHeight,
        ICollection<AssemblyLine>? assemblyLines = null)
    {
        public string Model { get; set; } = model;
        public int ReleaseYear { get; set; } = releaseYear;
        public string EngineType { get; set; } = engineType;
        public string EnginePower { get; set; } = enginePower;
        public int frontTireSize { get; set; } = frontTireSize;
        public int backTireSize { get; set; } = backTireSize;
        public int wheelsAmount { get; set; } = wheelsAmount;
        public int tankCapacity { get; set; } = tankCapacity;
        public string ecologicalStandart { get; set; } = ecologicalStandart;
        public float length { get; set; } = length;
        public float width { get; set; } = width;
        public float cabinHeight { get; set; } = cabinHeight;
        public ICollection<AssemblyLine>? assemblyLines { get; set; } = assemblyLines;
    }
}
