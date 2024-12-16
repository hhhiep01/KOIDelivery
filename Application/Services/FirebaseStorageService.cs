using Application.Interface;
using Domain.Entity;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Application.Services
{
    public class FirebaseStorageService : IFirebaseStorageService
    {
        private readonly IConfiguration _config;

        public FirebaseStorageService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> UploadUserImage(string userName, IFormFile file)
        {
            string firebaseBucket = _config["Firebase:Bucket"];

            var firebaseStorage = new FirebaseStorage(firebaseBucket);

            string fileName =  $"{Guid.NewGuid().ToString()}_{Path.GetFileName(file.FileName)}";

            if (userName.EndsWith("/"))
            {
                userName = userName.TrimEnd('/') ;
            }

            fileName = fileName.Replace("/", "-");

            var task = firebaseStorage.Child("UserAccounts").Child(userName).Child(fileName);

            var stream = file.OpenReadStream();
            await task.PutAsync(stream);

            return await task.GetDownloadUrlAsync();
        }

        public async Task<string> UploadOrderFishUrl(string orderFish, IFormFile file)
        {
            string firebaseBucket = _config["Firebase:Bucket"];

            var firebaseStorage = new FirebaseStorage(firebaseBucket);

            string fileName = $"{Guid.NewGuid().ToString()}_{Path.GetFileName(file.FileName)}";

            if (orderFish.EndsWith("/"))
            {
                orderFish = orderFish.TrimEnd('/');
            }

            fileName = fileName.Replace("/", "-");

            var task = firebaseStorage.Child("OrderFishes").Child(orderFish).Child(fileName);

            var stream = file.OpenReadStream();
            await task.PutAsync(stream);

            return await task.GetDownloadUrlAsync();
        }

        public async Task<string> UploadFishQualificationUrl(string fishQualification, IFormFile file)
        {
            string firebaseBucket = _config["Firebase:Bucket"];

            var firebaseStorage = new FirebaseStorage(firebaseBucket);

            string fileName = $"{Guid.NewGuid().ToString()}_{Path.GetFileName(file.FileName)}";

            if (fishQualification.EndsWith("/"))
            {
                fishQualification = fishQualification.TrimEnd('/');
            }

            fileName = fileName.Replace("/", "-");

            var task = firebaseStorage.Child("FishQualifications").Child(fishQualification).Child(fileName);

            var stream = file.OpenReadStream();
            await task.PutAsync(stream);

            return await task.GetDownloadUrlAsync();
        }

        public async Task<string> UploadFishDetailUrl(string fishDetail, IFormFile file)
        {
            string firebaseBucket = _config["Firebase:Bucket"];

            var firebaseStorage = new FirebaseStorage(firebaseBucket);

            string fileName = $"{Guid.NewGuid().ToString()}_{Path.GetFileName(file.FileName)}";

            if (fishDetail.EndsWith("/"))
            {
                fishDetail = fishDetail.TrimEnd('/');
            }

            fileName = fileName.Replace("/", "-");

            var task = firebaseStorage.Child("FishDetails").Child(fishDetail).Child(fileName);

            var stream = file.OpenReadStream();
            await task.PutAsync(stream);
            
            return await task.GetDownloadUrlAsync();
        }
    }
}
