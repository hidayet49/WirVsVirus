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
    public class TomTomApiService
    {
        private static string tomTomUrl = "https://api.tomtom.com/search/2/geocode/";
        private static string urlParameters = ".json?countrySet=DE&language=de-DE";
        private static string apiKeyParameterName = "&key=";
        public TomTomApiService(IConfiguration configuration)
        {
            ApiKey = configuration["TomTomApiKey"];
        }
        private string ApiKey { get; set; }
        public async Task<TomTomResponse> GetLatLongAsync(Address address)
        {
            string url = $"{tomTomUrl}{address.StreetAndNumber},+{address.ZipCode}{urlParameters}{apiKeyParameterName}{ApiKey}";
            WebRequest request = WebRequest.Create(new Uri(url));
            using (WebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    var tomTomResponse = JsonConvert.DeserializeObject<TomTomResponse>(await reader.ReadToEndAsync());
                    return tomTomResponse;
                }
            }
        }
    }

    public class TomTomResponse
    {
        public TomTomResult[] Results { get; set; }

        public class TomTomResult
        {
            public TomTomAddress Address { get; set; }

            public string Type { get; set; }

            public TomTomPosition Position { get; set; }

            public class TomTomAddress
            {
                public string StreetNumber { get; set; }
                public string StreetName { get; set; }
                public string postalCode { get; set; }
                public string LocalName { get; set; }
                public string Country { get; set; }
                public string FreeFormAddress { get; set; }
            }

            public class TomTomPosition
            {
                public double Lat { get; set; }
                public double Lon { get; set; }
            }
        }
    }
}