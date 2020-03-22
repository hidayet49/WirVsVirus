using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;
using System.Threading.Tasks;
using WeVsVirus.Business.Services.EmailServices;
using WeVsVirus.Business.Utility;
using WeVsVirus.Business.ViewModels;
using WeVsVirus.DataAccess;
using WeVsVirus.Models.Entities;

namespace WeVsVirus.Business.Services
{
    public interface IPatientAccountService : IAccountService {}

    public class PatientAccountService : AbstractAccountService, IPatientAccountService
    {
        public PatientAccountService(
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
            var account = Mapper.Map<PatientAccount>(model as SignUpPatientViewModel);

            GeometryFactory geometryFactory = GetGeometryFactory();
            account.Address.GeoLocation = geometryFactory.CreatePoint(new Coordinate(model.Address.Lng, model.Address.Lat));
            return (account, AccessRoles.WebClientUser);
        }

        protected async override Task CreateAccountAsync(IAccount account)
        {
            await UnitOfWork
                .Repository<PatientAccount>()
                .AddAsync(account as PatientAccount);
        }

    }
}