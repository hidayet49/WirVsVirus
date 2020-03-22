using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeVsVirus.Business.ViewModels;
using WeVsVirus.Models.Entities;

namespace WeVsVirus.Business.Mappers
{
    public class WeVsVirusMapperProfile : Profile
    {
        public WeVsVirusMapperProfile()
        {
            CreateMap<SignUpDriverViewModel, DriverAccount>()
                .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => new AppUser
                {
                    Email = src.Email,
                    UserName = src.Email
                }));

            CreateMap<SignUpMedicalInstituteViewModel, MedicalInstituteAccount>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.MedicalInstituteName))
            .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => new AppUser
            {
                Email = src.Email,
                UserName = src.Email
            }));

            CreateMap<Address, AddressViewModel>()
            .ForMember(dest => dest.Lng, opt => opt.MapFrom(src => src.GeoLocation.Coordinate.X))
            .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.GeoLocation.Coordinate.Y));

            CreateMap<AddressViewModel, Address>();
        }
    }
}
