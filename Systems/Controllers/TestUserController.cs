using E_cart.Controllers;
using E_cart.DTO.ProductDto;
using E_cart.DTO.UserDto;
using E_cart.Repository.Interface;
using ECartTest.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECartTest.Systems.Controllers
{
    public class TestUserController
    {
        [Fact]
        public async Task Get_ShouldReturn200Status()
        {
            //Arrange
            var mockService = new Mock<IUserService>();
            var mockUsers = UserMockData.GetUsers();
            mockService.Setup(_ => _.Get()).ReturnsAsync(mockUsers);

            var _sut = new UserController(mockService.Object);

            //Act
            var result = (OkObjectResult)await _sut.Get();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var okResult = (OkObjectResult)result;
            var responseData = okResult.Value as IEnumerable<UserDTO>;
            Assert.NotNull(responseData);
            Assert.Equal(mockUsers, responseData);
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_WhenServiceReturnsNull_ShouldReturnBadRequest()
        {
            //Arrange
            var mockService = new Mock<IUserService>();
            var mockUsers = UserMockData.GetEmptyUsers();
            mockService.Setup(_ => _.Get()).ReturnsAsync(mockUsers);

            var _sut = new UserController(mockService.Object);

            //Act
            var result = await _sut.Get();

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task SignUp_ShouldReturn200Status()
        {
            //Arrange
            var mockService = new Mock<IUserService>();
            var mockUser = UserMockData.NewUser();
            var mockOutputUser = UserMockData.SingleUser();
            mockService.Setup(_ => _.SignUP(It.IsAny<CreateUserDTO>())).ReturnsAsync(mockOutputUser);

            var _sut = new UserController(mockService.Object);

            //Act
            var result = (OkObjectResult)await _sut.SignUp(mockUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var user = Assert.IsType<UserDataDTO>(okResult.Value);
            if (result.Value is UserDataDTO response)
            {
                Assert.NotNull(response);
                Assert.Equal(mockOutputUser.Role, response.Role);
                Assert.Equal(mockOutputUser.Username, response.Username);
            }
        }

        [Fact]
        public async Task SignUp_InvalidUser_ReturnsBadRequest()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockUsers = UserMockData.NullUsers();
            var controller = new UserController(mockUserService.Object);

            var invalidCreateUserDTO = new CreateUserDTO
            {
                Username = "",
                Firstname = "test1",
                Lastname = "test1",
                Email = "test",
                Password = "test1",
                Imageurl = null,
                Number = 123456
            };

            
            mockUserService
                .Setup(service => service.SignUP(invalidCreateUserDTO))
                .ReturnsAsync(mockUsers);

            // Act
            var result = await controller.SignUp(invalidCreateUserDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnOkResult()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var productController = new UserController(userServiceMock.Object);
            int IdToDelete = 1;

            userServiceMock.Setup(service => service.Delete(IdToDelete))
                .ReturnsAsync(true);

            // Act
            var result = await productController.Delete(IdToDelete);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var message = Assert.IsType<string>(okResult.Value);
            Assert.Equal("Removed Successfully", message);

        }
    }
}
