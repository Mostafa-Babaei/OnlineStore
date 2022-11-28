using Application.Common.Model;
using Application.Model;
using Domain.Models;
using infrastructure.Service;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;

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
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ApiResult AddOrder()
        {
            //Todo:بررسی و ثبت سفارش و ارسال ایمیل برای مشتری و ادمین
            if (!User.Identity.IsAuthenticated)
                return ApiResult.ToErrorModel("لطفا وارد شوید");

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

        /// <summary>
        /// دریافت لیست سفارشات
        /// </summary>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetOrders(int? page = 1, int? count = 10)
        {
            return  OrderService.GetOrders(page.Value, count.Value);
        }

        /// <summary>
        /// دریافت لیست سفارشات مشتری
        /// </summary>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetOrdersOfUser(int? page = 1, int? count = 10)
        {
            string userId = GetUser();
            if (GetUser() == null)
                return ApiResult.ToErrorModel("کاربر یافت نشد");
            return  OrderService.GetUserOrders(userId, page.Value, count.Value);
        }

        /// <summary>
        /// دریافت سفارش
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetOrder(string orderNumber)
        {
            //بررسی کاربر
            string userId = GetUser();
            if (GetUser() == null)
                return ApiResult.ToErrorModel("کاربر یافت نشد");

            //بررسی مالکیت سفارش
            if (!OrderService.OrderForUser(orderNumber, userId))
                return ApiResult.ToErrorModel("سفارش متعلق به شما نیست");
            OrderDto orderDto = OrderService.GetOrderDto(orderNumber);

            //اطلاعات محصول
            if (orderDto.OrderItem.Any())
                foreach (var item in orderDto.OrderItem)
                    item.Product = productService.GetProductDto(item.ProductId);

            return ApiResult.ToSuccessModel("سفارش با موفقیت دریافت شد", orderDto);
        }

        /// <summary>
        /// تغییر وضعیت سفارش
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPut]
        public ApiResult changeStateOrder(string orderNumber, OrderState state)
        {
            return OrderService.ChangeStateOrder(orderNumber, state);
        }

        /// <summary>
        /// تسویه سفارش
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult PaymentOrder(string orderNumber)
        {
            return OrderService.PaymentOrders(orderNumber);
        }


        private string GetUser()
        {
            return this.User.Claims.FirstOrDefault(e => e.Type == "userId")?.Value;
        }

    }
}
