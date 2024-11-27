using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class OrderFish : Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public float Weight { get; set; }
        public float Length { get; set; }
        public string FishImgURL { get; set; }
        //
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
        public List<FishQualification> FishQualifications { get; set; }
        public List<FishHealth> FishHealths { get; set; }
    }
}
