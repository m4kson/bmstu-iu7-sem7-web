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
    public class SupplyDbConfiguration : IEntityTypeConfiguration<SupplyDb>
    {
        public void Configure(EntityTypeBuilder<SupplyDb> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).ValueGeneratedNever().IsRequired();
            builder.HasIndex(s => s.Id).IsUnique();

            builder
             .HasOne(s => s.Detail)
             .WithMany(d => d.Supplies) 
             .HasForeignKey(s => s.DetailId)
             .OnDelete(DeleteBehavior.Cascade);

            builder.Property(s => s.SupplyDate)
                  .IsRequired();

            builder.Property(s => s.Quantity)
                   .IsRequired();
        }
    }
}
