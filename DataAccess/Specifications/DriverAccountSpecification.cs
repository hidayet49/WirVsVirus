using WeVsVirus.Models.Entities;

namespace WeVsVirus.DataAccess.Specifications
{
    public class DriverAccountSpecification : BaseSpecification<DriverAccount>
    {
        public DriverAccountSpecification(string userId)
            : base(account => account.AppUserId == userId)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
        }
    }
}