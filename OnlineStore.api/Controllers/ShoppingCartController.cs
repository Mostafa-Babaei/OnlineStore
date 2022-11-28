using Application.Common.Model;
using Application.Model;
using Domain.Models;
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
        private readonly IProductService productService;
        private readonly IShopOrderService shopOrderService;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IProductService productService, IShopOrderService shopOrderService)
        {
            this.shoppingCartService = shoppingCartService;
            this.productService = productService;
            this.shopOrderService = shopOrderService;
        }

        /// <summary>
        /// افزودن به سبد خرید
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult AddToCart([FromBody] AddToCartDto toCartDto)
        {
            string userId = GetUser();
            if (GetUser() == null)
                return ApiResult.ToErrorModel("کاربر یافت نشد");

            Product product = productService.GetProduct(toCartDto.ProductId);
            if (product == null)
                return ApiResult.ToErrorModel("محصول یافت نشد");

            ShoppingCartDto shoppingCartDto = new ShoppingCartDto()
            {
                Count = toCartDto.Count,
                UserId = userId,
                ProductId = toCartDto.ProductId,
                Product = product
            };
            return shoppingCartService.AddToCart(shoppingCartDto);
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

        /// <summary>
        /// حذف کالا از سبد خرید
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult RemoveItemFromCart(int productId)
        {
            string userId = GetUser();
            if (GetUser() == null)
                return ApiResult.ToErrorModel("کاربر یافت نشد");

            return shoppingCartService.RemoveItemOfCart(userId, productId);
        }

        /// <summary>
        /// دریافت اطلاعات سبد خرید کاربر
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetCartItems()
        {
            string userId = GetUser();
            if (GetUser() == null)
                return ApiResult.ToErrorModel("کاربر یافت نشد");

            if (!shoppingCartService.ExistCartForUser(userId))
                return ApiResult.ToErrorModel("سبد خرید خالی است");

            var cartItems = shoppingCartService.GetItemsOfCustomer(userId);
            List<CartItemDto> cartItem = new List<CartItemDto>();

            if (cartItems.Any() && cartItems.Count > 0)
                foreach (var item in cartItems)
                {
                    var product = productService.GetProduct(item.ProductId);
                    if (product != null)
                    {
                        cartItem.Add(new CartItemDto()
                        {
                            Id = item.Id,
                            Count = item.Count,
                            ProductId = item.ProductId,
                            UserId = item.UserId,
                            Price = product.Price,
                            Title = product.Title,
                            TotalPrice = item.Count * product.Price

                        });
                    }
                }

            return ApiResult.ToSuccessModel("اطلاعات سبد خرید", cartItem);
        }

        /// <summary>
        /// تغییر تعداد آیتم در سبد خرید
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newCount"></param>
        /// <returns></returns>
        [HttpPut]
        public ApiResult ChangeNumberOfCartItem(int productId, int newCount)
        {
            string userId = GetUser();
            if (GetUser() == null)
                return ApiResult.ToErrorModel("کاربر یافت نشد");

            if (!shoppingCartService.ExistCartForUser(userId))
                return ApiResult.ToErrorModel("سبد خرید خالی است");

            return shoppingCartService.ChangeNumberOfItem(userId, productId, newCount);
        }

        /// <summary>
        /// تبدیل سبد خرید به سفارش
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult AddOrderFromCart()
        {
            try
            {
                //بررسی کاربر
                string userId = GetUser();
                if (GetUser() == null)
                    return ApiResult.ToErrorModel("کاربر یافت نشد");

                //بررسی سبد خرید
                if (!shoppingCartService.ExistCartForUser(userId))
                    return ApiResult.ToErrorModel("محصولی در سبد خرید شما یافت نشد");

                //دریافت اطلاعات سبد خرید
                List<ShoppingCartDto> carts = shoppingCartService.GetItemsOfCustomer(userId);

                List<InsertOrderItemDto> orderItems = new List<InsertOrderItemDto>();
                foreach (ShoppingCartDto cart in carts)
                {
                    Product product = productService.GetProduct(cart.ProductId);
                    orderItems.Add(new InsertOrderItemDto()
                    {
                        BasePrice = product.Price,
                        Product = product,
                        ProductId = product.ProductId,
                        Quantity = cart.Count,
                        TotalPrice = cart.Count * product.Price
                    });
                }

                //ثبت سفارش
                ApiResult addResult = shopOrderService.InsertOrder(new InsertOrderDto()
                {
                    ItemsOfOrder = orderItems,
                    IsActive = true,
                    Price = orderItems.Sum(e => e.TotalPrice),
                    IsPayment = false,
                    State = Domain.Enums.OrderState.DefineOrder,
                    OrderNumber = shopOrderService.GenerateInvoiceNumber(),
                    UserId = userId
                });

                //حذف سبد خرید
                if (addResult.IsSuccess)
                    shoppingCartService.DeleteCart(userId);

                return addResult;
                //Todo:ارسال ایمیل و پیامک برای مشتری

            }
            catch (Exception ex)
            {
                return ApiResult.ToErrorModel("خطای پیش بینی نشده در ثبت سفارش");
            }
        }

        /// <summary>
        /// دریافت تعداد محصول در سبد خرید
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public int GetNumberOfItem()
        {
            //بررسی کاربر
            string userId = GetUser();
            if (GetUser() == null)
                return 0;

            return shoppingCartService.GetNumberOfCartItem(userId);
        }

        private string GetUser()
        {
            return this.User.Claims.FirstOrDefault(e => e.Type == "userId")?.Value;
        }
    }
}
