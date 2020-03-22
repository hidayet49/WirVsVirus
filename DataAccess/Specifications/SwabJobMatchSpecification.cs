using WeVsVirus.Models;
using WeVsVirus.Models.Entities;

namespace WeVsVirus.DataAccess.Specifications
{
    public class SwabJobMatchSpecification : BaseSpecification<SwabJobMatch>
    {
        public SwabJobMatchSpecification(SwabJob job)
               : base(jobMatch => jobMatch.SwabJobId == job.Id)
        {
            AddIncludes();
        }

        public SwabJobMatchSpecification(int driverAccountId, int jobId)
            : base(jobMatch => jobMatch.DriverAccountId == driverAccountId
            && jobMatch.SwabJobId == jobId)
        {
            AddIncludes();
        }

        public SwabJobMatchSpecification(DriverAccount driverAccount)
            : base(jobMatch => jobMatch.DriverAccountId == driverAccount.Id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            // TODO uncomment when patientaccount is available
            AddInclude(jobMatch => jobMatch.SwabJob);
            // AddInclude($"{nameof(SwabJobMatch.SwabJob)}.{nameof(SwabJob.PatientAccount)}");
            // AddInclude($"{nameof(SwabJobMatch.SwabJob)}.{nameof(SwabJob.PatientAccount)}.{nameof(PatientAccount.Address)}");
        }
    }
}