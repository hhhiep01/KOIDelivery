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
    public class BoxAllocationConfig : IEntityTypeConfiguration<BoxAllocation>
    {
        public void Configure(EntityTypeBuilder<BoxAllocation> builder)
        {
            builder.HasOne(x => x.BoxType)
                .WithMany(x => x.BoxAllocations)
                .HasForeignKey(x => x.BoxTypeId);

            builder.HasOne(x => x.Order)
               .WithMany(x => x.BoxAllocations)
               .HasForeignKey(x => x.OrderId);
        }
    }
}
