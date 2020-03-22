using WeVsVirus.Models.Entities;

namespace WeVsVirus.DataAccess.Specifications
{
    public class SwabJobSpecification : BaseSpecification<SwabJob>
    {
        public SwabJobSpecification(int id)
            : base(job => job.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
        }
    }
}