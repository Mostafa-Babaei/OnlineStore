
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("Order", Schema = "Shop")]
    public class ShopOrder : BaseEntity
    {
        public ShopOrder()
        {
            IsActive = false;
            IsDelete = false;
            IsPayment = false;
            CreateAt = DateTime.Now;
            State = OrderState.DefineOrder;
        }
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string OrderNumber { get; set; }
        public OrderState State { get; set; }
        public bool IsPayment { get; set; }
        public bool IsActive { get; set; }
        public int Price { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}