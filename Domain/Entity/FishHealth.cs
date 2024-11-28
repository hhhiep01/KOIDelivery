﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class FishHealth
    {
        public int Id { get; set; } 
        public string HealthStatus { get; set; } 
        public DateTime CheckDate { get; set; } 
        public string Notes { get; set; }
        public string Temperature { get; set; }
        public string WaterQuality { get; set; } 
        public string Behavior { get; set; }
        //
        public OrderFish? OrderFish { get; set; }
        public int? OrderFishId { get; set; }
    }
}