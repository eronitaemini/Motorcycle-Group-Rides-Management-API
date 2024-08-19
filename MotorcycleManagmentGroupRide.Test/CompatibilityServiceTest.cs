using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Helpers;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using Motorcycle_Group_Rides_Management_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MotorcycleManagmentGroupRide.Test
{
    public class CompatibilityServiceTests
    {
        private readonly Mock<ICompatibilityRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<CompatibilityService>> _mockLogger;
        private readonly CompatibilityService _service;

        public CompatibilityServiceTests()
        {
            _mockRepo = new Mock<ICompatibilityRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<CompatibilityService>>();
            _service = new CompatibilityService(_mockRepo.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CheckCompatibilityAsync_ValidMotorcycleAndRoute_ReturnsCompatibilityLevel()
        {
            var motorcycleType = MotorcycleType.SPORT;
            var routeType = RouteType.HIGHWAY;
            var compatibilityList = new List<Compatibility>
        {
            new Compatibility { MotorcycleType = motorcycleType, RouteType = routeType, CompatibilityLevel = CompatibilityLevel. HIGHLY_COMPATIBLE }
        };

            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(compatibilityList);

            var result = await _service.CheckCompatibilityAsync(motorcycleType, routeType);

            Assert.Equal(CompatibilityLevel.HIGHLY_COMPATIBLE, result);
        }

        [Fact]
        public async Task CheckCompatibilityAsync_InvalidMotorcycleAndRoute_ReturnsNotCompatible()
        {
            var motorcycleType = MotorcycleType.CRUISER;
            var routeType = RouteType.MOUNTAIN;
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Compatibility>());

            var result = await _service.CheckCompatibilityAsync(motorcycleType, routeType);

            Assert.Equal(CompatibilityLevel.NOT_COMPATIBLE, result);
        }

        [Fact]
        public async Task GetCompatibleMotorcyclesAsync_ValidRoute_ReturnsMotorcycleTypes()
        {
            var routeType = RouteType.CITY;
            var compatibilityList = new List<Compatibility>
        {
            new Compatibility { MotorcycleType = MotorcycleType.TOURING, RouteType = routeType, CompatibilityLevel = CompatibilityLevel.HIGHLY_COMPATIBLE },
            new Compatibility { MotorcycleType = MotorcycleType.SPORT, RouteType = routeType, CompatibilityLevel = CompatibilityLevel.MODERATELY_COMPATIBLE }
        };

            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(compatibilityList);

            var result = await _service.GetCompatibleMotorcyclesAsync(routeType);

            Assert.Contains(MotorcycleType.TOURING, result);
            Assert.Contains(MotorcycleType.SPORT, result);
        }

        [Fact]
        public async Task GetCompatibleRoutesAsync_ValidMotorcycle_ReturnsRouteTypes()
        {
            var motorcycleType = MotorcycleType.DIRTBIKE;
            var compatibilityList = new List<Compatibility>
        {
            new Compatibility { MotorcycleType = motorcycleType, RouteType = RouteType.OFFROAD, CompatibilityLevel = CompatibilityLevel.HIGHLY_COMPATIBLE }
        };

            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(compatibilityList);

            var result = await _service.GetCompatibleRoutesAsync(motorcycleType);

            Assert.Contains(RouteType.OFFROAD, result);
        }

        [Fact]
        public async Task AddCompatibilityAsync_ValidDto_AddsCompatibility()
        {
            var createCompatibilityDto = new CreateCompatibilityDto
            {
                MotorcycleType = MotorcycleType.CRUISER,
                RouteType = RouteType.COASTAL,
                CompatibilityLevel = CompatibilityLevel.MODERATELY_COMPATIBLE
            };
            var compatibility = new Compatibility
            {
                CompatibilityId = 1,
                MotorcycleType = MotorcycleType.CRUISER,
                RouteType = RouteType.COASTAL,
                CompatibilityLevel = CompatibilityLevel.MODERATELY_COMPATIBLE
            };  
            _mockMapper.Setup(m => m.Map<Compatibility>(createCompatibilityDto)).Returns(compatibility);
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Compatibility>());

            await _service.AddCompatibilityAsync(createCompatibilityDto);

            _mockRepo.Verify(repo => repo.AddAsync(It.IsAny<Compatibility>()), Times.Once);
            _mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddCompatibilityAsync_DuplicateCompatibility_ThrowsInvalidOperationException()
        {
            var createCompatibilityDto = new CreateCompatibilityDto
            {
                MotorcycleType = MotorcycleType.CRUISER,
                RouteType = RouteType.COASTAL,
                CompatibilityLevel = CompatibilityLevel.MODERATELY_COMPATIBLE
            };
            var existingCompatibility = new Compatibility
            {
                MotorcycleType = MotorcycleType.CRUISER,
                RouteType = RouteType.COASTAL,
                CompatibilityLevel = CompatibilityLevel.MODERATELY_COMPATIBLE
            };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Compatibility> { existingCompatibility });

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddCompatibilityAsync(createCompatibilityDto));

            _mockRepo.Verify(repo => repo.AddAsync(It.IsAny<Compatibility>()), Times.Never);
            _mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Never);
        }

     

        [Fact]
        public async Task GetByIdAsync_InvalidId_ReturnsNull()
        {
            var id = 999;
            _mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Compatibility)null);

            var result = await _service.GetByIdAsync(id);

            Assert.Null(result);
        }

    
        [Fact]
        public async Task UpdateCompatibilityAsync_InvalidId_ThrowsKeyNotFoundException()
        {
            var compatibilityId = 999;
            var updateDto = new UpdateCompatibilityDto
            {
                MotorcycleType = MotorcycleType.CRUISER,
                RouteType = RouteType.CITY,
                CompatibilityLevel = CompatibilityLevel.NOT_COMPATIBLE
            };
            _mockRepo.Setup(repo => repo.GetByIdAsync(compatibilityId)).ReturnsAsync((Compatibility)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateCompatibilityAsync(compatibilityId, updateDto));

            _mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Compatibility>()), Times.Never);
            _mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_DeletesCompatibility()
        {
            var id = 1;
            var compatibility = new Compatibility
            {
                CompatibilityId = id,
                MotorcycleType = MotorcycleType.CRUISER,
                RouteType = RouteType.CITY,
                CompatibilityLevel = CompatibilityLevel.NOT_COMPATIBLE
            };
            _mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(compatibility);

            await _service.DeleteAsync(id);

            _mockRepo.Verify(repo => repo.DeleteAsync(id), Times.Once);
            _mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_DoesNotDeleteCompatibility()
        {
            var id = 999;
            _mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Compatibility)null);

            await _service.DeleteAsync(id);

            _mockRepo.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Never);
            _mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Never);
        }

     
    }

}
