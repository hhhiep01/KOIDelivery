using Application.Request.BoxType;
using Application.Request.KoiSize;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IBoxTypeService    
    {
        Task<ApiResponse> AddBoxTypeRequestAsync(BoxTypeRequest boxTypeRequest);
        Task<ApiResponse> GetAlBoxTypeAsync();
        Task<ApiResponse> DeleteBoxTypeAsync(int id);
        Task<ApiResponse> GetBoxTypeByIdAsync(int id);
        Task<ApiResponse> UpdateBoxTypeAsync(BoxTypeUpdateRequest boxTypeUpdateRequest);
    }
}
