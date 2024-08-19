using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using Motorcycle_Group_Rides_Management_API.Services;
using static Motorcycle_Group_Rides_Management_API.Dtos.MotorcycleDtos;

namespace MotorcycleGroupRidesManagementAPI.Tests
{
    public class MotorcycleServiceTests
    {
        private readonly Mock<IMotorcycleRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<MotorcycleService>> _mockLogger;
        private readonly MotorcycleService _service;

        public MotorcycleServiceTests()
        {
            _mockRepo = new Mock<IMotorcycleRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<MotorcycleService>>();
            _service = new MotorcycleService(_mockRepo.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetMotorcyclesAsync_ReturnsMotorcycles()
        {
            var motorcycles = new List<Motorcycle> { new Motorcycle { MotorcycleID = 1, Brand = "Honda", Model = "CBR500R" } };
            _mockRepo.Setup(repo => repo.GetMotorcyclesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()))
                     .ReturnsAsync(motorcycles);

            var result = await _service.GetMotorcyclesAsync("Honda", "Brand", true, 1, 10);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Honda", result.First().Brand);
        }

        [Fact]
        public async Task CreateAsync_ValidMotorcycle_CreatesMotorcycle()
        {
            var createMotorcycleDto = new CreateMotorcycleDto { Brand = "Honda", Model = "CBR500R" };
            _mockMapper.Setup(m => m.Map<Motorcycle>(createMotorcycleDto)).Returns(new Motorcycle { Brand = "Honda", Model = "CBR500R" });

            
            await _service.CreateAsync(createMotorcycleDto);

            _mockRepo.Verify(repo => repo.CreateAsync(It.IsAny<Motorcycle>()), Times.Once);
            _mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

    }
}
