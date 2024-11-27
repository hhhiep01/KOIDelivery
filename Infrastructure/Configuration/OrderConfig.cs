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
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(x => x.UserAccount)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.AccountId);
            
            builder.HasMany(x => x.OrderFishs)
               .WithOne(x => x.Order)
               .HasForeignKey(x => x.OrderId);

            builder.HasMany(x => x.RouteStops)
              .WithOne(x => x.Order)
              .HasForeignKey(x => x.OrderId);

            builder.HasOne(x => x.TransportService)
               .WithMany(x => x.Orders)
               .HasForeignKey(x => x.TransportServiceId);

        }
    }
}
