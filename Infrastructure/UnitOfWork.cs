using Application;
using Application.Repository;
using Domain.Entity;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
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
        public IPaymentRepository Payments { get;  }

        public IFeedbackRepository Feedbacks { get; }
        public IKoiSizeRepository KoiSizes { get; }
        public IBoxTypeRepository BoxTypes { get; }
        public IBoxAllocationRepository BoxAllocatios { get; }
        public IOrderItemRepository IOrderItems { get; }
        public IFishDetailRepository FishDetails { get; }


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
            Payments = new PaymentRepository(context);
            KoiSizes = new KoiSizeRepository(context);
            BoxTypes = new BoxTypeRepository(context);
            BoxAllocatios = new BoxAllocationRepository(context);
            IOrderItems = new OrderItemRepository(context);
            FishDetails = new FishDetailRepository(context);
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

        public async Task<T> ExecuteScalarAsync<T>(string sql) 
            // helper method thực thi 1 câu lệnh SQL, trả về 1 giá trị duy nhất dưới dạng kiểu dữ liệu chỉ định T
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand()) 
                // DBCommand thực thi 1 câu lệnh SQL
            {
                try
                {
                    // Đảm bảo rằng kết nối với cơ sở dữ liệu được mở trước khi thực thi
                    if (command.Connection!.State != System.Data.ConnectionState.Open) 
                        await command.Connection.OpenAsync();

                    command.CommandText = sql;

                    var result = await command.ExecuteScalarAsync(); 
                    //ExecuteScalarAsync trả về giá trị đầu tiên từ kết quả truy vấn
                    return (T)Convert.ChangeType(result, typeof(T));
                    //Chuyển đổi kết quả thành kiểu dữ liệu T
                }
                finally
                {
                    // Đóng kết nối dù thành công hay là lỗi
                    if (command.Connection!.State == System.Data.ConnectionState.Open)
                        await command.Connection.CloseAsync();
                }
            }
        }

        public async Task ExecuteRawSqlAsync(string sql)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                try
                {
                    if (command.Connection.State != System.Data.ConnectionState.Open)
                    {
                        await command.Connection.OpenAsync();
                    }

                    command.CommandText = sql;
                    await command.ExecuteNonQueryAsync();
                }
                finally
                {
                    if (command.Connection.State == System.Data.ConnectionState.Open)
                        await command.Connection.CloseAsync();
                }
            }
        }
    }
    
}
