using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using WeVsVirus.Models.Entities;
using WeVsVirus.DataAccess.Repositories;
using WeVsVirus.Business.Exceptions;
using WeVsVirus.Business.Utility;
using WeVsVirus.Business.ViewModels;
using WeVsVirus.Business.Services;

namespace WeVsVirus.WebApp.Api
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        public AccountController(
            IAuthService authService)
        {
            AuthService = authService;
        }
        protected IAuthService AuthService { get; }

        [HttpPost("auth")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var jwt = await AuthService.LoginAsync(model);
                    return Ok(jwt);
                }
                catch (HttpStatusCodeException)
                {
                    throw;
                }
                catch
                {
                    throw new InternalServerErrorHttpException("Interner Serverfehler beim Einloggen.");
                }
            }
            return BadRequest(ModelState);
        }


        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await AuthService.ResetPasswordAsync(model);
                    return Ok();
                }
                catch (HttpStatusCodeException)
                {
                    throw;
                }
                catch
                {
                    throw new InternalServerErrorHttpException("Interner Serverfehler beim Bestätigen eines Tokens.");
                }
            }
            return BadRequest(ModelState);
        }
    }
}
