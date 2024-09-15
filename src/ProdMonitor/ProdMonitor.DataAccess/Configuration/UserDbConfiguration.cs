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
    public class UserDbConfiguration : IEntityTypeConfiguration<UserDb>
    {
        public void Configure(EntityTypeBuilder<UserDb> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).ValueGeneratedNever().IsRequired();
            builder.HasIndex(u => u.Id).IsUnique();

            builder
                .HasMany(u => u.DetailOrders)
                .WithOne(d => d.User)
                .HasForeignKey(d => d.UserId);

            builder 
                .HasMany(u => u.ServiceReports)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            builder
                .HasMany(u => u.ServiceRequests)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);
        }
    }
}
