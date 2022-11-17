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
    public interface IShoppingCartService
    {
        ApiResult AddToCart(ShoppingCartDto shopingCart);
        ApiResult IncrementCart(ShoppingCartDto shopingCart,int count);
        ApiResult DecrementCart(ShoppingCartDto shopingCart, int count);
        ApiResult DeleteCart(string userId);
        List<ShoppingCartDto> GetItemsOfCustomer(string userId);
        ShoppingCart GetItemOfCart(string userId,int productId);
    }
}
