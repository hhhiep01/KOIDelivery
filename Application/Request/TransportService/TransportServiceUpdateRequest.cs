﻿using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.TransportService
{
    public class TransportServiceUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TransportType TransportType { get; set; }
        public string Description { get; set; }
        public double TransportPrice { get; set; }
        public double PricePerKg { get; set; }
        public double PricePerAmount { get; set; }
        public string FromProvince { get; set; }
        public string ToProvince { get; set; } 
        public bool IsActive { get; set; }
    }
}
