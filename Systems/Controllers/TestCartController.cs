using E_cart.Controllers;
using E_cart.DTO.CartDto;
using E_cart.DTO.ProductDto;
using E_cart.DTO.UserDto;
using E_cart.Models;
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
    public class TestCartController
    {
        [Fact]
        public async Task GetCart_ShouldReturn200Status()
        {
            //Arrange
            int userId = 1;
            var mockService = new Mock<ICartService>();
            var mockCart = CartMockData.SingleUser();
            mockService
                .Setup(_ => _.GetCart(userId))
                .ReturnsAsync(mockCart);

            var _sut = new CartController(mockService.Object);

            //Act
            var result = (OkObjectResult)await _sut.GetCart(userId);

            //Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<IEnumerable<CartDataDTO>>(okResult.Value);
            Assert.Equal(mockCart, response);
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetCart_ShouldReturnBadRequest()
        {
            //Arrange
            int userId = 1;
            var mockService = new Mock<ICartService>();
            mockService
                .Setup(_ => _.GetCart(userId))
                .ReturnsAsync(()=> null);

            var _sut = new CartController(mockService.Object);

            //Act
            var result = await _sut.GetCart(userId);

            //Assert
            Assert.NotNull(result);
            var noContentResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, noContentResult.StatusCode);
        }

        [Fact]
        public async Task AddToCart_ShouldReturn200Status()
        {
            //Arrange
            int userId = 1;
            var mockService = new Mock<ICartService>();
            var mockNewCart = CartMockData.NewCart();
            var mockCart = CartMockData.Cart();
            mockService
                .Setup(_ => _.AddToCart(userId, mockNewCart))
                .ReturnsAsync(mockCart);

            var _sut = new CartController(mockService.Object);

            //Act
            var result = (OkObjectResult)await _sut.AddToCart(userId, mockNewCart);

            //Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var cart = Assert.IsType<Cart>(okResult.Value);
            if (result.Value is Cart response)
            {
                Assert.NotNull(response);
                Assert.Equal(mockCart.TotalItems, response.TotalItems);
            }
            result.StatusCode.Should().Be(200);
        }
    }
}
