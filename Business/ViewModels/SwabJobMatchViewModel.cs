using System;

namespace WeVsVirus.Business.ViewModels
{
    public class SwabJobMatchViewModel
    {
        public int Id { get; set; } // this is swab job id, not swab job match
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string StreetAndNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public double Lng { get; set; }
        public double Lat { get; set; }
    }
}