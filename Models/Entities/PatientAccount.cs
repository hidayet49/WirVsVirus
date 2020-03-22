using System;
using System.ComponentModel.DataAnnotations.Schema;
using WeVsVirus.Models.Enums;

namespace WeVsVirus.Models.Entities
{
    public class PatientAccount : BaseEntity, IAccount
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTimeOffset Birthday { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }

        [NotMapped]
        public AccountType AccountType => AccountType.Patient;

        [NotMapped]
        public string FullName => $"{Firstname} {Lastname}";
    }
}
