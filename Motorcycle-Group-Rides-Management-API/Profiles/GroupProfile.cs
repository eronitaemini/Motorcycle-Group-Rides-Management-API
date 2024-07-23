using System;
using System.Text.RegularExpressions;
using AutoMapper;
using static Motorcycle_Group_Rides_Management_API.GroupDtos;

namespace Motorcycle_Group_Rides_Management_API.Profiles
{
	public class GroupProfile:Profile
	{
		public GroupProfile()
		{
			CreateMap<Group, ViewGroupDto>();
			CreateMap<ViewGroupDto, Group>();
		}
	}
}

