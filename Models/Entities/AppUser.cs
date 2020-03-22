using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WeVsVirus.Models.Entities
{
    public class AppUser : IdentityUser
    {
        [NotMapped]
        public ICollection<string> Roles { get; set; } = new List<string>();
    }
}
