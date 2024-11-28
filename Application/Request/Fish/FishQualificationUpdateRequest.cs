using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.Fish
{
    public class FishQualificationUpdateRequest
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ImageUrl { get; set; }
    }
}
