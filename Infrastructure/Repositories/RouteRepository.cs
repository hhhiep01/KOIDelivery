using Application.Repository;
using Domain.Entity;

namespace Infrastructure.Repositories
{
    public class RouteRepository : GenericRepository<Route>, IRouteRepository
    {
        public RouteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
