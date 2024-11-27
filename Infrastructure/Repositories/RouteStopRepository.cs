using Application.Repository;
using Domain.Entity;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RouteStopRepository : GenericRepository<RouteStop>, IRouteStopRepository
    {
        public RouteStopRepository(AppDbContext context) : base(context)
        {
        }
    }
}
