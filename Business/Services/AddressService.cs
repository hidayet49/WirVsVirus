using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WeVsVirus.Business.Exceptions;
using WeVsVirus.Business.Services.GeoServices;
using WeVsVirus.Business.ViewModels;
using WeVsVirus.DataAccess;
using WeVsVirus.Models.Entities;

namespace WeVsVirus.Business.Services
{
    public interface IAddressService
    {
        Task<List<AddressViewModel>> SuggestAddressesAsync(AddressViewModel address);
    }

    public class AddressService : IAddressService
    {
        public AddressService(IUnitOfWork unitOfWork,
        IMapper mapper,
        IGeoLocationService geoLocationService)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            GeoLocationService = geoLocationService;
        }
        public IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; set; }
        private IGeoLocationService GeoLocationService { get; }

        public async Task<List<AddressViewModel>> SuggestAddressesAsync(AddressViewModel address)
        {
            var addressToSearchFor = Mapper.Map<Address>(address);

            var addresses = await GeoLocationService.GetRelatedAddressesWithApiAsync(addressToSearchFor);
            return Mapper.Map<List<AddressViewModel>>(addresses);

        }

        // public async Task ChangeUsersAddressAsync(string userId, AddressViewModel address)
        // {
        //     address.AppUserId = userId;
        //     var newAddress = Mapper.Map<Address>(address);

        //     var addressRepository = UnitOfWork.Repository<Address>();
        //     var existingAddress = await addressRepository.GetByAsync(new AddressSpecification(userId));

        //     if (existingAddress == null)
        //     {
        //         newAddress = await GeoLocationService.FillOutAddressWithApiAsync(newAddress);
        //         await addressRepository.AddAsync(newAddress);
        //     }
        //     else if (IsAddressDifferent(existingAddress, newAddress))
        //     {
        //         existingAddress.City = newAddress.City;
        //         existingAddress.StreetAndNumber = newAddress.StreetAndNumber;
        //         existingAddress.ZipCode = newAddress.ZipCode;
        //         newAddress = await GeoLocationService.FillOutAddressWithApiAsync(existingAddress);
        //     }
        //     await UnitOfWork.CompleteAsync();
        // }

        // private static bool IsAddressDifferent(Address addressA, Address addressB)
        // {
        //     return string.Compare(addressB.City, addressA.City, true) != 0
        //     || string.Compare(addressB.StreetAndNumber, addressA.StreetAndNumber, true) != 0
        //     || string.Compare(addressB.ZipCode, addressA.ZipCode, true) != 0;
        // }
    }
}