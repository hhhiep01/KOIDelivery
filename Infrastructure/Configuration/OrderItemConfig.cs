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
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasOne(x => x.KoiSize)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.KoiSizeId);
            
            builder.HasOne(x => x.Order)
                 .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.KoiSizeId);
        }
    }
}
