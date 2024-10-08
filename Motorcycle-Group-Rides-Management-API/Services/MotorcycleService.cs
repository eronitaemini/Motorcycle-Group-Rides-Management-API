﻿using System;
using AutoMapper;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using Motorcycle_Group_Rides_Management_API.Repository;
using static Motorcycle_Group_Rides_Management_API.Dtos.MotorcycleDtos;

namespace Motorcycle_Group_Rides_Management_API.Services
{
    public class MotorcycleService : IMotorcycleService
	{
        private readonly IMotorcycleRepository _repo;
        private IMapper _mapper;
        private readonly ILogger<MotorcycleService> _logger;

        public MotorcycleService(IMotorcycleRepository repo, IMapper mapper, ILogger<MotorcycleService> logger)
		{
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateAsync(MotorcycleDtos.CreateMotorcycleDto createMotorcycleDto)
        {
           
            var motorcycle = _mapper.Map<Motorcycle>(createMotorcycleDto);
            await _repo.CreateAsync(motorcycle);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var selectedMotorcycle = await _repo.GetByIdAsync(id);

            if (selectedMotorcycle == null)
            {

                throw new KeyNotFoundException("Motorcycle not found");

            }

            await _repo.DeleteAsync(id);
            await _repo.SaveChangesAsync();
        }

        public async Task<List<MotorcycleDtos.ViewMotorcycleDto>> GetAllAsync()
        {
            var allMotorcycles = await _repo.GetAllAsync();
            return _mapper.Map<List<ViewMotorcycleDto>>(allMotorcycles);
        }

        public async Task<MotorcycleDtos.ViewMotorcycleDto> GetByIdAsync(int id)
        {
            var motorcycle = await _repo.GetByIdAsync(id);

            if (motorcycle != null)
            {
                return _mapper.Map<ViewMotorcycleDto>(motorcycle);
            }
            return null;
        }

        public async Task<IEnumerable<Motorcycle>> GetMotorcyclesAsync(string searchQuery, string sortBy, bool ascending, int pageNumber, int pageSize)
        {
            _logger.LogInformation("Fetching motorcycles with searchQuery: {SearchQuery}, sortBy: {SortBy}, ascending: {Ascending}, pageNumber: {PageNumber}, pageSize: {PageSize}",
                                searchQuery, sortBy, ascending, pageNumber, pageSize);

            var motorcycles = await _repo.GetMotorcyclesAsync(searchQuery, sortBy, ascending, pageNumber, pageSize);

            _logger.LogInformation("{MotorcycleCount} motorcycles fetched", motorcycles.Count());

            return motorcycles;
        }

        public async Task UpdateAsync(int id, MotorcycleDtos.UpdateMotorcycleDto updateMotorcycleDto)
        {
            var motorcycle = await _repo.GetByIdAsync(id);

            if (id != updateMotorcycleDto.MotorcycleID)
            {
                throw new ArgumentException("ID Mismatch");
            }

            if (motorcycle == null)
            {
                throw new KeyNotFoundException("Route not found");
            }

            _mapper.Map(updateMotorcycleDto, motorcycle);
            await _repo.UpdateAsync(motorcycle);
            await _repo.SaveChangesAsync();
        }

        public async Task<List<ViewMotorcycleDto>> GetMotorcyclesByUserIdAsync(Guid userId)
        {
            var motorcycles = await _repo.GetMotorcyclesByUserIdAsync(userId);

            if (motorcycles == null || motorcycles.Count == 0)
            {
                throw new KeyNotFoundException("No motorcycles found for this user.");
            }

            return _mapper.Map<List<MotorcycleDtos.ViewMotorcycleDto>>(motorcycles);
        }
    }
}

