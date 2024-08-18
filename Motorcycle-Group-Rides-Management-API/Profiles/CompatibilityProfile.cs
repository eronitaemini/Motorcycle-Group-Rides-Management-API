using AutoMapper;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Models;
using static Motorcycle_Group_Rides_Management_API.Dtos.FeedbackDto;

namespace Motorcycle_Group_Rides_Management_API.Profiles
{
    public class CompatibilityProfile : Profile
    {
        public CompatibilityProfile() {

            //     CreateMap<CreateCompatibilityDto, Compatibility>().ReverseMap();
            //            CreateMap<Compatibility, UpdateCompatibilityDto>().ReverseMap();

            CreateMap<CreateCompatibilityDto, Compatibility>()
           .ForMember(dest => dest.MotorcycleType, opt => opt.MapFrom(src => src.MotorcycleType))
           .ForMember(dest => dest.RouteType, opt => opt.MapFrom(src => src.RouteType))
           .ForMember(dest => dest.CompatibilityLevel, opt => opt.MapFrom(src => src.CompatibilityLevel));

            // Map between UpdateCompatibilityDto and Compatibility
            CreateMap<UpdateCompatibilityDto, Compatibility>()
                .ForMember(dest => dest.MotorcycleType, opt => opt.MapFrom(src => src.MotorcycleType))
                .ForMember(dest => dest.RouteType, opt => opt.MapFrom(src => src.RouteType))
                .ForMember(dest => dest.CompatibilityLevel, opt => opt.MapFrom(src => src.CompatibilityLevel));

            // Map between Compatibility and CompatibilityDto
            CreateMap<Compatibility, CompatibilityDto>()
                .ForMember(dest => dest.MotorcycleType, opt => opt.MapFrom(src => src.MotorcycleType.ToString()))
                .ForMember(dest => dest.RouteType, opt => opt.MapFrom(src => src.RouteType.ToString()))
                .ForMember(dest => dest.CompatibilityLevel, opt => opt.MapFrom(src => src.CompatibilityLevel.ToString()));



        }
    }
}
