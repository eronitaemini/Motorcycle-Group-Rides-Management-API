using System;
using AutoMapper;
using Motorcycle_Group_Rides_Management_API.Models;
using static Motorcycle_Group_Rides_Management_API.Dtos.GroupDto;

namespace Motorcycle_Group_Rides_Management_API.Profiles
{
	public class GroupProfile:Profile
	{
		public GroupProfile()
		{
			CreateMap<Group, ViewGroupDto>();
			CreateMap<CreateUpdateGroupDto, Group>();
		}
	}
}

