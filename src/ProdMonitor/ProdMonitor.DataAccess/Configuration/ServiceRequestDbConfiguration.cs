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
    public class ServiceRequestDbConfiguration : IEntityTypeConfiguration<ServiceRequestDb>
    {
        public void Configure(EntityTypeBuilder<ServiceRequestDb> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).ValueGeneratedNever().IsRequired();
            builder.HasIndex(s => s.Id).IsUnique();

            builder
                .HasOne(s => s.ServiceReport)
                .WithOne(r => r.ServiceRequest)
                .HasForeignKey<ServiceReportDb>(r => r.RequestId);

            builder
                .HasOne(s => s.AssemblyLine)
                .WithMany(l => l.ServiceRequests)
                .HasForeignKey(l => l.LineId);

            builder
                .HasOne(s => s.User)
                .WithMany(u => u.ServiceRequests)
                .HasForeignKey(s => s.UserId);

        }
    }
}
