using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ProdMonitor.DataAccess.Models;
using ProdMonitor.DataAccess.Configuration;

namespace ProdMonitor.DataAccess.Context
{
    public class ProdMonitorContext : DbContext
    {
        public DbSet<AssemblyLineDb> AssemblyLines { get; set; }
        public DbSet<DetailDb> Details { get; set; }
        public DbSet<DetailOrderDb> DetailOrders { get; set; }
        public DbSet<OrderDetailDb> OrderDetails { get; set; }
        public DbSet<ServiceReportDb> ServiceReports { get; set; }
        public DbSet<ServiceRequestDb> ServiceRequests { get; set; }
        public DbSet<TractorDb> Tractors { get; set; }
        public DbSet<UserDb> Users { get; set; }
        public DbSet<SupplyDb> Supplies { get; set; }


        public ProdMonitorContext(DbContextOptions<ProdMonitorContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AssemblyLineDbConfiguration());
            modelBuilder.ApplyConfiguration(new DetailDbConfiguration());
            modelBuilder.ApplyConfiguration(new DetailOrderDbConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailDbConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceReportDbConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceRequestDbConfiguration());
            modelBuilder.ApplyConfiguration(new TractorDbConfiguration());
            modelBuilder.ApplyConfiguration(new UserDbConfiguration());
            modelBuilder.ApplyConfiguration(new SupplyDbConfiguration());
        }
    }
}
