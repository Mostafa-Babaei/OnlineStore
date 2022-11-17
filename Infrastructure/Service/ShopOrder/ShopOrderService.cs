using Application.Common;
using Application.Common.Model;
using Application.Model;
using AutoMapper;
using Domain.Enums;
using infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Service.Order
{
    public class ShopOrderService : IShopOrderService
    {
        private readonly IGenericRepository<Domain.Models.ShopOrder> orderRepository;
        private readonly IMapper mapper;

        public ShopOrderService(IGenericRepository<Domain.Models.ShopOrder> orderRepository,
            IMapper mapper
            )
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }
        public ApiResult ChangeStateOrder(string orderNumber, OrderState state)
        {
            var order = GetOrder(orderNumber);
            if (order == null)
                ApiResult.ToErrorModel("سفارش یافت نشد");
            order.State = state;
            int result = orderRepository.SaveEntity();
            if (result <= 0)
                return ApiResult.ToErrorModel("خطا در تغییر وضعیت سفارش");

            return ApiResult.ToSuccessModel("تغییر وضعیت سفارش با موفقیت انجام شد");
        }

        public int CountOrder()
        {
            return orderRepository.Count();
        }

        public string GenerateInvoiceNumber()
        {
            bool repeatFlag = true;
            int tryCount = 5;
            string invoceNumber = "N";
            invoceNumber += ShamsiDate.GetShamsiDateNow().Substring(2, 2);
            while (repeatFlag && tryCount < 0)
            {
                invoceNumber += new Random().Next(10000, 99999).ToString();
                repeatFlag = ExistInvoiceNumber(invoceNumber);
                tryCount--;
            }
            return invoceNumber;
        }

        public OrderDto GetOrderDto(string orderNumber)
        {
            var model = GetOrder(orderNumber);
            return mapper.Map<OrderDto>(model);
        }

        public List<OrderDto> GetOrders(int page, int pageSize)
        {
            PagingDto<Domain.Models.ShopOrder> pagingDto = new PagingDto<Domain.Models.ShopOrder>()
            {
                Page = page,
                PageSize = pageSize
            };
            var orders = orderRepository.GetWithPaging(null, pagingDto);
            var result = mapper.Map<List<Domain.Models.ShopOrder>, List<OrderDto>>(orders.PageData);
            return result;
        }

        public List<OrderDto> GetUserOrders(string userName, int page, int pageSize)
        {
            PagingDto<Domain.Models.ShopOrder> pagingDto = new PagingDto<Domain.Models.ShopOrder>()
            {
                Page = page,
                PageSize = pageSize
            };
            var orders = orderRepository.GetWithPaging(e => e.UserId == userName, pagingDto);
            var result = mapper.Map<List<Domain.Models.ShopOrder>, List<OrderDto>>(orders.PageData);
            return result;
        }

        public int GetUserOrdersCount(string userName)
        {
            return orderRepository.Count(e => e.UserId == userName);
        }

        public ApiResult InsertOrder(InsertOrderDto model)
        {
            var insertDiscount = mapper.Map<Domain.Models.ShopOrder>(model);
            orderRepository.Add(insertDiscount);
            int result = orderRepository.SaveEntity();
            if (result <= 0)
                return ApiResult.ToErrorModel("خطا در ثبت سفارش");

            return ApiResult.ToSuccessModel("ثبت سفارش با موفقیت انجام شد");
            throw new NotImplementedException();
        }

        public Task NotifyNewOrderToAdmin(int orderId, List<string> adminEmails)
        {
            throw new NotImplementedException();
        }

        public bool ExistInvoiceNumber(string invoiceNumber)
        {
            return orderRepository.Exist(e => e.OrderNumber == invoiceNumber);
        }

        public Task SendEmailToCustomer(int orderId)
        {
            throw new NotImplementedException();
        }

        public Domain.Models.ShopOrder GetOrder(string invoiceNumber)
        {
            return orderRepository.Find(e => e.OrderNumber == invoiceNumber).SingleOrDefault();
        }

    }
}
