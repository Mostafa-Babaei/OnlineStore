using Application.Common.Model;
using Application.Model;
using Domain.Models;
using infrastructure.Service;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace OnlineStore.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IShopOrderService OrderService;
        private readonly IProductService productService;

        public OrderController(IShoppingCartService shoppingCartService, IShopOrderService OrderService, IProductService productService)
        {
            this.shoppingCartService = shoppingCartService;
            this.OrderService = OrderService;
            this.productService = productService;
        }

        /// <summary>
        /// ثبت سفارش
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult AddOrder()
        {
            //Todo:بررسی و ثبت سفارش و ارسال ایمیل برای مشتری و ادمین
            if (!User.Identity.IsAuthenticated)
            {
                return ApiResult.ToErrorModel("لطفا وارد شوید");
            }

            string userId = this.User.Claims.FirstOrDefault(e => e.Type == "userId")?.Value;

            if (string.IsNullOrEmpty(userId))
                return ApiResult.ToErrorModel("کاربر یافت نشد ، لطفا دوباره امتحان کنید");

            if (!shoppingCartService.ExistCartForUser(userId))
                return ApiResult.ToErrorModel("سبد خرید شما خالی است");

            List<ShoppingCartDto> items = shoppingCartService.GetItemsOfCustomer(userId);

            InsertOrderDto model = new InsertOrderDto();
            List<InsertOrderItemDto> itemOrder = new List<InsertOrderItemDto>();
            foreach (var item in items)
            {
                Product product = productService.GetProduct(item.ProductId);
                itemOrder.Add(new InsertOrderItemDto()
                {
                    BasePrice = product.Price,
                    ProductId = product.ProductId,
                    Product = product,
                    Quantity = item.Count,
                    TotalPrice = product.Price * item.Count

                });
            }
            model.ItemsOfOrder = itemOrder;
            return OrderService.InsertOrder(model);

        }
    }
}
