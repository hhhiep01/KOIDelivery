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
    public class OrderFishConfig : IEntityTypeConfiguration<OrderFish>
    {
        public void Configure(EntityTypeBuilder<OrderFish> builder)
        {
            builder.HasOne(o => o.Order)
                 .WithMany(o => o.OrderFishs)
                 .HasForeignKey(o => o.OrderId);
            builder.HasMany(o => o.FishHealths)
                 .WithOne(o => o.OrderFish)
                 .HasForeignKey(o => o.OrderFishId);
        }
    }
}
