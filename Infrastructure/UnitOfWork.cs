using Application;
using Application.Repository;
using Domain.Entity;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private AppDbContext _context;
        public IUserAccountRepository UserAccounts { get; }
        public IEmailVerificationRepository EmailVerifications { get; }
        public IOrderRepository Orders { get; }
        public ITransportServiceRepository TransportServices { get; }
        public IOrderFishRepository OrderFishes { get; }
        public IFishQualificationRepository FishQualifications { get; }
        public IFishHealthRepository FishHealths { get;  }
        public IRouteStopRepository RouteStops { get;  }
        public IRouteRepository Routes { get;  }
        public IDriverRepository Drivers { get;  }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            UserAccounts = new UserAccountRepository(context);
            EmailVerifications = new EmailVerificationRepository(context);
            Orders = new OrderRepository(context);
            TransportServices = new TransportServiceRepository(context);
            OrderFishes = new OrderFishRepository(context);
            FishQualifications = new FishQualificationRepository(context);
            FishHealths = new FishHealthRepository(context);
            RouteStops = new RouteStopRepository(context);
            Routes = new RouteRepository(context);
            Drivers = new DriverRepository(context);
        }
        public async Task SaveChangeAsync()
        {
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }   
    }
    
}
