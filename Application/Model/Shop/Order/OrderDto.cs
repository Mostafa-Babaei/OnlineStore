using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string OrderNumber { get; set; }
        public int State { get; set; }
        public bool IsPayment { get; set; }
        public bool IsActive { get; set; }
        public int Price { get; set; }
        public List<OrderItemDto> OrderItem { get; set; }
    }
}
