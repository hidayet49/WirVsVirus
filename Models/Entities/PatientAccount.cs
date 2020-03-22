using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WeVsVirus.Models.Entities
{
    public class PatientAccount: BaseEntity
    {
        public int Id { get; set; }

        public string  Givenname { get; set; }

        public string  Lastname { get; set; }

        public string Address { get; set; }

        public string AdditionalAddress { get; set; }

        public string ZipCode { get; set; }

        public string Location { get; set; }

        [NotMapped]
        public AccountType AccountType => AccountType.Patient;

    }
}
