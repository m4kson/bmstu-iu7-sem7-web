namespace ProdMonitor.DataAccess.Models
{
    public class TractorDb
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
        public string EcologicalStandard { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float CabinHeight { get; set; }


        public virtual ICollection<AssemblyLineDb>? AssemblyLines { get; set; }


        public TractorDb(Guid id,
                         string model,
                         int releaseYear,
                         string engineType,
                         string enginePower,
                         int frontTireSize,
                         int backTireSize,
                         int wheelsAmount,
                         int tankCapacity,
                         string ecologicalStandard,
                         float length,
                         float width,
                         float cabinHeight)
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
            EcologicalStandard = ecologicalStandard;
            Length = length;
            Width = width;
            CabinHeight = cabinHeight;
        }

        public TractorDb()
        {
        }
    }
}
