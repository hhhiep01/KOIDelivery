using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response.Fish
{
    public class OrderFishResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public float Weight { get; set; }
        public float Length { get; set; }
        public string FishImgURL { get; set; }

        public int? OrderId { get; set; }

        public List<FishQualificationResponse> FishQualifications { get; set; }
        public List<FishHealthResponse> FishHealths { get; set; }
    }
}
