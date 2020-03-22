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
using WeVsVirus.Business.Utility;

namespace WeVsVirus.WebApp.Api
{
    [Route("api/[controller]")]
    public class SwabJobAccountController : Controller
    {
        public SwabJobAccountController(ISwabJobMatchService swabJobMatchService,
        UserManager<AppUser> userManager)
        {
            SwabJobMatchService = swabJobMatchService;
            UserManager = userManager;
        }

        private ISwabJobMatchService SwabJobMatchService { get; }

        private UserManager<AppUser> UserManager { get; }

        [HttpGet("forMe")]
        [Authorize(Policy = PolicyNames.DriverUserPolicy)]
        public virtual async Task<IActionResult> GetJobsForMe()
        {
            try
            {
                var userId = UserManager.GetUserId(User);
                var swabJobMatches = await SwabJobMatchService.GetJobMatchesForUserAsync(userId);
                return Ok(swabJobMatches);
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
        
        [HttpPost("[action]")]
        [Authorize(Policy = PolicyNames.DriverUserPolicy)]
        public async Task<IActionResult> Accept([FromBody] PrimitiveViewModel<int> jobId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = UserManager.GetUserId(User);
                    await SwabJobMatchService.AcceptJobAsync(jobId.Data, userId);
                    return Ok();
                }
                catch (HttpStatusCodeException)
                {
                    throw;
                }
                catch
                {
                    throw new InternalServerErrorHttpException("Interner Serverfehler beim Akzeptieren einer Bewerbung.");
                }
            }
            return BadRequest(ModelState);
        }
    }
}
