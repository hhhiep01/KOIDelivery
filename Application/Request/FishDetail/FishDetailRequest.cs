using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.FishDetail
{
    public class FishDetailRequest
    {
        public string Name { get; set; }
        public int Age { get; set; }
       
        public float Length { get; set; }
        public int? OrderItemId { get; set; }

        public IFormFile File { get; set; }
    }
}
