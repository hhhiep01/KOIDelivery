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
        public float PricePerKm { get; set; }
        public float PricePerKg { get; set; }
        public float PricePerAmount { get; set; }
        
        public bool IsActive { get; set; }
        //
        public List<Order> Orders { get; set; }
    }
    public enum TransportType
    {
        Road,
        Air
    }
}
