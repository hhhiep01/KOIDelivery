using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.Fish
{
    public class OrderFishRequest
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public float Weight { get; set; }
        public float Length { get; set; }
        public int? OrderId { get; set; }

        public IFormFile File { get; set; }
    }
}
