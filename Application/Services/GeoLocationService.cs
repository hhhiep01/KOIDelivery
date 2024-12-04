using Application.Helper;
using Application.Interface;
using MaxMind.GeoIP2;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GeoLocationService : IGeoLocationService
    {
        private readonly HttpClient _httpClient;
        private readonly GeoHelper _geoHelper;

        public GeoLocationService(HttpClient httpClient, GeoHelper geoHelper)
        {
            _httpClient = httpClient;
            _geoHelper = geoHelper;
        }

        //public GeoLocation GetLocationFromIp(string ipAddress)
        //{
        //    using (var reader = new DatabaseReader(_geoLite2DbPath)) 
        //    {
        //        try
        //        {
        //            var city = reader.City(ipAddress);
        //            return new GeoLocation
        //            {
        //                Country = city.Country.Name,
        //                City = city.City.Name,
        //                Latitude = city.Location.Latitude,
        //                Longitude = city.Location.Longitude
        //            };
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //}

        public async Task<string> GetGeoInfo()
        {
            return await _geoHelper.GetGeoInfo();
        }
    }

    public class GeoLocation
    {
        public string Country { get; set; }
        public string City { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
