using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WeVsVirus.Models.Enums;

namespace WeVsVirus.Models.Entities
{
    public class DriverAccount : BaseEntity
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTimeOffset Birthday { get; set; }
        public string ExpoToken { get; set; }
        public ICollection<SwabJobMatch> SwabJobMatches { get; set; } = new List<SwabJobMatch>();
        public ICollection<SwabJob> SwabJobs { get; set; } = new List<SwabJob>();

        [NotMapped]
        public AccountType AccountType => AccountType.Driver;
    }
}
