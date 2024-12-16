using Application.Request.BoxType;
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
    }
}
