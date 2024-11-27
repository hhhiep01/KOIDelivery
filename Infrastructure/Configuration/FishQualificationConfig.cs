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
    public class FishQualificationConfig : IEntityTypeConfiguration<FishQualification>
    {
        public void Configure(EntityTypeBuilder<FishQualification> builder)
        {
            throw new NotImplementedException();
        }
    }
}
