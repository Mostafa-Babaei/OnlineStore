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
        ApiResult AddToCart(ShopingCartDto shopingCart);
        ApiResult IncrementCart(ShoppingCart shopingCart,int count);
        ApiResult DecrementCart(ShoppingCart shopingCart, int count);
        ApiResult DeleteCart(string userId);
        List<ShopingCartDto> GetItemsOfCustomer(string userId);
        ShoppingCart GetItemOfCart(string userId,int productId);
    }
}
