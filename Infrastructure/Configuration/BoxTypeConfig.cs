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
    public class BoxTypeConfig : IEntityTypeConfiguration<BoxType>
    {
        public void Configure(EntityTypeBuilder<BoxType> builder)
        {
            builder.HasMany(x => x.BoxAllocations)
                .WithOne(x => x.BoxType)
                .HasForeignKey(x => x.BoxTypeId);

        }
    }
}
