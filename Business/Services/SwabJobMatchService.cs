using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WeVsVirus.Business.Exceptions;
using WeVsVirus.Business.Services.GeoServices;
using WeVsVirus.Business.ViewModels;
using WeVsVirus.DataAccess;
using WeVsVirus.DataAccess.Specifications;
using WeVsVirus.Models.Entities;
using WeVsVirus.Models.Enums;

namespace WeVsVirus.Business.Services
{
    public interface ISwabJobMatchService
    {
        Task<List<SwabJobMatchViewModel>> GetJobMatchesForUserAsync(string userId);
        Task AcceptJobAsync(int jobId, string userId);
    }

    public class SwabJobMatchService : ISwabJobMatchService
    {
        public SwabJobMatchService(
            IUnitOfWork unitOfWork,
            IDriverAccountService driverAccountService,
            IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            DriverAccountService = DriverAccountService;
        }
        public IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; set; }
        private IDriverAccountService DriverAccountService { get; }

        public async Task<List<SwabJobMatchViewModel>> GetJobMatchesForUserAsync(string userId)
        {
            var account = await DriverAccountService.GetAccountAsync(userId);
            var jobMatches = await UnitOfWork.Repository<SwabJobMatch>().GetByAsync(new SwabJobMatchSpecification(account));
            return Mapper.Map<List<SwabJobMatchViewModel>>(jobMatches);
        }

        public async Task AcceptJobAsync(int jobId, string userId)
        {
            var account = await DriverAccountService.GetAccountAsync(userId);
            var job = await UnitOfWork.Repository<SwabJob>().GetByAsync(new SwabJobSpecification(jobId));
            if (job == null)
            {
                throw new NotFoundHttpException("Job");
            }
            if (job.DriverAccountId != null)
            {
                throw new ConflictHttpException("Dieser Job wurde bereits von einem Fahrer angenommen");
            }
            if (job.State != SwabJobState.Open)
            {
                throw new ConflictHttpException("Dieser Job steht nicht mehr zur Verfügung.");
            }

            var jobMatchesRepository = UnitOfWork.Repository<SwabJobMatch>();
            var allJobMatchesToThisJob = await jobMatchesRepository.ListAsync(new SwabJobMatchSpecification(job));
            jobMatchesRepository.DeleteRange(allJobMatchesToThisJob);
            job.DriverAccountId = account.Id;
            job.State = SwabJobState.Assigned;
            await UnitOfWork.CompleteAsync();
            // TODO notify all drivers of allJobMatchesToThisJob to fetch new state
            // TODO notify user that a driver accepted his job
        }

        public async Task CompleteJobAsync(int jobId, string userId)
        {
            var account = await DriverAccountService.GetAccountAsync(userId);
            var job = await UnitOfWork.Repository<SwabJob>().GetByAsync(new SwabJobSpecification(jobId));
            if (job == null)
            {
                throw new NotFoundHttpException("Job");
            }
            if (job.DriverAccountId != account.Id)
            {
                throw new ConflictHttpException("Dieser Job ist nicht dir zugewiesen.");
            }
            if (job.State != SwabJobState.Assigned)
            {
                throw new ConflictHttpException("Dieser Job steht nicht mehr zur Verfügung.");
            }
            job.State = SwabJobState.Complete;
            job.CompletionTime = DateTimeOffset.Now;

            var jobMatchesRepository = UnitOfWork.Repository<SwabJobMatch>();
            var allJobMatchesToThisJob = await jobMatchesRepository.ListAsync(new SwabJobMatchSpecification(job));
            jobMatchesRepository.DeleteRange(allJobMatchesToThisJob);
            job.DriverAccountId = account.Id;
            await UnitOfWork.CompleteAsync();
            // TODO notify user that a driver completed the job
        }
    }
}