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
using WeVsVirus.Business.Services.EmailServices;
using NetTopologySuite.Geometries;

namespace WeVsVirus.Business.Services
{
    public interface IMedicalInstituteAccountService : IAccountService
    {
    }

    public class MedicalInstituteAccountService : AbstractAccountService, IMedicalInstituteAccountService
    {
        public MedicalInstituteAccountService(
            UserManager<AppUser> userManager,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IAccountEmailService accountEmailService)
            : base(userManager, mapper, unitOfWork)
        {
            AccountEmailService = accountEmailService;
        }
        private IAccountEmailService AccountEmailService { get; }

        public override (IAccount Account, string AccessRole) ConvertToAccount(ISignUpViewModel model)
        {
            var account = Mapper.Map<MedicalInstituteAccount>(model as SignUpMedicalInstituteViewModel);

            GeometryFactory geometryFactory = GetGeometryFactory();
            account.Address.GeoLocation = geometryFactory.CreatePoint(new Coordinate(model.Address.Lng, model.Address.Lat));
            return ((IAccount) account, AccessRoles.HealthOfficeUser);
        }

        protected async override Task CreateAccountAsync(IAccount account)
        {
            await UnitOfWork.Repository<MedicalInstituteAccount>().AddAsync(account as MedicalInstituteAccount);
        }
    }
}