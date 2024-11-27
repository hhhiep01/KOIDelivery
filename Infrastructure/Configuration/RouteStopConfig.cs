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
    public class RouteStopConfig : IEntityTypeConfiguration<RouteStop>
    {
        public void Configure(EntityTypeBuilder<RouteStop> builder)
        {
            builder.HasOne(x => x.Order)
               .WithMany(x => x.RouteStops)
               .HasForeignKey(x => x.OrderId);

            builder.HasOne(x => x.Route)
               .WithMany(x => x.RouteStops)
               .HasForeignKey(x => x.RouteId);
        }
    }
}
