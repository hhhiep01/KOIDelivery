using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class FishDetail: Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Length { get; set; }
        public string FishImgURL { get; set; }
        //
        public int? OrderItemId { get; set; }
        public OrderItem? OrderItem { get; set; }
        public List<FishQualification> FishQualifications { get; set; }
        public List<FishHealth> FishHealths { get; set; }
       
    }
}
