
using Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{

    [Table("UseDiscount", Schema = "Shop")]
    public class UseDiscount : BaseEntity
    {

        [Key]
        public int UseDiscountId { get; set; }

        public DateTime UseDate { get; set; }

        public int OrderId { get; set; }

        public int Amount { get; set; }
            
        public bool IsCancel { get; set; }

        public int DiscountId { get; set; }

        public virtual Discount Discount{ get; set; }

    }
}