using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response.TransportService
{
    public class TransportServiceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TransportType TransportType { get; set; }
        public string Description { get; set; }
        public decimal TransportPrice { get; set; }
        //public decimal PricePerKm { get; set; }
        //public decimal PricePerKg { get; set; }
        //public decimal PricePerAmount { get; set; }
        public string FromProvince { get; set; } 
        public string ToProvince { get; set; } 
        public bool IsActive { get; set; }
    }
}
