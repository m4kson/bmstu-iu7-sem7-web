using System;
using System.Collections.Generic;

namespace ProdMonitor.DataAccess.Models
{
    public class DetailDb
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public virtual ICollection<AssemblyLineDb> AssemblyLines { get; set; }
        public virtual ICollection<OrderDetailDb> OrderDetails { get; set; }
        public virtual ICollection<SupplyDb> Supplies { get; set; } = new List<SupplyDb>();

        public DetailDb(Guid id,
                        string name,
                        string country,
                        int amount,
                        float price,
                        int length,
                        int height,
                        int width)
        {
            Id = id;
            Name = name;
            Country = country;
            Amount = amount;
            Price = price;
            Length = length;
            Height = height;
            Width = width;

            Supplies = new List<SupplyDb>();
        }
    }
}
