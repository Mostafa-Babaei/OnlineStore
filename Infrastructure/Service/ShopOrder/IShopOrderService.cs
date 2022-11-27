using Application.Common.Model;
using Application.Model;
using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Service
{
    public interface IShopOrderService
    {
        /// <summary>
        /// ثبت سفارش جدید
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ApiResult InsertOrder(InsertOrderDto model);

        /// <summary>
        /// تعداد سفارش
        /// </summary>
        /// <returns></returns>
        int CountOrder();

        /// <summary>
        /// تغییر وضعیت سفارش
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        ApiResult ChangeStateOrder(string orderNumber, OrderState state);

        /// <summary>
        /// دریافت مدل سفارش
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        OrderDto GetOrderDto(string orderNumber);

        /// <summary>
        /// دریافت اطلاعات سفارش
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        ShopOrder GetOrder(string invoiceNumber);

        /// <summary>
        /// دریافت لیست سفارشات
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        ApiResult GetOrders(int page, int pageSize);

        /// <summary>
        /// بررسی موجود بودن شماره سفارش
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        bool ExistInvoiceNumber(string invoiceNumber);

        /// <summary>
        /// ایجاد شماره سفارش
        /// </summary>
        /// <returns></returns>
        string GenerateInvoiceNumber();

        /// <summary>
        /// دریافت سفارشات کاربر
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        ApiResult GetUserOrders(string userName, int page, int pageSize);

        /// <summary>
        /// تعداد سفارشات کاربر
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        int GetUserOrdersCount(string userName);

        /// <summary>
        /// ارسال ایمیل ثبت سفارش
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task SendEmailToCustomer(int orderId);

        /// <summary>
        /// گزارش ثبت سفارش به مدیر
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="adminEmails"></param>
        /// <returns></returns>
        Task NotifyNewOrderToAdmin(int orderId, List<string> adminEmails);

        /// <summary>
        /// بررسی مالکیت سفارش مشتری
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool OrderForUser(string orderNumber, string userId);
    }
}
