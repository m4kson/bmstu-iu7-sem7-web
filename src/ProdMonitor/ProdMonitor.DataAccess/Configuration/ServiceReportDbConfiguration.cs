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
    public class ServiceReportDbConfiguration : IEntityTypeConfiguration<ServiceReportDb>
    {
        public void Configure(EntityTypeBuilder<ServiceReportDb> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id).ValueGeneratedNever().IsRequired();
            builder.HasIndex(r => r.Id).IsUnique();

            builder
                .HasOne(r => r.ServiceRequest)
                .WithOne(s => s.ServiceReport);

            builder
                .HasOne(r => r.AssemblyLine)
                .WithMany(l => l.ServiceReports);

            builder
                .HasOne(r => r.User)
                .WithMany(u => u.ServiceReports);
        }

    }
}
