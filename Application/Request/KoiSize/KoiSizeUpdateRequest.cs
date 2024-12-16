using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.KoiSize
{
    public class KoiSizeUpdateRequest
    {
        public int Id { get; set; }
        public float SizeCmMin { get; set; }
        public float SizeCmMax { get; set; }
        public float SizeInchMin { get; set; }
        public float SizeInchMax { get; set; }
        public int SpaceRequired { get; set; }
    }
}
