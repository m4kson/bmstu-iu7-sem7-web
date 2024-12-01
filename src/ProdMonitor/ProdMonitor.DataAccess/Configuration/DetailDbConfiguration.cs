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
    public class DetailDbConfiguration : IEntityTypeConfiguration<DetailDb>
    {
        public void Configure(EntityTypeBuilder<DetailDb> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id).ValueGeneratedNever().IsRequired();
            builder.HasIndex(d => d.Id).IsUnique();

            builder
                .HasMany(d => d.AssemblyLines)
                .WithMany(l => l.Details);

            builder
                .HasMany(d => d.OrderDetails)
                .WithOne(o => o.Detail)
                .HasForeignKey(o => o.DetailId);
        }
    }
}
