using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeVsVirus.Business.ViewModels
{
    public class SignUpMedicalInstituteViewModel: ISignUpViewModel
    {
        [Required(ErrorMessage = "Institutname fehlt")]
        public string MedicalInstituteName { get; set; }

        [Required(ErrorMessage = "E-Mail-Adresse fehlt")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Passwort fehlt.")]
        [StringLength(100, ErrorMessage = "Das Passwort muss aus mindestens sechs Zeichen bestehen", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Die Passwörter stimmen nicht überein")]
        public string ConfirmPassword { get; set; }
        public AddressViewModel Address { get; set; }

    }
}
