using WeVsVirus.Models.Entities;

namespace WeVsVirus.DataAccess.Specifications
{
    public class AddressSpecification : BaseSpecification<Address>
    {
        public AddressSpecification(int id)
            : base(address => address.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
        }
    }
}