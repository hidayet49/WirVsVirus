using NetTopologySuite.Geometries;

namespace WeVsVirus.Models.Entities
{
    public class Address : BaseEntity
    {
        public int Id { get; set; }
        public string StreetAndNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public Point GeoLocation { get; set; }
    }
}
