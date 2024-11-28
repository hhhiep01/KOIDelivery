using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.FishQualification
{
    public class FishQualificationRequest
    {
        public DateTime CreateAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ImageUrl { get; set; }
        public int OrderFishId { get; set; }
    }
}
