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
    public class TransportServiceConfig : IEntityTypeConfiguration<TransportService>
    {
        public void Configure(EntityTypeBuilder<TransportService> builder)
        {
            builder.HasMany(o => o.UserAccounts)
                .WithOne(o => o.TransportService)
                .HasForeignKey(o => o.TransportServiceId);
        }
    }
}
