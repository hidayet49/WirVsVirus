using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WeVsVirus.Models.Entities;

namespace WeVsVirus.Business.Services.GeoServices
{
    public class GoogleMapsApiService
    {

        private static string googleMapsUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=";
        private static string urlParameters = "&sensor=false&language=de-DE";
        private static string apiKeyParameterName = "&key=";
        public GoogleMapsApiService(IConfiguration configuration)
        {
            ApiKey = configuration["GoogleMapsApiKey"];
        }
        private string ApiKey { get; set; }
        public async Task<GoogleMapsResponse> GetLatLongAsync(Address address)
        {
            string url = $"{googleMapsUrl}{address.StreetAndNumber},+{address.ZipCode}{urlParameters}{apiKeyParameterName}{ApiKey}";
            WebRequest request = WebRequest.Create(new Uri(url));
            using (WebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    var googleResponse = JsonConvert.DeserializeObject<GoogleMapsResponse>(await reader.ReadToEndAsync());
                    return googleResponse;
                }
            }
        }
    }

    public class GoogleMapsResponse
    {
        public string Status { get; set; }
        public GoogleMapsResult[] Results { get; set; }

        public class GoogleMapsResult
        {
            public string Formatted_Address { get; set; }
            public string Location_Type { get; set; }
            public string[] Types { get; set; }
            public GoogleMapsAddressComponents[] Address_Components { get; set; }

            public GoogleMapsGeometry Geometry { get; set; }

            public class GoogleMapsGeometry
            {
                public GoogleMapsLocation Location { get; set; }

                public class GoogleMapsLocation
                {
                    public double Lat { get; set; }
                    public double Lng { get; set; }
                }
            }
            public class GoogleMapsAddressComponents
            {
                public string Long_Name { get; set; }
                public string Short_Name { get; set; }
                public string[] Types { get; set; }
            }
        }
    }
}