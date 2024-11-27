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
    public class UserConfig : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.HasMany(x => x.Orders)
                .WithOne(x => x.UserAccount)
                .HasForeignKey(x => x.AccountId);
            
            builder.HasOne(o => o.TransportService)
               .WithMany(o => o.UserAccounts)
               .HasForeignKey(o => o.TransportServiceId);


            builder.HasOne(d => d.Driver)
                   .WithOne(ua => ua.UserAccount)
                   .HasForeignKey<UserAccount>(d => d.DriverId); 
        }
    }
}
