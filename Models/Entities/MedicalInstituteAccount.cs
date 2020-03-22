using System.ComponentModel.DataAnnotations.Schema;
using WeVsVirus.Models.Enums;

namespace WeVsVirus.Models.Entities
{
    public class MedicalInstituteAccount : BaseEntity, IAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }

        [NotMapped]
        public AccountType AccountType => AccountType.HealthOffice;
    }
}
