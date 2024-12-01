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
    public class AssemblyLineDbConfiguration: IEntityTypeConfiguration<AssemblyLineDb>
    {
        public void Configure(EntityTypeBuilder<AssemblyLineDb> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id).ValueGeneratedNever().IsRequired();
            builder.HasIndex(l => l.Id).IsUnique();

            builder
                .HasMany(l => l.Tractors)
                .WithMany(t => t.AssemblyLines);

            builder
                .HasMany(l => l.Details)
                .WithMany(d => d.AssemblyLines);

            builder
                .HasMany(l => l.ServiceReports)
                .WithOne(r => r.AssemblyLine)
                .HasForeignKey(r => r.LineId);

            builder
                .HasMany(l => l.ServiceRequests)
                .WithOne(r => r.AssemblyLine)
                .HasForeignKey(r => r.LineId);
        }
    }
}
