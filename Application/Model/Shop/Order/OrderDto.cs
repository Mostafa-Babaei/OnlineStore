using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string Description { get; set; }
        public string OrderNumber { get; set; }
        public OrderState State { get; set; }
        public string Fullname { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime WorkDate { get; set; }
        public bool IsPayment { get; set; }
        public bool IsActive { get; set; }
        public long Price { get; set; }
        public DateTime? DiscountExpirationDate { get; set; }
        public long Discount { get; set; }
        public string DiscountCode { get; set; }
        public Product product { get; set; }
        public string UserOrder { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
