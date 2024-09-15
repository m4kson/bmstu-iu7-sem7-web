namespace ProdMonitor.Domain.Models
{
    public class Detail
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public ICollection<AssemblyLine> AssemblyLines { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }  

        public Detail(Guid id,
            string name,
            string country,
            int amount,
            float price,
            int length,
            int height,
            int width,
            ICollection<AssemblyLine>? assemblyLines = null,
            ICollection<OrderDetail>? orderDetails = null)
        {
            Id = id;
            Name = name;
            Country = country;
            Amount = amount;
            Price = price;
            Length = length;
            Height = height;
            Width = width;
            AssemblyLines = assemblyLines ?? new List<AssemblyLine>();
            OrderDetails = orderDetails ?? new List<OrderDetail>();
        }

        public void AddLine(AssemblyLine line)
        {
            if (!AssemblyLines.Contains(line))
            {
                AssemblyLines.Add(line);
            }
        }

        public void RemoveLine(AssemblyLine line)
        {
            AssemblyLines.Remove(line);
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            if (!OrderDetails.Contains(orderDetail))
            {
                OrderDetails.Add(orderDetail);
            }
        }

        public void RemoveOrderDetail(OrderDetail orderDetail)
        {
            OrderDetails.Remove(orderDetail);
        }
    }
}
