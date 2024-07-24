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
			CreateMap<Motorcycle, ViewMotorcycleDto>();
			CreateMap<CreateMotorcycleDto, Motorcycle>();
			CreateMap<UpdateMotorcycleDto, Motorcycle>();
		}
	}
}

