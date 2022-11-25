using Application.Common.Model;
using Application.Model;
using Domain.Entities;
using Domain.Models;
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
        ApiResult IncrementCart(ShoppingCartDto shopingCart, int count);
        ApiResult DecrementCart(ShoppingCartDto shopingCart, int count);
        ApiResult DeleteCart(string userId);

        /// <summary>
        /// دریافت اطلاعات سبد خرید مشتری
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<ShoppingCartDto> GetItemsOfCustomer(string userId);

        /// <summary>
        /// دریافت آیتم سبد خرید
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        ShoppingCart GetItemOfCart(string userId, int productId);

        /// <summary>
        /// حذف آیتم از سبد خرید
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        ApiResult RemoveItemOfCart(string userId, int productId);

        /// <summary>
        /// بررسی موجود بودن سبد خرید مشتری
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool ExistCartForUser(string userId);

        /// <summary>
        /// تغییر تعداد محصول در سبد خرید
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ProductId"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        ApiResult ChangeNumberOfItem(string userId, int productId, int count);

    }
}
