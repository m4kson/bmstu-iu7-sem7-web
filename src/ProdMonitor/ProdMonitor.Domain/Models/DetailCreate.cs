using System.Reflection.Emit;

namespace ProdMonitor.Domain.Models
{
    public class DetailCreate(string name,
        string country,
        int amount,
        float price,
        int length,
        int height,
        int width,
        ICollection<AssemblyLine>? assemblyLines = null,
        ICollection<OrderDetail>? orderDetails = null)
    {
        public string Name { get; set; } = name;
        public string Country { get; set; } = country;
        public int Amount { get; set; } = amount;
        public float Price { get; set; } = price;
        public int Length { get; set; } = length;
        public int Height { get; set; } = height;
        public int Width { get; set; } = width;
        public ICollection<AssemblyLine>? AssemblyLines { get; set; } = assemblyLines;
        public ICollection<OrderDetail>? OrderDetails { get; set; } = orderDetails;
    }
}
