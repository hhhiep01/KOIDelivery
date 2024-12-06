using Application.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GoogleMapService : IGoogleMapService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public GoogleMapService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<string> GetCurrentLocationAsync()
        {
            var apiKey = _config["GoogleMapAPI:Key"];
            var url = $"https://www.gomaps.pro/geolocation/v1/geolocate?key={apiKey}";

            var request = new { considerIp = true };
            var response = await _httpClient.PostAsJsonAsync(url, request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }

            return null;
        }
        public async Task<string> GetDistanceAsync(string origin, string destination)
        {
            var apiKey = _config["GoogleMapAPI:Key"];
            string url = $"https://maps.gomaps.pro/maps/api/distancematrix/json?origins={Uri.EscapeDataString(origin)}&destinations={Uri.EscapeDataString(destination)}&key={apiKey}";

            
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content; // Trả về JSON kết quả
            }

            return null;
        }


    }
}
