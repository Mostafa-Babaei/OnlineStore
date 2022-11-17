using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class OrderItem
    {

        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int BasePrice { get; set; }
        public int TotalPrice { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }


        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public ShopOrder Order { get; set; }

    }
}
