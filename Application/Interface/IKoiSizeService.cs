using Application.Request.KoiSize;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IKoiSizeService
    {
        Task<ApiResponse> AddKoiSizeAsync(KoiSizeRequest koiSizeRequest);
        Task<ApiResponse> GetAllKoiSizeAsync();
    }
}
