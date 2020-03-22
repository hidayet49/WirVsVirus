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
    public class DriverAccountController : AccountController
    {
        public DriverAccountController(
            IDriverAccountService driverAccountService,
            IAuthService authService) : base(authService)
        {
            DriverAccountService = driverAccountService;
        }
        private IDriverAccountService DriverAccountService { get; }

        [HttpPost("[action]")]
        // TODO only allow for health officess when health office sign up implementation is ready
        // [Authorize(Policy = PolicyNames.HealthOfficeUserPolicy)]
        [AllowAnonymous]
        public virtual async Task<IActionResult> NewUser([FromBody] SignUpDriverViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var account = await DriverAccountService.CreateNewUserAsync(model);
                    return Ok();
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
