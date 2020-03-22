using Newtonsoft.Json;
using System;

namespace WeVsVirus.Models.Entities
{
    public class SwabJobMatch : BaseEntity
    {
        public int DriverAccountId { get; set; }

        public DriverAccount DriverAccount { get; set; }

        public int SwabJobId { get; set; }

        public SwabJob SwabJob { get; set; }

        public DateTimeOffset MatchTimeStamp { get; set; }
    }
}
