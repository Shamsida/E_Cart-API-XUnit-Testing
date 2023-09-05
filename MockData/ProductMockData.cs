using E_cart.Models;
using ECartProduct = E_cart.Models.Product;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_cart.DTO.ProductDto;
using Microsoft.AspNetCore.Http;
using Moq;

namespace ECartTest.MockData
{
    public class ProductMockData
    {
        public static List<ProductDTO> GetProducts()
        {
            return new List<ProductDTO>
            {
                new ProductDTO {
                    Id = 1,
                    CategoryId = 1,
                    Title = "Twin Cute Bunny Set Combo",
                    Description = "Pellentesque nisl ac dictum tincidunt ut viverra non, sem in sed phasellus tempor.",
                    Price = 440,
                    Image = "e42a6a74-024c-4efd-b67c-4bc0c5893340.jpg"
                },
                new ProductDTO {
                    Id = 2,
                    CategoryId = 2,
                    Title = "Twin Cute Bunny Set Combo",
                    Description = "Pellentesque nisl ac dictum tincidunt ut viverra non, sem in sed phasellus tempor.",
                    Price = 440,
                    Image = "e42a6a74-024c-4efd-b67c-4bc0c5893340.jpg"
                },
                new ProductDTO {
                    Id = 3,
                    CategoryId = 3,
                    Title = "Twin Cute Bunny Set Combo",
                    Description = "Pellentesque nisl ac dictum tincidunt ut viverra non, sem in sed phasellus tempor.",
                    Price = 440,
                    Image = "e42a6a74-024c-4efd-b67c-4bc0c5893340.jpg"
                }
            };
        }

        public static List<ProductDTO> GetEmptyProducts()
        {
            return null;
        }

        public static ECartProduct GetProductById()
        {
            return new ECartProduct
            {
                Id = 1,
                CategoryId = 1,
                Title = "Twin Cute Bunny Set Combo",
                Description = "Pellentesque nisl ac dictum tincidunt ut viverra non, sem in sed phasellus tempor.",
                Price = 440,
                Image = "e42a6a74-024c-4efd-b67c-4bc0c5893340.jpg"
            };
        }

        public static CreateProductDTO NewProduct()
        {
            var mockImage = new Mock<IFormFile>();
            mockImage.Setup(x => x.FileName).Returns("3a3a0a02-05bd-4679-8f25-cbee87b88e8a.jpg");
            mockImage.Setup(x => x.Length).Returns(1024);

            return new CreateProductDTO
            {
                CategoryName = "dress",
                Title = "White Comfort Maxx",
                Description = "Pellentesque nisl ac dictum tincidunt ut viverra non, sem in sed phasellus tempor.",
                Price = 550,
                Image = mockImage.Object
            };
        }

        public static ECartProduct NewProductOut()
        {
            return new ECartProduct
            {
                Id = 3,
                CategoryId = 1,
                Title = "White Comfort Maxx",
                Description = "Pellentesque nisl ac dictum tincidunt ut viverra non, sem in sed phasellus tempor.",
                Price = 550,
                Image = "e42a6a74-024c-4efd-b67c-4bc0c5893340.jpg"
            };
        }

        public static ECartProduct NewEmptyProduct()
        {
            return null;
        }
        public static UpdateProductDTO UpdateProduct()
        {
            var mockImage = new Mock<IFormFile>();
            mockImage.Setup(x => x.FileName).Returns("3a3a0a02-05bd-4679-8f25-cbee87b88e8a.jpg");
            mockImage.Setup(x => x.Length).Returns(1024);

            return new UpdateProductDTO { 
                CategoryName = "dress",
                Title = "White Comfort Maxx",
                Description = "Pellentesque nisl ac dictum tincidunt ut viverra non, sem in sed phasellus tempor.",
                Price = 550,
                Image = mockImage.Object
            };
        }
    }
}
