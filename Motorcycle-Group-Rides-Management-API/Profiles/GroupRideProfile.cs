using AutoMapper;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Profiles
{
    public class GroupRideProfile: Profile
    {
        public GroupRideProfile()
        {
            CreateMap<CreateGroupRideDto, GroupRide>();
            CreateMap<GroupRide, GroupRideDto>()
                .ForMember(dest => dest.UserIds, opt => opt.MapFrom(src => src.UserGroupRides.Select(ug => ug.UserId).ToList()));
        }
    }
}
