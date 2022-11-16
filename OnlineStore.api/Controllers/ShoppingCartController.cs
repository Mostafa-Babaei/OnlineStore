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
    [Route("api/[controller]")]
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
        public ApiResult AddToCart([FromBody] ShopingCartDto shopingCart)
        {
            return shoppingCartService.AddToCart(shopingCart);
        }

    }
}
