
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Motorcycle_Group_Rides_Management_API.Authentications;
using Motorcycle_Group_Rides_Management_API.Controllers;
using Motorcycle_Group_Rides_Management_API.Dtos;

namespace Motorcycle_Group_Rides_Management_API.Tests
{
    public class UserTestings
    {

        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly UsersController _controller;

        public UserTestings()
        {
            var store = new Mock<IUserStore<IdentityUser>>();
            _userManagerMock = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
            _controller = new UsersController(_userManagerMock.Object);
        }

     

      

        [Fact]
        public async Task GetUserById_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "1";
            _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync((IdentityUser)null);

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnOk_WhenUserIsDeleted()
        {
            // Arrange
            var userId = "1";
            var user = new IdentityUser { Id = userId, UserName = "user1", Email = "user1@example.com" };
            _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.DeleteUser(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User deleted successfully", okResult.Value);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "1";
            _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync((IdentityUser)null);

            // Act
            var result = await _controller.DeleteUser(userId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

      

        [Fact]
        public async Task GetUserByUsername_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var username = "user1";
            _userManagerMock.Setup(um => um.FindByNameAsync(username)).ReturnsAsync((IdentityUser)null);

            // Act
            var result = await _controller.GetUserByUsername(username);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnOk_WhenUserIsUpdated()
        {
            // Arrange
            var userId = "1";
            var user = new IdentityUser { Id = userId, UserName = "olduser", Email = "olduser@example.com" };
            var updateDto = new UpdateUserDto { Username = "newuser", Email = "newuser@example.com" };
            _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.UpdateAsync(It.IsAny<IdentityUser>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.UpdateUser(userId, updateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User updated successfully", okResult.Value);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "1";
            var updateDto = new UpdateUserDto { Username = "newuser", Email = "newuser@example.com" };
            _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync((IdentityUser)null);

            // Act
            var result = await _controller.UpdateUser(userId, updateDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

       


    }
}
