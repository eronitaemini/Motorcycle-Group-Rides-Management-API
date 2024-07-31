using System;
using AutoMapper;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Models;
using static Motorcycle_Group_Rides_Management_API.Dtos.RoutesDto;

namespace Motorcycle_Group_Rides_Management_API.Profiles
{
	public class RouteProfile:Profile
	{
		public RouteProfile()
		{
			CreateMap<Routes, ViewRouteDto>();
			CreateMap<CreateRouteDto, Routes>();
			CreateMap<UpdateRouteDto,Routes>();

		}
	}
}

