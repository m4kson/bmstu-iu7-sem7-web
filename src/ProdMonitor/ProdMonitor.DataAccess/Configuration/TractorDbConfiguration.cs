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
    public class TractorDbConfiguration : IEntityTypeConfiguration<TractorDb>
    {
        public void Configure(EntityTypeBuilder<TractorDb> builder) 
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).ValueGeneratedNever().IsRequired();
            builder.HasIndex(t => t.Id).IsUnique();

            builder
                .HasMany(t => t.AssemblyLines)
                .WithMany(l => l.Tractors);
        }
    }
}
