using Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Category", Schema = "Shop")]
    public class Category : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentId { get; set; }
        public int Order { get; set; }

        /// <summary>
        /// ارتباط با جدول محصولات
        /// </summary>
        public virtual ICollection<Product> Products{ get; set; }

    }
}
