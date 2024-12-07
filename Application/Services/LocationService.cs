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
    public class LocationService : ILocationService
    {
        public void ProcessLocation(double latitude, double longitude)
        {
            Console.WriteLine($"[Service] Processing location: Latitude = {latitude}, Longitude = {longitude}");
        }
    }
}
