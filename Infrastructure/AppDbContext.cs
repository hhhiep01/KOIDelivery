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
            optionsBuilder.UseSqlServer("Data Source=koifarmshopserver.database.windows.net;Initial Catalog=KoiFarmDeliverySystem;Persist Security Info=True;User ID=Huydqse151428;Password=Huy123456789;Encrypt=True;Trust Server Certificate=True");
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }   
}
