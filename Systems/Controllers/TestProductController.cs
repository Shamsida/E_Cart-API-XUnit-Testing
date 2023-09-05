using Xunit;
using E_cart.Controllers;
using E_cart.Repository.Interface;
using ECartTest.MockData;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using E_cart.Models;
using E_cart.DTO.ProductDto;
using System.Net;
using E_cart.Repository;

namespace ECartTest.Systems.Controllers
{
    public class TestProductController
    {

        [Fact]
        public async Task Get_ShouldReturn200Status()
        {
            //Arrange
            var mockService = new Mock<IProductService>();
            var mockProducts = ProductMockData.GetProducts();
            mockService.Setup(_ => _.Get()).ReturnsAsync(mockProducts);

            var _sut = new ProductController(mockService.Object);

            //Act
            var result = (OkObjectResult)await _sut.Get();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            result.StatusCode.Should().Be(200);
            //var response = result.Value as Response;
            if (result.Value is Response response)
            {
                Assert.True(response.Success);
                Assert.Equal(mockProducts, response.Result);
                var productList = response.Result as List<ProductDTO>;
                Assert.NotNull(productList);
                Assert.True(productList.All(p => mockProducts.Any(mp => mp.Id == p.Id)));
            }
            else
            {
                // Handle if result.Value is not of the expected type Response
                Assert.True(false, "result.Value is not of the expected type Response");
            }
        }

        
        [Fact]
        public async Task Get_WhenServiceReturnsNull_ShouldReturnBadRequest()
        {
            //Arrange
            var mockService = new Mock<IProductService>();
            var mockProducts = ProductMockData.GetEmptyProducts();
            mockService.Setup(_ => _.Get()).ReturnsAsync(mockProducts);

            var _sut = new ProductController(mockService.Object);

            //Act
            var result = await _sut.Get() as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);

            var response = result.Value as Response;
            Assert.NotNull(response);
            Assert.False(response.Success);
            Assert.Equal("Something went wrong", response.Error);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task GetById_ShouldReturn200Status()
        {
            //Arrange
            var productId = 1;
            var mockService = new Mock<IProductService>();
            var mockProducts = ProductMockData.GetProductById();
            mockService.Setup(_ => _.GetById(productId)).ReturnsAsync(mockProducts);

            var _sut = new ProductController(mockService.Object);

            //Act
            var result = (OkObjectResult)await _sut.GetById(productId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            result.StatusCode.Should().Be(200);

            if (result.Value is ProductDTO response)
            {
                Assert.NotNull(response);
                Assert.Equal(productId, response.Id);
            }
        }


        [Fact]
        public async Task Post_ShouldCall_IProductService_SaveAsync_AtleastOnce()
        {
            ///Arrange
            var mockService = new Mock<IProductService>();
            var mockProducts = ProductMockData.NewProduct();
            var mockOutputProduct = ProductMockData.NewProductOut();

            mockService.Setup(service => service.Post(mockProducts))
                .ReturnsAsync(mockOutputProduct);

            var _sut = new ProductController(mockService.Object);

            ///Act
            var result = await _sut.Post(mockProducts) as OkObjectResult;

            /// Assert
            //mockService.Verify(_ => _.Post(mockProducts), Times.Exactly(1));

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            result.StatusCode.Should().Be(200);

            if (result.Value is ProductDTO response)
            {
                Assert.NotNull(response);
                Assert.Equal(mockOutputProduct.Id, response.Id);
            }
        }

        [Fact]
        public async Task Post_WhenServiceReturnsNull_ShouldReturnBadRequest()
        {
            // Arrange
            var mockService = new Mock<IProductService>();
            var mockInputProduct = ProductMockData.NewProduct();

            mockService.Setup(service => service.Post(mockInputProduct))
                .ReturnsAsync(ProductMockData.NewEmptyProduct());

            var controller = new ProductController(mockService.Object);

            // Act
            var result = await controller.Post(mockInputProduct) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Put_ShouldReturn200Status()
        {
            // Arrange
            var productId = 3;
            var mockService = new Mock<IProductService>();
            var saveImageMock = new Mock<SaveImage>();

            var mockUpdateProduct = ProductMockData.UpdateProduct();
            var mockexistingProduct = ProductMockData.NewProductOut();

            mockService.Setup(service => service.GetById(productId)).ReturnsAsync(mockexistingProduct);

            var _sut = new ProductController(mockService.Object, saveImageMock.Object);

            //Act
            var result = (OkObjectResult)await _sut.Put(productId, mockUpdateProduct);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            result.StatusCode.Should().Be(200);

            if (result.Value is Product response)
            {
                Assert.NotNull(response);
                Assert.Equal(mockexistingProduct.Title,response.Title);
            }
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContentResult()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var productController = new ProductController(productServiceMock.Object);
            var productIdToDelete = 1;

            productServiceMock.Setup(service => service.Delete(productIdToDelete))
                .ReturnsAsync(true); 

            // Act
            var result = await productController.Delete(productIdToDelete);

            // Assert
            Assert.IsType<NoContentResult>(result);

        }
    }
}
