using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using WeVsVirus.Models.Enums;

namespace WeVsVirus.Models.Entities
{
    public class SwabJob : BaseEntity
    {
        public int Id { get; set; }

        public DateTimeOffset CreationTime { get; set; }
        public DateTimeOffset? CompletionTime { get; set; }

        public SwabJobState State { get; set; }
        
        // TODO uncomment this and migrate

        // public PatientAccount PatientAccount { get; set; }
        // public int PatientAccountId { get; set; }
        public int? DriverAccountId { get; set; }
        public DriverAccount DriverAccount { get; set; }
    }
}
