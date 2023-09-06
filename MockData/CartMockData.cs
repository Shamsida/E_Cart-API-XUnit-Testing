using E_cart.DTO.CartDto;
using E_cart.DTO.UserDto;
using E_cart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECartTest.MockData
{
    public class CartMockData
    {
        public static List<CartDataDTO> SingleUser()
        {
            return new List<CartDataDTO>
            {
                new CartDataDTO
                {
                CartId = 1,
                TotalPrice = 450,
                TotalItems = 2,
                User = new UserDataDTO
                {
                    UserId = 1,
                    Username = "test",
                    Firstname = "test",
                    Lastname = "test",
                    Email = "test@gmail.com",
                    Role = "user",
                    Number = 123456
                }
                },
                new CartDataDTO
                {
                CartId = 2,
                TotalPrice = 500,
                TotalItems = 1,
                User = new UserDataDTO
                {
                    UserId = 1,
                    Username = "test",
                    Firstname = "test",
                    Lastname = "test",
                    Email = "test@gmail.com",
                    Role = "user",
                    Number = 123456
                }
                }
            };
        }
        public static AddtoCartDTO NewCart()
        {
            return new AddtoCartDTO
            {
                ProdID = 1,
                Qty = 1,
            };
        }
        public static Cart Cart()
        {
            return new Cart
            {
                Id = 1,
                StripePaymentIntentId = "gvfyhgtfvyhgbjukhy",
                TotalPrice = 500,
                TotalItems = 1,
                ClientSecret = "test"
            };
        }
    }
}
