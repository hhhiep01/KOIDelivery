using Domain.Entity;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FirebaseStorageService
    {
        private readonly IConfiguration _config;

        public FirebaseStorageService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> UploadUserImage(string username, IFormFile file)
        {
            string firebaseBucket = _config["Firebase:Bucket"];

            var firebaseStorage = new FirebaseStorage(firebaseBucket);

            string fileName =  file.FileName;

            var task = firebaseStorage.Child("Users").Child(username).Child(fileName);

            var stream = file.OpenReadStream();
            await task.PutAsync(stream);

            return await task.GetDownloadUrlAsync();
        }


    }
}
