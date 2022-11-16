using Application.Common.Model;
using Application.Model;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Service
{
    public class ShoppingCartService : IShoppingCartService
    {
        public ApiResult AddToCart(ShopingCartDto shopingCart)
        {
            throw new NotImplementedException();
        }

        public ApiResult DecrementCart(ShoppingCart shopingCart, int count)
        {
            throw new NotImplementedException();
        }

        public ApiResult DeleteCart(string userId)
        {
            throw new NotImplementedException();
        }

        public ShoppingCart GetItemOfCart(string userId, int productId)
        {
            throw new NotImplementedException();
        }

        public List<ShopingCartDto> GetItemsOfCustomer(string userId)
        {
            throw new NotImplementedException();
        }

        public ApiResult IncrementCart(ShoppingCart shopingCart, int count)
        {
            throw new NotImplementedException();
        }
    }
}
