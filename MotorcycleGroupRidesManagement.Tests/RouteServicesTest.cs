using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using Motorcycle_Group_Rides_Management_API.Services;
using Motorcycle_Group_Rides_Management_API.Dtos;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Motorcycle_Group_Rides_Management_API.External;
using static Motorcycle_Group_Rides_Management_API.Dtos.RoutesDto;

namespace MotorcycleGroupRidesManagementAPI.Tests
{
    public class RouteServiceTests
    {
        private readonly Mock<IRouteRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IRouteInfo> _mockRouteInfo;
        private readonly Mock<ILogger<RouteService>> _mockLogger;
        private readonly RouteService _service;

        public RouteServiceTests()
        {
            _mockRepo = new Mock<IRouteRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockRouteInfo = new Mock<IRouteInfo>();
            _mockLogger = new Mock<ILogger<RouteService>>();
            _service = new RouteService(_mockRepo.Object, _mockMapper.Object, _mockRouteInfo.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateAsync_ValidRoute_CreatesRoute()
        {
            var createRouteDto = new CreateRouteDto { StartingPoint = "New York", EndingPoint = "Los Angeles" };
            _mockMapper.Setup(m => m.Map<Routes>(createRouteDto)).Returns(new Routes { StartingPoint = "New York", EndingPoint = "Los Angeles" });
            _mockRouteInfo.Setup(ri => ri.GetRouteInfoAsync("New York", "Los Angeles")).ReturnsAsync(new RouteInfo { DistanceText = "2,800 miles", DurationText = "42 hours" });

            await _service.CreateAsync(createRouteDto);

            _mockRepo.Verify(repo => repo.CreateAsync(It.IsAny<Routes>()), Times.Once);
            _mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        

        [Fact]
        public async Task GetByIdAsync_NonExistentRoute_ReturnsNull()
        {
            var routeId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.GetByIdAsync(routeId)).ReturnsAsync((Routes)null);

            var result = await _service.GetByIdAsync(routeId);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ExistingRoute_DeletesRoute()
        {
            var routeId = Guid.NewGuid();
            var route = new Routes { RouteId = routeId };
            _mockRepo.Setup(repo => repo.GetByIdAsync(routeId)).ReturnsAsync(route);

            await _service.DeleteAsync(routeId);

            _mockRepo.Verify(repo => repo.DeleteAsync(routeId), Times.Once);
            _mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_NonExistentRoute_ThrowsKeyNotFoundException()
        {
            var routeId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.GetByIdAsync(routeId)).ReturnsAsync((Routes)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteAsync(routeId));
        }
    }
}
