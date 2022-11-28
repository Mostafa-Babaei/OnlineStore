using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities;

namespace Domain.Models
{
    [Table("Product", Schema = "Shop")]
    public class Product : BaseEntity
    {
        [Key]
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PicPath { get; set; }
        public bool IsActive { get; set; }
        public int Quntity { get; set; }
        public int Price { get; set; }

        /// <summary>
        /// ارتباط با جدول  دسته بندی
        /// </summary>
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }


        /// <summary>
        /// ارتباط با برند
        /// </summary>
        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

        /// <summary>
        /// ارتباط با سفارش
        /// </summary>
        public virtual ICollection<ShopOrder> ShopOrders { get; set; }

        /// <summary>
        /// ارتباط با سبد خرید
        /// </summary>
        public virtual ICollection<OrderItem> OrderItems { get; set; }


    }
}
