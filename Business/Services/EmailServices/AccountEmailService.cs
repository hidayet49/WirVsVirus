using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WeVsVirus.Business.Utility;
using WeVsVirus.Models.Entities;

namespace WeVsVirus.Business.Services.EmailServices
{
    public interface IAccountEmailService : IWeVsVirusEmailService
    {
        Task SendDriverSignUpMailAsync(DriverAccount account);
        Task SendRegistrationConfirmationMailForMedicalInstitutAsync(MedicalInstituteAccount account);
    }

    public class AccountEmailService : WeVsVirusEmailService, IAccountEmailService
    {

        public AccountEmailService(
            IEmailService emailService,
            EmailTemplateIdsConfiguration emailTemplateIds,
            FrontendConfiguration frontendConfiguration,
            UserManager<AppUser> userManager)
            : base(emailService, emailTemplateIds, frontendConfiguration)
        {
            UserManager = userManager;
        }
        private UserManager<AppUser> UserManager { get; }

        public async Task SendDriverSignUpMailAsync(DriverAccount account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            var passwordResetToken = await UserManager.GeneratePasswordResetTokenAsync(account.AppUser);
            passwordResetToken = Uri.EscapeDataString(passwordResetToken);

            var templateId = EmailTemplateIds.DriverSignUpConfirmationLink;
            var templateData = GetEmailBodyDataForDriverSignUpConfirmationLink(account, passwordResetToken);
            await EmailService.SendEmailWithSendGridTemplateAsync(account.AppUser.Email, $"{account.Firstname} {account.Lastname}", templateId, templateData);
        }


        public async Task SendRegistrationConfirmationMailForMedicalInstitutAsync(MedicalInstituteAccount account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            var token = await UserManager.GenerateEmailConfirmationTokenAsync(account.AppUser);
            token = Uri.EscapeDataString(token);

            var templateId = EmailTemplateIds.HealthOfficeSignUpConfirmationLink;
            var templateData = GetEmailBodyDataForMedicalInstituteSignUpConfirmationLink(account, token);
            await EmailService.SendEmailWithSendGridTemplateAsync(account.AppUser.Email, account.Name, templateId, templateData);
        }

        private dynamic GetEmailBodyDataForDriverSignUpConfirmationLink(DriverAccount account, string token)
        {
            return new
            {
                name = account.Firstname,
                url = $"{FrontendConfiguration.Url}driver-signup-confirmation?email={account.AppUser.UserName}&token={token}"
            };
        }

        private dynamic GetEmailBodyDataForMedicalInstituteSignUpConfirmationLink(MedicalInstituteAccount account, string token)
        {
            return new
            {
                nameOfHealthOffice = account.Name,
                url = $"{FrontendConfiguration.Url}medical-institute-signup-confirmation?email={account.AppUser.UserName}&token={token}"
            };
        }
    }
}