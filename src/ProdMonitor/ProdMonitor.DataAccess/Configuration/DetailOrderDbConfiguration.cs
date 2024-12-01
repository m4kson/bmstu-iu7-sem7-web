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
    public class DetailOrderDbConfiguration : IEntityTypeConfiguration<DetailOrderDb>
    {
        public void Configure(EntityTypeBuilder<DetailOrderDb> builder)
        {
            builder.HasKey(j => j.Id);

            builder.Property(j => j.Id).ValueGeneratedNever().IsRequired();
            builder.HasIndex(j => j.Id).IsUnique();

            builder
                .HasMany(j => j.OrderDetails)
                .WithOne(o => o.Order)
                .HasForeignKey(o => o.DetailOrderId);

            builder
                .HasOne(j => j.User)
                .WithMany(u => u.DetailOrders);
        }
    }
}
