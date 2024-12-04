using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IFirebaseStorageService
    {
        Task<string> UploadUserImage(string userName, IFormFile file);

        Task<string> UploadOrderFishUrl(string orderFish, IFormFile file);

        Task<string> UploadFishQualificationUrl(string fishQualification, IFormFile file);
    }
}
