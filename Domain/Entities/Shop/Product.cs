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
        public long Price { get; set; }


    }
}
