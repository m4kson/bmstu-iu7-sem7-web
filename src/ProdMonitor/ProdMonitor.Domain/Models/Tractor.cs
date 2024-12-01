namespace ProdMonitor.Domain.Models
{
    public class Tractor
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public int ReleaseYear { get; set; }
        public string EngineType { get; set; }
        public string EnginePower { get; set; }
        public int FrontTireSize { get; set; }
        public int BackTireSize { get; set; }
        public int WheelsAmount { get; set; }
        public int TankCapacity { get; set; }
        public string EcologicalStandart { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float CabinHeight { get; set; }
        public ICollection<AssemblyLine> AssemblyLines { get; set; }


        public Tractor(Guid id,
                       string model,
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
            Id = id;
            Model = model;
            ReleaseYear = releaseYear;
            EngineType = engineType;
            EnginePower = enginePower;
            FrontTireSize = frontTireSize;
            BackTireSize = backTireSize;
            WheelsAmount = wheelsAmount;
            TankCapacity = tankCapacity;
            EcologicalStandart = ecologicalStandart;
            Length = length;
            Width = width;
            CabinHeight = cabinHeight;
            AssemblyLines = assemblyLines ?? new List<AssemblyLine>();
        }

        public void AddAssemblyLine(AssemblyLine assemblyLine)
        {
            if (assemblyLine == null)
            {
                throw new ArgumentNullException(nameof(assemblyLine));
            }
            AssemblyLines.Add(assemblyLine);
        }

        public void RemoveAssemblyLine(AssemblyLine assemblyLine)
        {
            if (assemblyLine == null)
            {
                throw new ArgumentNullException(nameof(assemblyLine));
            }
            AssemblyLines.Remove(assemblyLine);
        }
    }
}
