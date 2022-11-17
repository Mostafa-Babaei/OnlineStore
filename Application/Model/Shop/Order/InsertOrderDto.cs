using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model
{
    public class InsertOrderDto
    {
        public string Description { get; set; }
        public string OrderNumber { get; set; }
        public OrderState State { get; set; }
        public bool IsPayment { get; set; }
        public bool IsActive { get; set; }
        public int Price { get; set; }
        public List<InsertOrderItemDto> ItemsOfOrder { get; set; }
    }
}
