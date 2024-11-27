using Application.Repository;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FishQualificationRepository : GenericRepository<FishQualification>, IFishQualificationRepository
    {
        public FishQualificationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
