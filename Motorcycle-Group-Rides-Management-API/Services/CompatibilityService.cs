using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Motorcycle_Group_Rides_Management_API.Data;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Helpers;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using Motorcycle_Group_Rides_Management_API.Repository;
using Umbraco.Core.Persistence;
using static Motorcycle_Group_Rides_Management_API.Dtos.FeedbackDto;

namespace Motorcycle_Group_Rides_Management_API.Services
{


    public class CompatibilityService : ICompatibilityService
    {
        private readonly ICompatibilityRepository _compatibilityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CompatibilityService> _logger;

        public CompatibilityService(ICompatibilityRepository compatibilityRepository, IMapper mapper, ILogger<CompatibilityService> logger)
        {
            _compatibilityRepository = compatibilityRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // Check if a given motorcycle type is compatible with a given route type
        public async Task<CompatibilityLevel> CheckCompatibilityAsync(MotorcycleType motorcycleType, RouteType routeType)
        {
            var compatibility = await _compatibilityRepository
                .GetAllAsync();

            var result = compatibility.FirstOrDefault(c => c.MotorcycleType == motorcycleType && c.RouteType == routeType);

            return result?.CompatibilityLevel ?? CompatibilityLevel.NOT_COMPATIBLE;
        }

        // Get all motorcycles that are compatible with a specific route type
        public async Task<IEnumerable<MotorcycleType>> GetCompatibleMotorcyclesAsync(RouteType routeType)
        {
            var compatibilities = await _compatibilityRepository.GetAllAsync();

            return compatibilities
                .Where(c => c.RouteType == routeType && c.CompatibilityLevel != CompatibilityLevel.NOT_COMPATIBLE)
                .Select(c => c.MotorcycleType)
                .Distinct();
        }

        // Get all routes that are compatible with a specific motorcycle type
        public async Task<IEnumerable<RouteType>> GetCompatibleRoutesAsync(MotorcycleType motorcycleType)
        {
            var compatibilities = await _compatibilityRepository.GetAllAsync();

            return compatibilities
                .Where(c => c.MotorcycleType == motorcycleType && c.CompatibilityLevel != CompatibilityLevel.NOT_COMPATIBLE)
                .Select(c => c.RouteType)
                .Distinct();
        }

        // Add a new compatibility record to the system
        public async Task<CompatibilityDto> AddCompatibilityAsync(CreateCompatibilityDto compatibilityDto)
        {
            /* var compatibility = _mapper.Map<Compatibility>(compatibilityDto);
              await _compatibilityRepository.AddAsync(compatibility);
              await _compatibilityRepository.SaveChangesAsync();

              return _mapper.Map<CompatibilityDto>(compatibility);*/

            // Check if a compatibility with the same motorcycle type and route type already exists
            // Check if a compatibility with the same motorcycle type and route type already exists
            var existingCompatibility = await _compatibilityRepository.GetAllAsync();
            var duplicate = existingCompatibility
                .FirstOrDefault(c => c.MotorcycleType == compatibilityDto.MotorcycleType
                                  && c.RouteType == compatibilityDto.RouteType);

            if (duplicate != null)
            {
                // Stop the operation and throw an exception if a duplicate is found
                throw new InvalidOperationException("A compatibility with the same MotorcycleType and RouteType already exists.");
            }

            // Proceed with adding the new compatibility only if no duplicate is found
            var compatibility = _mapper.Map<Compatibility>(compatibilityDto);
            await _compatibilityRepository.AddAsync(compatibility);
            await _compatibilityRepository.SaveChangesAsync();

            return _mapper.Map<CompatibilityDto>(compatibility);



        }





        // Get a compatibility by its ID
        public async Task<CompatibilityDto> GetByIdAsync(int id)
        {
           
            var compatibility = await _compatibilityRepository.GetByIdAsync(id);
            return compatibility == null ? null : _mapper.Map<CompatibilityDto>(compatibility);
        }

 

        // Update an existing compatibility record
        public async Task<CompatibilityDto> UpdateCompatibilityAsync(int CompatibilityId, Dtos.UpdateCompatibilityDto updateCompatibilityDto)
        {
            var compatibility = await _compatibilityRepository.GetByIdAsync(CompatibilityId);



            if (compatibility == null)
            {
                throw new KeyNotFoundException("Compatibility not found");
            }

            _mapper.Map(updateCompatibilityDto, compatibility);
            await _compatibilityRepository.UpdateAsync(compatibility);
            await _compatibilityRepository.SaveChangesAsync();

            return _mapper.Map<CompatibilityDto>(compatibility);

            
        }

        // Delete a compatibility record by its ID
        public async Task DeleteAsync(int id)
        {
            var compatibility = await _compatibilityRepository.GetByIdAsync(id);
            if (compatibility != null)
            {
                await _compatibilityRepository.DeleteAsync(id);
                await _compatibilityRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CompatibilityDto>> GetCompatibilitiesAsync(string searchQuery, string sortBy, bool ascending, int pageNumber, int pageSize)
        {
            _logger.LogInformation("Fetching compatibilities with searchQuery: {SearchQuery}, sortBy: {SortBy}, ascending: {Ascending}, pageNumber: {PageNumber}, pageSize: {PageSize}",
                                    searchQuery, sortBy, ascending, pageNumber, pageSize);

            var compatibilities = await _compatibilityRepository.GetCompatibilitiesAsync(searchQuery, sortBy, ascending, pageNumber, pageSize);

            _logger.LogInformation("{CompatibilityCount} compatibilities fetched", compatibilities.Count());

            return _mapper.Map<IEnumerable<CompatibilityDto>>(compatibilities);
        }

    }
}