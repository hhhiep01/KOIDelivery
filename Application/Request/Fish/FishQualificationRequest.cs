using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.Fish
{
    public class FishQualificationRequest
    {
        public DateTime CreateAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int OrderFishId { get; set; }
        public string Name { get; set; }

        public IFormFile File { get; set; }
    }
}
