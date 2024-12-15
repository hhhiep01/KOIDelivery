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
    public class KoiSizeConfig : IEntityTypeConfiguration<KoiSize>
    {
        public void Configure(EntityTypeBuilder<KoiSize> builder)
        {
            builder.HasMany(x => x.OrderItems)
                 .WithOne(x => x.KoiSize)
                 .HasForeignKey(x => x.KoiSizeId);
        }
    }
}
