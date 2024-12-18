﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class TransportService : Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TransportType TransportType { get; set; }
        public string Description { get; set; }
        public decimal? TransportPrice { get; set; }
        public decimal? PricePerKm { get; set; }
        //public decimal PricePerAmount { get; set; }
        public string? FromProvince { get; set; } = string.Empty;
        public string? ToProvince { get; set; } = string.Empty;

        public bool IsActive { get; set; }
        //
        public List<Order> Orders { get; set; }
    }
    public enum TransportType
    {
        Local,        
        Domestic,    
        International 
    }
    
}
