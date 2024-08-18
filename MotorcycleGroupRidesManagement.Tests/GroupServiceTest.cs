using System;
using AutoMapper;
using Moq;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Models;
using Motorcycle_Group_Rides_Management_API.Services;
using static Motorcycle_Group_Rides_Management_API.Dtos.GroupDto;

namespace MotorcycleGroupRidesManagementAPI.Tests
{
    public class GroupServiceTests
    {
        private readonly Mock<IGroupRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GroupService _service;

        public GroupServiceTests()
        {
            _mockRepo = new Mock<IGroupRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new GroupService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetGroupsAsync_ReturnsGroups()
        {
            var groups = new List<Group> { new Group { GroupID = Guid.NewGuid(), Name = "Adventure Riders" } };
            _mockRepo.Setup(repo => repo.GetGroupsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()))
                     .ReturnsAsync(groups);

            var result = await _service.GetGroupsAsync("Adventure", "GroupName", true, 1, 10);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Adventure Riders", result.First().Name);
        }

        [Fact]
        public async Task CreateAsync_ValidGroup_CreatesGroup()
        {
            var createGroupDto = new CreateUpdateGroupDto { Name = "Adventure Riders" };
            _mockMapper.Setup(m => m.Map<Group>(createGroupDto)).Returns(new Group { Name = "Adventure Riders" });

            await _service.CreateAsync(createGroupDto);

            _mockRepo.Verify(repo => repo.CreateAsync(It.IsAny<Group>()), Times.Once);
            _mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

    }
}
