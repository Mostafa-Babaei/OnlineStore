using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.Model
{
    public class ShopingCartDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        
        [NotMapped]
        public Product Product { get; set; }

        public int Count { get; set; }

        public string UserId { get; set; }
    }
}
