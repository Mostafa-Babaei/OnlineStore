using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Category", Schema = "Shop")]
    public class Brand : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Logo { get; set; }
        public bool IsActive{ get; set; }
    }
}
