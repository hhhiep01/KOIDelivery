﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class FishQualification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        
        //public string Name { get; set; }
       

        public FishDetail FishDetail { get; set; }
        public int FishDetailId { get; set; }
    }
}
