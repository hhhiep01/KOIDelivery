using Application.Request.User;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUserAccountService
    {
        Task<ApiResponse> GetUserProfileAsync();
        Task<ApiResponse> UpdateUserProfileAsync(UpdateUserRequest updateUserRequest);
        Task<ApiResponse> GetAllAccountAsync();
    }
}
