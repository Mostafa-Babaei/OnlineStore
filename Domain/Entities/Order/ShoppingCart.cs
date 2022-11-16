using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("ShoppingCart", Schema = "Order")]
    public class ShoppingCart : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        
        public int Count { get; set; }
        
        public string UserId { get; set; }

    }
}
