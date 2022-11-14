using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryPic { get; set; }
        public int ParentId { get; set; }
        public int Order { get; set; }

    }
}
