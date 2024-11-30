using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response.Fish
{
    public class FishQualificationResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ImageUrl { get; set; }
    }
}
