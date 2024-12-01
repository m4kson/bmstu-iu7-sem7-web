using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdMonitor.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.DataAccess.Configuration
{
    public class OrderDetailDbConfiguration : IEntityTypeConfiguration<OrderDetailDb>
    {
        public void Configure(EntityTypeBuilder<OrderDetailDb> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).ValueGeneratedNever().IsRequired();
            builder.HasIndex(o => o.Id).IsUnique();

            builder
                .HasOne(o => o.Order)
                .WithMany(j => j.OrderDetails);

            builder
                .HasOne(o => o.Detail)
                .WithMany(d => d.OrderDetails);
        }
    }
}
