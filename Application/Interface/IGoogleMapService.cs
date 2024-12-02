using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IGoogleMapService
    {
        Task<string> GetCurrentLocationAsync();
        Task<string> GetDistanceAsync(string origin, string destination);
    }
}
