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
    public class PatientAccountController : AccountController
    {
        public PatientAccountController(
            IPatientAccountService patientAccountService,
            IAccountEmailService accountEmailService,
            IAuthService authService) : base(authService)
        {
            PatientAccountService = patientAccountService;
            AccountEmailService = accountEmailService;
        }

        private IAccountEmailService AccountEmailService { get; }
        private IPatientAccountService PatientAccountService { get; }

        [HttpPost("")]
        [HttpPost("[action]")]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> NewUser([FromBody] SignUpPatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    (IAccount account, string accessRole) = PatientAccountService.ConvertToAccount(model);
                    account = await PatientAccountService.CreateNewUserAsync(account, model.Password, accessRole);
                    await AccountEmailService.SendRegistrationConfirmationMailForPatientAsync(account as PatientAccount);
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
