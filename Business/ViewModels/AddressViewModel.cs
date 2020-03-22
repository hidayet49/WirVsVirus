using System.ComponentModel.DataAnnotations;

namespace WeVsVirus.Business.ViewModels
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Straße und Hausnummer fehlen.")]
        public string StreetAndNumber { get; set; }

        [Required(ErrorMessage = "Postleitzahl ist ungültig.")]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Ortsangabe fehlt.")]
        public string City { get; set; }
        public double Lng { get; set; }
        public double Lat { get; set; }
    }
}
