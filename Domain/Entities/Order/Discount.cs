using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Domain.Entities
{
    [Table("Discount", Schema = "Shop")]
    public class Discount : BaseEntity
    {
        [Key]
        public int DiscountId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public DiscountType DiscountType { get; set; }
        public int Amount { get; set; }
        public int Count { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<UseDiscount> UseDiscounts { get; set; }
    }

}