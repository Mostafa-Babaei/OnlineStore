using Application.Common.Model;
using Application.Model;
using infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.api.Controllers
{
    /// <summary>
    /// سبد خرید
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        /// <summary>
        /// افزودن به سبد خرید
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult AddToCart([FromBody] ShoppingCartDto shopingCart)
        {
            return shoppingCartService.AddToCart(shopingCart);
        }

        /// <summary>
        /// دریافت سبد خرید مشتری
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetCartOfUser(string userId)
        {
            return ApiResult.ToSuccessModel("سبد خرید مشتری", shoppingCartService.GetItemsOfCustomer(userId));
        }

        [HttpDelete]
        public ApiResult RemoveItemFromCart(string userId)
        {
            return ApiResult.ToSuccessModel("سبد خرید مشتری", shoppingCartService.GetItemsOfCustomer(userId));
        }

    }
}
