using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.FishDetail
{
    public class FishDetailUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Weight { get; set; }
        public decimal Length { get; set; }
        public string FishImgURL { get; set; }
    }
}
