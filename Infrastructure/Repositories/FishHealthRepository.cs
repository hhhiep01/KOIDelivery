using Application.Repository;
using Domain.Entity;


namespace Infrastructure.Repositories
{
    public class FishHealthRepository : GenericRepository<FishHealth>, IFishHealthRepository
    {
        public FishHealthRepository(AppDbContext context) : base(context)
        {
        }
    }
}
