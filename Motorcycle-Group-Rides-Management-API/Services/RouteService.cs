﻿using System;
using AutoMapper;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Services
{
    public class RouteService : IRouteService
	{
        private readonly IRouteRepository _repo;
        private readonly IMapper _mapper;
		public RouteService(IRouteRepository repo, IMapper mapper)
		{
            _repo = repo;
            _mapper = mapper;
		}

        public async Task CreateAsync(CreateRouteDto createRouteDto)
        {
            createRouteDto.RouteId = new Guid();
            var route = _mapper.Map<Routes>(createRouteDto);
            await _repo.CreateAsync(route);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var selectedRoute= await _repo.GetByIdAsync(id);
            
            if (selectedRoute == null)
            {

                throw new KeyNotFoundException("Route not found");
               
            }

            await _repo.DeleteAsync(id);
            await _repo.SaveChangesAsync();


        }

        public async Task<List<ViewRouteDto>> GetAllAsync()
        {
           var allRoutes = await _repo.GetAllAsync();
            return _mapper.Map<List<ViewRouteDto>>(allRoutes);
        }

        public async Task<ViewRouteDto> GetByIdAsync(Guid id)
        {
            var route = await _repo.GetByIdAsync(id);

            if (route != null)
            {
                return _mapper.Map<ViewRouteDto>(route);
            }
            return null;
        }

        public async Task UpdateAsync(Guid RouteId, UpdateRouteDto updateRouteDto)
        {
            var route = await _repo.GetByIdAsync(RouteId);

            if (RouteId != updateRouteDto.RouteId)
            {
                throw new ArgumentException("ID Mismatch");
            }

            if (route == null)
            {
                throw new KeyNotFoundException("Route not found");
            }

            _mapper.Map(updateRouteDto, route);
            await _repo.UpdateAsync(route);
            await _repo.SaveChangesAsync();
        }


       


    }
}

