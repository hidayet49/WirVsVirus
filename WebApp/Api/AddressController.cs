using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Web;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using WeVsVirus.Business.Utility;
using WeVsVirus.Business.Exceptions;
using WeVsVirus.Models.Entities;
using WeVsVirus.Business.ViewModels;
using WeVsVirus.Business.Services;

namespace WeVsVirus.WebApp.Api
{
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        public AddressController(
            UserManager<AppUser> userManager,
            IAddressService addressService)
        {
            UserManager = userManager;
            AddressService = addressService;
        }

        protected IAddressService AddressService { get; }

        protected UserManager<AppUser> UserManager { get; }

        // [HttpPost("")]
        // [HttpPost("[action]")]
        // public async Task<IActionResult> ChangeMyAddress([FromBody] AddressViewModel model)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             var userId = UserManager.GetUserId(User);
        //             await AddressService.ChangeUsersAddressAsync(userId, model);
        //             return Ok();
        //         }
        //         catch (HttpStatusCodeException)
        //         {
        //             throw;
        //         }
        //         catch
        //         {
        //             throw new InternalServerErrorHttpException("Interner Serverfehler beim Speichern der Adresse");
        //         }
        //     }
        //     return BadRequest(ModelState);
        // }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> SuggestAddresses([FromBody] AddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var addresses = await AddressService.SuggestAddressesAsync(model);
                    return Ok(addresses);
                }
                catch (HttpStatusCodeException)
                {
                    throw;
                }
                catch
                {
                    throw new InternalServerErrorHttpException("Interner Serverfehler beim Suchen der Adresse");
                }
            }
            return BadRequest(ModelState);
        }
    }
}
