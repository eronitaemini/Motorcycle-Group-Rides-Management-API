using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Motorcycle_Group_Rides_Management_API.Controllers;
using Motorcycle_Group_Rides_Management_API.Dtos;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Motorcycle_Group_Rides_Management_API.Tests
{
    public class GroupRideTestings
    {
        private readonly Mock<IGroupRideRepository> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly GroupRideController _controller;

        public GroupRideTestings()
        {
            _repositoryMock = new Mock<IGroupRideRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateGroupRideDto, GroupRide>();
                cfg.CreateMap<GroupRide, GroupRideDto>();
                cfg.CreateMap<CreateGroupRideDto, GroupRide>();
            });
            _mapper = mapperConfig.CreateMapper();
            _controller = new GroupRideController(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task CreateGroupRide_ShouldReturnOk_WhenGroupRideIsCreated()
        {
            // Arrange
            var createDto = new CreateGroupRideDto
            {
                Name = "Group Ride 1",
                Description = "Description",
                StartingPoint = "Start",
                EndingPoint = "End"
            };
            var groupRide = _mapper.Map<GroupRide>(createDto);
            _repositoryMock.Setup(repo => repo.CreateAsync(groupRide)).Returns(Task.CompletedTask);
            _repositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);

            // Act
            var result = await _controller.CreateGroupRide(createDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Group ride created successfully.", okResult.Value);
        }

       

        [Fact]
        public async Task GetGroupRideById_ShouldReturnNotFound_WhenGroupRideDoesNotExist()
        {
            // Arrange
            var groupRideId = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.GetByIdAsync(groupRideId)).ReturnsAsync((GroupRide)null);

            // Act
            var result = await _controller.GetGroupRideById(groupRideId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateGroupRide_ShouldReturnOk_WhenGroupRideIsUpdated()
        {
            // Arrange
            var groupRideId = Guid.NewGuid();
            var existingGroupRide = new GroupRide
            {
                Id = groupRideId,
                Name = "Old Ride",
                Description = "Old Description",
                StartingPoint = "Old Start",
                EndingPoint = "Old End"
            };
            var updateDto = new CreateGroupRideDto
            {
                Name = "Updated Ride",
                Description = "Updated Description",
                StartingPoint = "Updated Start",
                EndingPoint = "Updated End"
            };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(groupRideId)).ReturnsAsync(existingGroupRide);
            _repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<GroupRide>())).Returns(Task.CompletedTask);
            _repositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateGroupRide(groupRideId, updateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Group ride updated successfully.", okResult.Value);
        }

        [Fact]
        public async Task UpdateGroupRide_ShouldReturnNotFound_WhenGroupRideDoesNotExist()
        {
            // Arrange
            var groupRideId = Guid.NewGuid();
            var updateDto = new CreateGroupRideDto
            {
                Name = "Updated Ride",
                Description = "Updated Description",
                StartingPoint = "Updated Start",
                EndingPoint = "Updated End"
            };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(groupRideId)).ReturnsAsync((GroupRide)null);

            // Act
            var result = await _controller.UpdateGroupRide(groupRideId, updateDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteGroupRide_ShouldReturnOk_WhenGroupRideIsDeleted()
        {
            // Arrange
            var groupRideId = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.DeleteAsync(groupRideId)).Returns(Task.CompletedTask);
            _repositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteGroupRide(groupRideId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Group ride deleted successfully.", okResult.Value);
        }

       

        

       
    }
}
