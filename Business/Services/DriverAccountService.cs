using System.Collections.Generic;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WeVsVirus.Business.ViewModels;
using WeVsVirus.Models.Entities;
using WeVsVirus.DataAccess;
using WeVsVirus.Business.Utility;
using WeVsVirus.Business.Exceptions;
using WeVsVirus.Business.Services.EmailServices;
using WeVsVirus.DataAccess.Specifications;

namespace WeVsVirus.Business.Services
{
    public interface IDriverAccountService
    {
        Task<DriverAccount> CreateNewUserAsync(SignUpDriverViewModel model);
        Task<DriverAccount> GetAccountAsync(string userId);
    }

    public class DriverAccountService : IDriverAccountService
    {
        public DriverAccountService(
            UserManager<AppUser> userManager,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IAccountEmailService accountEmailService)
        {
            UnitOfWork = unitOfWork;
            UserManager = userManager;
            Mapper = mapper;
            AccountEmailService = accountEmailService;
        }
        private UserManager<AppUser> UserManager { get; }
        private IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; }
        private IAccountEmailService AccountEmailService { get; }

        public async Task<DriverAccount> GetAccountAsync(string userId)
        {
            var account = await UnitOfWork.Repository<DriverAccount>().GetByAsync(new DriverAccountSpecification(userId));
            if (account == null)
            {
                throw new NotFoundHttpException("User wurde nicht gefunden.");
            }
            return account;
        }

        public async Task<DriverAccount> CreateNewUserAsync(SignUpDriverViewModel model)
        {
            var driverAccount = Mapper.Map<DriverAccount>(model);
            driverAccount.AppUser.EmailConfirmed = true;
            // Need to wrap this in transaction since UserManager is not working properly
            // when AutoSaveChanges=false and called two times (AppUser and Role)
            // => results in foreign key constraint error for Account
            using (var transaction = UnitOfWork.DbContext.Database.BeginTransaction())
            {
                // TODO create random password
                var result = await UserManager.CreateAsync(driverAccount.AppUser, "RANDOMINITIALPASSWORD");
                await UnitOfWork.CompleteAsync();
                if (result.Succeeded)
                {
                    driverAccount.AppUserId = driverAccount.AppUser.Id;
                    driverAccount = await UnitOfWork.Repository<DriverAccount>().AddAsync(driverAccount);
                    result = await UserManager.AddToRoleAsync(driverAccount.AppUser, AccessRoles.DriverUser);
                    await UnitOfWork.CompleteAsync();
                    if (result.Succeeded)
                    {
                        try
                        {
                            // TODO Send email with token to driver
                            await AccountEmailService.SendDriverSignUpMailAsync(driverAccount);
                            await UnitOfWork.CompleteAsync();
                            await transaction.CommitAsync();
                            return driverAccount;
                        }
                        catch
                        {
                            throw new CreateNewUserException();
                        }
                    }
                }
                if (result.Errors.FirstOrDefault(err => err.Code == IdentityErrorCodes.DuplicateUserName || err.Code == IdentityErrorCodes.DuplicateEmail) != null)
                {
                    throw new UserNameAlreadyTakenException();
                }
                else
                {
                    throw new CreateNewUserException(result.Errors.FirstOrDefault()?.Description);
                }
            }
        }
    }
}