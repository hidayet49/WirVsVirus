using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WeVsVirus.Models.Enums
{
    public enum AccountType
    {
        Patient = 0,
        HealthOffice = 1,
        Driver = 2
    }
}
