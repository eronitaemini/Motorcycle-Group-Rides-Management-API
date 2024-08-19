using AutoMapper;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Profiles
{
    public class StatisticsProfile : Profile
    {
         public StatisticsProfile() {

            CreateMap<KeyValuePair<string, int>, IncidentLocationCountDto>();


            CreateMap<Route, PopularityRouteDto>();
            // Mapping for other necessary DTOs
            CreateMap<IncidentReport, IncidentReportDto>();
            CreateMap<IncidentReportDto, IncidentReport>();

            CreateMap<CreateIncidentReportDto, IncidentReport>();
            CreateMap<UpdateIncidentStatusDto, IncidentReport>();


            // Map from IncidentReport to IncidentLocationCountDto
            CreateMap<IncidentReport, IncidentLocationCountDto>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.IncidentCount, opt => opt.Ignore()); // Assuming IncidentCount is manually populated

            // Map from PopularityRoute to PopularityRouteDto
            CreateMap<GroupRide, PopularityRouteDto>()
                .ForMember(dest => dest.RouteId, opt => opt.MapFrom(src => src.RouteID))
                .ForMember(dest => dest.Popularity, opt => opt.Ignore()); // Populated in the repository

           

          


        }
    }
}
