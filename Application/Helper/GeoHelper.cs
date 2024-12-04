using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helper
{
    public class GeoHelper
    {
        private readonly HttpClient _httpClient;
        public GeoHelper() 
        {
            _httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(5)
            };
        }

        private async Task<string> GetIPAddress()
        {
            var ipAddress = await _httpClient.GetAsync($"http://ipinfo.io/ip");
            if (ipAddress.IsSuccessStatusCode)
            {
                var json = await ipAddress.Content.ReadAsStringAsync();
                return json.Trim();
            }
            return string.Empty;
        }

        public async Task<string> GetGeoInfo()
        {
            var ipAddress = await GetIPAddress();

            var response = await _httpClient.GetAsync($"http://api.ipstack.com/" + ipAddress + "?access_key=fa09a35335484732f7abdc26cc7b74f6");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return json;
            }
            return null;
        }
    }
}
