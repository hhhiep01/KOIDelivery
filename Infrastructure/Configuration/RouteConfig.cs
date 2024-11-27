using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class RouteConfig : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.HasMany(o => o.RouteStops)
                .WithOne(o => o.Route)
                .HasForeignKey(o => o.RouteId);
            builder.HasOne(o => o.Driver)
                .WithMany(o => o.Routes)
                .HasForeignKey(o => o.DriverId);
        }
    }
}
