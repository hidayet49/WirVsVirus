using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using WeVsVirus.Models.Entities;
using WeVsVirus.DataAccess.Repositories;
using WeVsVirus.Business.Exceptions;
using WeVsVirus.Business.ViewModels;
using WeVsVirus.Business.Services;
using WeVsVirus.Business.Services.EmailServices;

namespace WeVsVirus.WebApp.Api
{
    [Route("api/[controller]")]
    public class MedicalInstituteAccountController : AccountController
    {
        public MedicalInstituteAccountController(
            IMedicalInstituteAccountService medicalInstituteAccountService,
            IAccountEmailService accountEmailService,
            IAuthService authService) : base(authService)
        {
            MedicalInstituteAccountService = medicalInstituteAccountService;
            AccountEmailService = accountEmailService;
        }
        private IMedicalInstituteAccountService MedicalInstituteAccountService { get; }
        private IAccountEmailService AccountEmailService { get; }

        [HttpPost("")]
        [HttpPost("[action]")]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> NewUser([FromBody] SignUpMedicalInstituteViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    (IAccount account, string accessRole) = MedicalInstituteAccountService.ConvertToAccount(model);
                    account = await MedicalInstituteAccountService.CreateNewUserAsync(account, model.Password, accessRole);
                    await AccountEmailService.SendRegistrationConfirmationMailForMedicalInstitutAsync(account as MedicalInstituteAccount);
                    return Ok();
                }
                catch (UserNameAlreadyTakenException e)
                {
                    ModelState.AddModelError("Email", e.Message);
                }
                catch (HttpStatusCodeException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw new InternalServerErrorHttpException(e);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
