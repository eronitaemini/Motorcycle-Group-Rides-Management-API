using AutoMapper;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.IncidentReportProfile
{
    public class IncidentReportProfile : Profile
    {
        public IncidentReportProfile()
        {
            CreateMap<CreateIncidentReportDto, IncidentReport>();
            CreateMap<IncidentReport, CreateIncidentReportDto>();
            CreateMap<IncidentReport, IncidentReportDto>(); 
            CreateMap<UpdateIncidentStatusDto, IncidentReport>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
