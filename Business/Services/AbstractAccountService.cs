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
using NetTopologySuite.Geometries;
using NetTopologySuite;

namespace WeVsVirus.Business.Services
{
    public interface IAccountService
    {
        (IAccount Account, string AccessRole) ConvertToAccount(ISignUpViewModel model);
        Task<IAccount> CreateNewUserAsync(IAccount account, string password, string accessRole);
    }

    public abstract class AbstractAccountService : IAccountService
    {
        public AbstractAccountService(
            UserManager<AppUser> userManager,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            UserManager = userManager;
            Mapper = mapper;
        }
        protected UserManager<AppUser> UserManager { get; }
        protected IUnitOfWork UnitOfWork { get; }
        protected IMapper Mapper { get; }

        public abstract (IAccount Account, string AccessRole) ConvertToAccount(ISignUpViewModel model);
        protected abstract Task CreateAccountAsync(IAccount account);

        public virtual async Task<IAccount> CreateNewUserAsync(IAccount account, string password, string accessRole)
        {
            // Need to wrap this in transaction since UserManager is not working properly
            // when AutoSaveChanges=false and called two times (AppUser and Role)
            // => results in foreign key constraint error for Account
            using (var transaction = UnitOfWork.DbContext.Database.BeginTransaction())
            {
                var result = await UserManager.CreateAsync(account.AppUser, password);
                await UnitOfWork.CompleteAsync();
                if (result.Succeeded)
                {
                    var address = UnitOfWork.Repository<Address>().AddAsync(account.Address);
                    account.AppUserId = account.AppUser.Id;
                    account.AddressId = address.Id;
                    await CreateAccountAsync(account);
                    result = await UserManager.AddToRoleAsync(account.AppUser, accessRole);
                    await UnitOfWork.CompleteAsync();
                    if (result.Succeeded)
                    {
                        try
                        {
                            await UnitOfWork.CompleteAsync();
                            await transaction.CommitAsync();
                            return account;
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

        protected static GeometryFactory GetGeometryFactory()
        {
            return NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        }
    }
}