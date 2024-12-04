using Application.Interface;
using Application.Request.User;
using Application.Response;
using Application.Response.UserAccount;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserAccountService : IUserAccountService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IClaimService _claim;
        public UserAccountService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claim)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _claim = claim;
        }
        public async Task<ApiResponse> GetUserProfileAsync()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var claim = _claim.GetUserClaim();
                var user = await _unitOfWork.UserAccounts.GetAsync(x => x.Id == claim.Id);
                var userResponse = _mapper.Map<UserProfileResponse>(user);
                return apiResponse.SetOk(userResponse);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> UpdateUserProfileAsync(UpdateUserRequest updateUserRequest)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var claim = _claim.GetUserClaim();
                var user = await _unitOfWork.UserAccounts.GetAsync(x => x.Id == claim.Id);
                _mapper.Map(updateUserRequest,user);
                return apiResponse.SetOk("Update Success");
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

    }
}
