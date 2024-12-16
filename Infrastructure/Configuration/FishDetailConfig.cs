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
    public class FishDetailConfig : IEntityTypeConfiguration<FishDetail>
    {
        public void Configure(EntityTypeBuilder<FishDetail> builder)
        {
            builder.HasMany(x => x.FishQualifications)
                .WithOne(x => x.FishDetail)
                .HasForeignKey(x => x.FishDetailId);

            builder.HasMany(x => x.FishHealths)
                .WithOne(x => x.FishDetail)
                .HasForeignKey(x => x.FishDetailId);

        }
    }
}
