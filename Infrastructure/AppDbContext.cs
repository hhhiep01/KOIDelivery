using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("Host=172.17.0.2; Port=5431; Database=koidelivery; Username=postgres; Password=matkhau;Include Error Detail=True;TrustServerCertificate=True");
        }
        public DbSet<UserAccount> Users { get; set; }
        public DbSet<EmailVerification> EmailVerifications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<TransportService> TransportServices { get; set; }
        public DbSet<OrderFish> OrderFishs { get; set; }
        public DbSet<FishQualification> FishQualifications { get; set; }
        public DbSet<FishHealth> FishHealths { get; set; }
        public DbSet<RouteStop> RouteStops { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<KoiSize> KoiSizes { get; set; }
        public DbSet<BoxType> BoxTypes { get; set; }
        public DbSet<BoxAllocation> BoxAllocations { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<FishDetail> FishDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }   
}
