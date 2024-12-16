using Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUnitOfWork
    {
        public IUserAccountRepository UserAccounts { get; }
        public IEmailVerificationRepository EmailVerifications { get; }
        public IOrderRepository Orders { get; }
        public ITransportServiceRepository TransportServices { get; }
        public IOrderFishRepository OrderFishes { get; }
        public IFishQualificationRepository FishQualifications { get; }
        public IFishHealthRepository FishHealths { get; }
        public IRouteStopRepository RouteStops { get; }
        public IRouteRepository Routes { get; }
        public IDriverRepository Drivers { get; }
        public IPaymentRepository Payments { get; }
        public IFeedbackRepository Feedbacks { get; }
        public IKoiSizeRepository KoiSizes { get; }
        public IBoxTypeRepository BoxTypes { get; }
        public IBoxAllocationRepository BoxAllocatios { get; }
        public IOrderItemRepository IOrderItems { get; }
        public IFishDetailRepository FishDetails { get; }

        public Task SaveChangeAsync();
        Task<T> ExecuteScalarAsync<T>(string sql);
        Task ExecuteRawSqlAsync(string sql);
    }
}
