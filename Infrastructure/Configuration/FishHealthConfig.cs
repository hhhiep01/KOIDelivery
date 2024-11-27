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
    public class FishHealthConfig : IEntityTypeConfiguration<FishHealth>
    {
        public void Configure(EntityTypeBuilder<FishHealth> builder)
        {
            builder.HasOne(o => o.OrderFish)
                 .WithMany(o => o.FishHealths)
                 .HasForeignKey(o => o.OrderFishId);
        }
    }
}
