using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ICaculateTotalPriceService
    {
        Task<ApiResponse> CaculateTotalPriceLocal(int OrderId);
        Task<ApiResponse> CaculateTotalPriceInternational(int OrderId);
        Task<ApiResponse> CaculateTotalPriceDomestic(int OrderId);
    }
}
