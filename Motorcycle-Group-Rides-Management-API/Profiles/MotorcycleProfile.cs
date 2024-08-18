using System;
using AutoMapper;
using Motorcycle_Group_Rides_Management_API.Models;
using static Motorcycle_Group_Rides_Management_API.Dtos.MotorcycleDtos;

namespace Motorcycle_Group_Rides_Management_API.Profiles
{
	public class MotorcycleProfile:Profile
	{
		public MotorcycleProfile()
		{
			CreateMap<Motorcycle, ViewMotorcycleDto>().ForMember(dest=>dest.Type, opt=>opt.MapFrom(src=>src.Type.ToString()));
			CreateMap<CreateMotorcycleDto, Motorcycle>();
			CreateMap<UpdateMotorcycleDto, Motorcycle>();
		}
	}
}

