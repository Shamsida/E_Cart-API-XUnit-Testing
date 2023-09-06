using E_cart.DTO.ProductDto;
using E_cart.DTO.UserDto;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECartTest.MockData
{
    public class UserMockData
    {
        public static List<UserDTO> GetUsers()
        {
            return new List<UserDTO>
            {
                new UserDTO {
                    UserId = 1,
                    Username = "Test1",
                    Firstname = "Test1",
                    Lastname = "Test1",
                    Email = "Test1@gmail.com",
                    Role = "user",
                    Imageurl = "Test1",
                    Number = 123456
                },
                new UserDTO {
                    UserId = 2,
                    Username = "Test2",
                    Firstname = "Test2",
                    Lastname = "Test2",
                    Email = "Test2@gmail.com",
                    Role = "user",
                    Imageurl = "Test2",
                    Number = 123456
                },
                new UserDTO {
                    UserId = 3,
                    Username = "Test3",
                    Firstname = "Test3",
                    Lastname = "Test3",
                    Email = "Test3@gmail.com",
                    Role = "user",
                    Imageurl = "Test3",
                    Number = 123456
                }
            };
        }
        public static List<UserDTO> GetEmptyUsers()
        {
            return null;
        }
        public static CreateUserDTO NewUser()
        {
            var mockImage = new Mock<IFormFile>();
            mockImage.Setup(x => x.FileName).Returns("3a3a0a02-05bd-4679-8f25-cbee87b88e8a.jpg");
            mockImage.Setup(x => x.Length).Returns(1024);

            return new CreateUserDTO
            {
                Username = "test1",
                Firstname="test1",
                Lastname="test1",
                Email = "test1@gmail.com",
                Password = "test1",
                Imageurl = mockImage.Object,
                Number = 123456
            };
        }
        public static UserDataDTO SingleUser()
        {
            return new UserDataDTO
            {
                UserId = 1,
                Username = "test",
                Firstname = "test",
                Lastname = "test",
                Email = "test@gmail.com",
                Role = "user",
                Number = 123456
            };
        }
        public static UserDataDTO NullUsers()
        {
            return null;
        }
    }
}
